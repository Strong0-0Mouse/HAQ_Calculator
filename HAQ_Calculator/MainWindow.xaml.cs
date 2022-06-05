using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HAQ_Calculator
{
    public partial class MainWindow
    {
        private readonly HaqCalculator _haqCalculator;

        private bool _isOpenFirstHalfINfo;
        private bool _isOpenSecondHalfINfo;
        
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
            
            FirstHalfButtonInfo.Click += FirstHalfButtonInfoOnClick;
            _haqCalculator.FirstHalf.ListAnswers.CollectionChanged += ListAnswersFirstOnCollectionChanged;
            
            SecondHalfButtonInfo.Click += SecondHalfButtonInfoOnClick;
            _haqCalculator.SecondHalf.ListAnswers.CollectionChanged += ListAnswersSecondOnCollectionChanged;

            PrevHaqValue.TextChanged += PrevHaqValueOnTextChanged;

            InitializeTest(false);
        }

        private void PrevHaqValueOnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var value = double.Parse((sender as TextBox)!.Text);
                (sender as TextBox)!.Text = value switch
                {
                    > 3.5 => "3.5",
                    < 0 => "0",
                    _ => (sender as TextBox)!.Text
                };
                _haqCalculator.PrevHaq = double.Parse((sender as TextBox)!.Text);
                _haqCalculator.DeltaHaq = _haqCalculator.Haq / _haqCalculator.PrevHaq;
            }
            catch
            {
                (sender as TextBox)!.Text = string.Empty;
            }
        }

        #region FirstHalf

        private void ListAnswersFirstOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _isOpenFirstHalfINfo = !_isOpenFirstHalfINfo;
            UpdateFirstList();
            _isOpenFirstHalfINfo = !_isOpenFirstHalfINfo;
            UpdateFirstList();
        }

        private void FirstHalfButtonInfoOnClick(object sender, RoutedEventArgs e)
        {
            _isOpenFirstHalfINfo = !_isOpenFirstHalfINfo;
            UpdateFirstList();
        }

        private void UpdateFirstList()
        {
            if (_isOpenFirstHalfINfo)
            {
                var scroll = new ScrollViewer();
                var stack = new StackPanel();
                foreach (var answer in _haqCalculator.FirstHalf.ListAnswers)
                {
                    var textBlock = new TextBox {Text = answer, TextWrapping = TextWrapping.Wrap};
                    textBlock.FontSize = 16;
                    textBlock.TextChanged += TextBlockFirstOnTextChanged;
                    stack.Children.Add(textBlock);
                }
                scroll.Content = stack;
                Grid.SetRow(scroll, 2);
                FirstHalfInfo.Children.Add(scroll);
            }
            else
            {
                FirstHalfInfo.Children.RemoveAt(FirstHalfInfo.Children.Count - 1);
            }
        }

        private void TextBlockFirstOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isOpenFirstHalfINfo = !_isOpenFirstHalfINfo;
            UpdateFirstList();
            _isOpenFirstHalfINfo = !_isOpenFirstHalfINfo;
            UpdateFirstList();
        }

        #endregion

        #region SecondHalf

        private void ListAnswersSecondOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _isOpenSecondHalfINfo = !_isOpenSecondHalfINfo;
            UpdateSecondList();
            _isOpenSecondHalfINfo = !_isOpenSecondHalfINfo;
            UpdateSecondList();
        }

        private void SecondHalfButtonInfoOnClick(object sender, RoutedEventArgs e)
        {
            _isOpenSecondHalfINfo = !_isOpenSecondHalfINfo;
            UpdateSecondList();
        }

        private void UpdateSecondList()
        {
            if (_isOpenSecondHalfINfo)
            {
                var scroll = new ScrollViewer();
                var stack = new StackPanel();
                foreach (var answer in _haqCalculator.SecondHalf.ListAnswers)
                {
                    var textBlock = new TextBox {Text = answer, TextWrapping = TextWrapping.Wrap};
                    textBlock.TextChanged += TextBlockSecondOnTextChanged;
                    stack.Children.Add(textBlock);
                }
                scroll.Content = stack;
                Grid.SetRow(scroll, 2);
                SecondHalfInfo.Children.Add(scroll);
            }
            else
            {
                SecondHalfInfo.Children.RemoveAt(SecondHalfInfo.Children.Count - 1);
            }
        }

        private void TextBlockSecondOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isOpenSecondHalfINfo = !_isOpenSecondHalfINfo;
            UpdateSecondList();
            _isOpenSecondHalfINfo = !_isOpenSecondHalfINfo;
            UpdateSecondList();
        }

        #endregion

        private void InitializeTest(bool isReinitialize)
        {
            _haqCalculator.IncludeChapters = 0;
            _haqCalculator.TotalPoints = 0;
            _haqCalculator.Haq = 0;
            _haqCalculator.DressingAndPersonalCare.IsEnabled = false;
            _haqCalculator.DressingAndPersonalCare.TotalPoints = 0;
            _haqCalculator.GettingUp.IsEnabled = false;
            _haqCalculator.GettingUp.TotalPoints = 0;
            _haqCalculator.Meal.IsEnabled = false;
            _haqCalculator.Meal.TotalPoints = 0;
            _haqCalculator.Walks.IsEnabled = false;
            _haqCalculator.Walks.TotalPoints = 0;
            _haqCalculator.Hygiene.IsEnabled = false;
            _haqCalculator.Hygiene.TotalPoints = 0;
            _haqCalculator.AchievableRange.IsEnabled = false;
            _haqCalculator.AchievableRange.TotalPoints = 0;
            _haqCalculator.PowerBrushes.IsEnabled = false;
            _haqCalculator.PowerBrushes.TotalPoints = 0;
            _haqCalculator.OtherActivities.IsEnabled = false;
            _haqCalculator.OtherActivities.TotalPoints = 0;

            _haqCalculator.FirstHalf.TotalPoints = 0;
            _haqCalculator.SecondHalf.TotalPoints = 0;

            _haqCalculator.FirstHalf.ListAnswers = new();
            _haqCalculator.SecondHalf.ListAnswers = new();
            
            SetEnabledProperty(_haqCalculator.DressingAndPersonalCare, IndicatorDressingAndPersonalCare, DressingAndPersonalCare,
                IndicatorDressingAndPersonalCareResult);
            SetEnabledProperty(_haqCalculator.GettingUp, IndicatorGettingUp, GettingUp,
                IndicatorGettingUpResult);
            SetEnabledProperty(_haqCalculator.Meal, IndicatorMeal, Meal,
                IndicatorMealResult);
            SetEnabledProperty(_haqCalculator.Walks, IndicatorWalks, Walks,
                IndicatorWalksResult);
            SetEnabledProperty(_haqCalculator.Hygiene, IndicatorHygiene, Hygiene,
                IndicatorHygieneResult);
            SetEnabledProperty(_haqCalculator.AchievableRange, IndicatorAchievableRange, AchievableRange,
                IndicatorAchievableRangeResult);
            SetEnabledProperty(_haqCalculator.PowerBrushes, IndicatorPowerBrushes, PowerBrushes,
                IndicatorPowerBrushesResult);
            SetEnabledProperty(_haqCalculator.OtherActivities, IndicatorOtherActivities, OtherActivities,
                IndicatorOtherActivitiesResult);
            AddQuestions("Одевание и уход за собой", DressingAndPersonalCare, isReinitialize, QuestionsDressingAndPersonalCare);
            AddQuestions("Вставание", GettingUp, isReinitialize, QuestionsGettingUp);
            AddQuestions("Питание", Meal, isReinitialize, QuestionsMeal);
            AddQuestions("Ходьба", Walks, isReinitialize, QuestionsWalks);
            AddQuestions("Приспособления", FirstHalfFixtures, isReinitialize);
            AddQuestions("Нужда в посторонней помощи", FirstHalfNeedHelp, isReinitialize);
            AddQuestions("Гигиена", Hygiene, isReinitialize, QuestionsHygiene);
            AddQuestions("Достижимый радиус действия", AchievableRange, isReinitialize, QuestionsAchievableRange);
            AddQuestions("Сила кистей", PowerBrushes, isReinitialize, QuestionsPowerBrushes);
            AddQuestions("Прочие виды деятельности",  OtherActivities, isReinitialize, QuestionsOtherActivities);
            AddQuestions("Приспособления", SecondHalfFixtures, isReinitialize);
            AddQuestions("Нужда в посторонней помощи", SecondHalfNeedHelp, isReinitialize);
            AddQuestions("Боль", Pain, isReinitialize, StackPain);
        }

        private void ChapterGotFocus(object sender, RoutedEventArgs e)
        {
            var chapter = (Chapters) Enum.Parse(typeof(Chapters), (sender as GroupBox)!.Name, true);
            _haqCalculator.ActiveChapter = chapter;
        }

        private void AddQuestions(string header, HeaderedContentControl item, bool isReinitialize, Panel stack = null)
        {
            var chapter = (Chapters) Enum.Parse(typeof(Chapters), item.Name, true);
            var lines = _haqCalculator.GetQuestions(chapter);
            var questionNum = 0;
            if (chapter != Chapters.FirstHalfFixtures && chapter != Chapters.SecondHalfFixtures &&
                chapter != Chapters.FirstHalfNeedHelp && chapter != Chapters.SecondHalfNeedHelp &&
                chapter != Chapters.Pain)
            {
                if (isReinitialize)
                    for (var i = 0; i < lines.Count; i++)
                        stack?.Children.RemoveAt(stack.Children.Count - 1);
                        
                foreach (var questionElement in lines)
                {
                    var question = new Question(questionElement, _haqCalculator, chapter, questionNum);
                    stack?.Children.Add(question.GetControl());
                    questionNum++;
                }
                
                ((item.Header as StackPanel)!.Children[0] as TextBlock)!.Text = header;
                if (!isReinitialize)
                    ((item.Header as StackPanel)!.Children[1] as Button)!.Click += EnabledChanged;
                item.Content = stack;
            }
            else if (chapter == Chapters.Pain)
            {
                ((item.Header as StackPanel)!.Children[0] as TextBlock)!.Text = header;
                var question = new Question(lines[0], _haqCalculator, chapter, 0, new List<string>());
                if (isReinitialize)
                {
                    stack?.Children.RemoveAt(0);
                    SliderPain.Value = 0;
                }

                stack?.Children.Insert(0, question.GetControl());
            }
            else
            {
                ((item.Header as StackPanel)!.Children[0] as TextBlock)!.Text = header;
                var questionElement = new Question(lines[0], _haqCalculator, chapter, 0, lines.Skip(1).ToList());
                stack?.Children.Clear();
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

        private void SetEnabledProperty(Chapter chapter, Ellipse item, GroupBox box, Ellipse resultItem = null)
        {
            if (chapter.IsEnabled)
            {
                chapter.IsEnabled = false;
                box.Background = new SolidColorBrush(Colors.Gainsboro);
                ((box.Header as StackPanel)!.Children[0] as TextBlock)!.Margin = new Thickness(0, 0, 0, 20);
                ((box.Header as StackPanel)!.Children[1] as Button)!.Content = "Учитывать раздел";
                item.Fill = Brushes.Red;
                if (resultItem != null) 
                    resultItem.Fill = Brushes.Red;
                _haqCalculator.IncludeChapters--;
                _haqCalculator.TotalPoints -= chapter.TotalPoints;
            }
            else
            {
                chapter.IsEnabled = true;
                box.Background = null;
                ((box.Header as StackPanel)!.Children[0] as TextBlock)!.Margin = new Thickness(0, 0, 0, 0);
                ((box.Header as StackPanel)!.Children[1] as Button)!.Content = "Не учитывать раздел";
                item.Fill = Brushes.Green;
                if (resultItem != null) 
                    resultItem.Fill = Brushes.Green;
                _haqCalculator.IncludeChapters++;
                _haqCalculator.TotalPoints += chapter.TotalPoints;
            }

            if (_haqCalculator.IncludeChapters < 6)
                _haqCalculator.Haq = -1;
            else
            {
                _haqCalculator.Haq = _haqCalculator.TotalPoints / _haqCalculator.IncludeChapters;
                _haqCalculator.DeltaHaq = _haqCalculator.Haq / _haqCalculator.PrevHaq;
            }
        }

        private void EnabledChanged(object sender, RoutedEventArgs e)
        {
            switch (_haqCalculator.ActiveChapter)
            {
                case Chapters.DressingAndPersonalCare:
                    SetEnabledProperty(_haqCalculator.DressingAndPersonalCare, IndicatorDressingAndPersonalCare, DressingAndPersonalCare,
                        IndicatorDressingAndPersonalCareResult);
                    break;
                case Chapters.GettingUp:
                    SetEnabledProperty(_haqCalculator.GettingUp, IndicatorGettingUp, GettingUp,
                        IndicatorGettingUpResult);
                    break;
                case Chapters.Meal:
                    SetEnabledProperty(_haqCalculator.Meal, IndicatorMeal, Meal,
                        IndicatorMealResult);
                    break;
                case Chapters.Walks:
                    SetEnabledProperty(_haqCalculator.Walks, IndicatorWalks, Walks,
                        IndicatorWalksResult);
                    break;
                case Chapters.Hygiene:
                    SetEnabledProperty(_haqCalculator.Hygiene, IndicatorHygiene, Hygiene,
                        IndicatorHygieneResult);
                    break;
                case Chapters.AchievableRange:
                    SetEnabledProperty(_haqCalculator.AchievableRange, IndicatorAchievableRange, AchievableRange,
                        IndicatorAchievableRangeResult);
                    break;
                case Chapters.PowerBrushes:
                    SetEnabledProperty(_haqCalculator.PowerBrushes, IndicatorPowerBrushes, PowerBrushes,
                        IndicatorPowerBrushesResult);
                    break;
                case Chapters.OtherActivities:
                    SetEnabledProperty(_haqCalculator.OtherActivities, IndicatorOtherActivities, OtherActivities,
                        IndicatorOtherActivitiesResult);
                    break;
         }
        }

        private void OutputResult(object sender, RoutedEventArgs e)
        {
            _haqCalculator.Haq = _haqCalculator.TotalPoints / _haqCalculator.IncludeChapters;
            _haqCalculator.DeltaHaq = _haqCalculator.Haq / _haqCalculator.PrevHaq;
            if (_haqCalculator.IncludeChapters < 6)
                MessageBox.Show(
                    $"Кол-во учитываемых разделов должно быть ≥ 6\nСейчас учитывается лишь {_haqCalculator.IncludeChapters}",
                    "Ошибка");
            else
                MessageBox.Show(
                    $"HAQ ({_haqCalculator.TotalPoints}/{_haqCalculator.IncludeChapters}) = {_haqCalculator.Haq}",
                    "Результат");
        }

        private void ClearTest(object sender, RoutedEventArgs e)
        {
            InitializeTest(true);
        }
    }
}