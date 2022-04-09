using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using HAQ_Calculator.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HAQ_Calculator
{
    public class HaqCalculator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public HaqCalculator()
        {
            DressingAndPersonalCare.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.DressingAndPersonalCare).Count]);
            GettingUp.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.GettingUp).Count]);
            Meal.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.Meal).Count]);
            Walks.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.Walks).Count]);
            Hygiene.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.Hygiene).Count]);
            AchievableRange.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.AchievableRange).Count]);
            PowerBrushes.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.PowerBrushes).Count]);
            OtherActivities.QuestionsPoints =
                new List<int>(new int[GetQuestions(Chapters.OtherActivities).Count]);
        }

        private Chapter _dressingAndPersonalCare = new();
        private Chapter _gettingUp = new();
        private Chapter _meal = new();
        private Chapter _walks = new();
        private Chapter _hygiene = new();
        private Chapter _achievableRange = new();
        private Chapter _powerBrushes = new();
        private Chapter _otherActivities = new();
        private Chapter _firstHalf = new();
        private Chapter _secondHalf = new();
        private double _totalPoints;
        private double _pain;
        private double _haq;
        private double _includeChapters;
        
        public Chapters ActiveChapter { get; set; }
        public double IncludeChapters
        {
            get => _includeChapters;
            set
            {
                _includeChapters = value;
                OnPropertyChanged();
            }
        }
        public double Haq
        {
            get => _haq;
            set
            {
                _haq = Math.Round(value, 2);
                OnPropertyChanged();
            }
        }
        public double TotalPoints
        {
            get => _totalPoints;
            set
            {
                _totalPoints = value;
                OnPropertyChanged();
            }
        }
        public double Pain
        {
            get => _pain;
            set
            {
                _pain = value;
                OnPropertyChanged();
            }
        }
        public Chapter DressingAndPersonalCare
        {
            get => _dressingAndPersonalCare;
            set
            {
                _dressingAndPersonalCare = value;
                OnPropertyChanged();
            } 
        }
        public Chapter GettingUp
        {
            get => _gettingUp;
            set
            {
                _gettingUp = value;
                OnPropertyChanged();
            } 
        }
        public Chapter Meal
        {
            get => _meal;
            set
            {
                _meal = value;
                OnPropertyChanged();
            } 
        }
        public Chapter Walks
        {
            get => _walks;
            set
            {
                _walks = value;
                OnPropertyChanged();
            } 
        }
        public Chapter Hygiene
        {
            get => _hygiene;
            set
            {
                _hygiene = value;
                OnPropertyChanged();
            } 
        }
        public Chapter AchievableRange
        {
            get => _achievableRange;
            set
            {
                _achievableRange = value;
                OnPropertyChanged();
            } 
        }
        public Chapter PowerBrushes
        {
            get => _powerBrushes;
            set
            {
                _powerBrushes = value;
                OnPropertyChanged();
            } 
        }
        public Chapter OtherActivities
        {
            get => _otherActivities;
            set
            {
                _otherActivities = value;
                OnPropertyChanged();
            } 
        }
        public Chapter FirstHalf
        {
            get => _firstHalf;
            set
            {
                _firstHalf = value;
                OnPropertyChanged();
            } 
        }
        public Chapter SecondHalf
        {
            get => _secondHalf;
            set
            {
                _secondHalf = value;
                OnPropertyChanged();
            } 
        }

        public List<string> GetQuestions(Chapters chapter)
        {
            var list = JObject.Parse(File.ReadAllText("Questions.json"));
            return JsonConvert.DeserializeObject<List<string>>(list[chapter.ToString()]!.ToString());
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}