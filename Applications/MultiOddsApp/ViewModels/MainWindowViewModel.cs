using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using OxyPlot;
using ReactiveUI;

namespace MultiOddsApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _pocketHand = "QQ-88";
        private string _board = "2h 3h 4h";
        private bool _win = true;
        private bool _handStrength = true;
        private bool _potential = true;
        private int _accuracyLevel = 6;

        public MainWindowViewModel()
        {
            CalculateCommand = ReactiveCommand.Create(Calculate);
        }

        public string PocketHand
        {
            get => _pocketHand; 
            set => this.RaiseAndSetIfChanged(ref _pocketHand, value);
        }

        public string Board
        {
            get => _board;
            set => this.RaiseAndSetIfChanged(ref _board, value);
        }

        public bool Win
        {
            get => _win;
            set => this.RaiseAndSetIfChanged(ref _win, value);
        }

        public bool HandStrength
        {
            get => _handStrength;
            set => this.RaiseAndSetIfChanged(ref _handStrength, value);
        }

        public bool Potential
        {
            get => _potential;
            set => this.RaiseAndSetIfChanged(ref _potential, value);
        }

        public int AccuracyLevel
        {
            get => _accuracyLevel;
            set => this.RaiseAndSetIfChanged(ref _accuracyLevel, value);
        }
        
        public PlotModel PlotModel { get; private set; }

        public ReactiveCommand<Unit, Unit> CalculateCommand { get; }

        private void Calculate()
        {
            
        }
    }
}
