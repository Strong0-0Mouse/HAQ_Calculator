using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HAQ_Calculator
{
    public partial class MainWindow
    {
        private readonly HaqCalculator _haqCalculator;
        public MainWindow()
        {
            InitializeComponent();
            _haqCalculator = new HaqCalculator();
            DataContext = _haqCalculator;
            DressingAndPersonalCare.GotFocus += ChapterGotFocus;
            GettingUp.GotFocus += ChapterGotFocus;
            Meal.GotFocus += ChapterGotFocus;
            Walks.GotFocus += ChapterGotFocus;
            Hygiene.GotFocus += ChapterGotFocus;
            AchievableRange.GotFocus += ChapterGotFocus;
            PowerBrushes.GotFocus += ChapterGotFocus;
            OtherActivities.GotFocus += ChapterGotFocus;
            
            DressingAndPersonalCare.LostFocus += ChapterLostFocus;
            GettingUp.LostFocus += ChapterLostFocus;
            Meal.LostFocus += ChapterLostFocus;
            Walks.LostFocus += ChapterLostFocus;
            Hygiene.LostFocus += ChapterLostFocus;
            AchievableRange.LostFocus += ChapterLostFocus;
            PowerBrushes.LostFocus += ChapterLostFocus;
            OtherActivities.LostFocus += ChapterLostFocus;

            _haqCalculator.IncludeChapters = 8;
            
            AddQuestions(DressingAndPersonalCare, QuestionsDressingAndPersonalCare);
            AddQuestions(GettingUp, QuestionsGettingUp);
            AddQuestions(Meal, QuestionsMeal);
            AddQuestions(Walks, QuestionsWalks);
            AddQuestions(FirstHalfFixtures);
            AddQuestions(FirstHalfNeedHelp);
            AddQuestions(Hygiene, QuestionsHygiene);
            AddQuestions(AchievableRange, QuestionsAchievableRange);
            AddQuestions(PowerBrushes, QuestionsPowerBrushes);
            AddQuestions(OtherActivities, QuestionsOtherActivities);
            AddQuestions(SecondHalfFixtures);
            AddQuestions(SecondHalfNeedHelp);
        }

        private void ChapterLostFocus(object sender, RoutedEventArgs e)
        {
            ChangeEnableChapterButton.IsEnabled = false;
        }

        private void ChapterGotFocus(object sender, RoutedEventArgs e)
        {
            ChangeEnableChapterButton.IsEnabled = true;
            var chapter = (Chapters) Enum.Parse(typeof(Chapters), (sender as TabItem)!.Name, true);
            _haqCalculator.ActiveChapter = chapter;
        }

        private void AddQuestions(ContentControl item, Panel stack = null)
        {
            var chapter = (Chapters) Enum.Parse(typeof(Chapters), item.Name, true);
            var lines = _haqCalculator.GetQuestions(chapter);
            var questionNum = 0;
            if (chapter != Chapters.FirstHalfFixtures && chapter != Chapters.SecondHalfFixtures &&
                chapter != Chapters.FirstHalfNeedHelp && chapter != Chapters.SecondHalfNeedHelp)
            {
                foreach (var questionElement in lines)
                {
                    var question = new Question(questionElement, _haqCalculator, chapter, questionNum);
                    stack?.Children.Add(question.GetControl());
                    questionNum++;
                }

                item.Content = stack;
            }
            else
            {
                var questionElement = new Question(lines[0], _haqCalculator, chapter, 0, lines.Skip(1).ToList());
                item.Content = questionElement.GetControl();
            }
            
        }

        private void PainChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _haqCalculator.Pain = Math.Round(SliderPain.Value);
        }

        private void PainTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = int.Parse((sender as TextBox)!.Text);
                (sender as TextBox)!.Text = value switch
                {
                    > 100 => "100",
                    < 0 => "0",
                    _ => (sender as TextBox)!.Text
                };
                _haqCalculator.Pain = int.Parse((sender as TextBox)!.Text);
                SliderPain.Value = int.Parse((sender as TextBox)!.Text);
            }
            catch
            {
                (sender as TextBox)!.Text = string.Empty;
            }
        }

        private void EnabledChanged(object sender, RoutedEventArgs e)
        {
            switch (_haqCalculator.ActiveChapter)
            {
                case Chapters.DressingAndPersonalCare:
                    SetEnabledProperty(_haqCalculator.DressingAndPersonalCare, DressingAndPersonalCare);
                    break;
                case Chapters.GettingUp:
                    SetEnabledProperty(_haqCalculator.GettingUp, GettingUp);
                    break;
                case Chapters.Meal:
                    SetEnabledProperty(_haqCalculator.Meal, Meal);
                    break;
                case Chapters.Walks:
                    SetEnabledProperty(_haqCalculator.Walks, Walks);
                    break;
                case Chapters.Hygiene:
                    SetEnabledProperty(_haqCalculator.Hygiene, Hygiene);
                    break;
                case Chapters.AchievableRange:
                    SetEnabledProperty(_haqCalculator.AchievableRange, AchievableRange);
                    break;
                case Chapters.PowerBrushes:
                    SetEnabledProperty(_haqCalculator.PowerBrushes, PowerBrushes);
                    break;
                case Chapters.OtherActivities:
                    SetEnabledProperty(_haqCalculator.OtherActivities, OtherActivities);
                    break;
            }
        }

        private void SetEnabledProperty(Chapter chapter, ContentControl item)
        {
            if (chapter.IsEnabled)
            {
                chapter.IsEnabled = false;
                item.Background = Brushes.Tomato;
                _haqCalculator.IncludeChapters--;
                _haqCalculator.TotalPoints -= chapter.TotalPoints;
            }
            else
            {
                chapter.IsEnabled = true;
                item.Background = Brushes.Transparent;
                _haqCalculator.IncludeChapters++;
                _haqCalculator.TotalPoints += chapter.TotalPoints;
            }

            if (_haqCalculator.IncludeChapters < 6)
                _haqCalculator.Haq = -1;
            else
                _haqCalculator.Haq = _haqCalculator.TotalPoints / _haqCalculator.IncludeChapters;
        }
    }
}