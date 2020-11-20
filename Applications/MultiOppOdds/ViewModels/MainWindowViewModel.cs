using System.Reactive;
using ReactiveUI;
using HoldemHand;
using OxyPlot;
using OxyPlot.Series;
using System.Drawing;
using OxyPlot.Axes;
using System.Threading.Tasks;
using System;

namespace MultiOddsApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _pocketHand = "As Ks";
        private string _board = "2h 3h 4h";
        private bool _win = true;
        private bool _handStrength = true;
        private bool _potential = true;
        private int _accuracyLevel = 6;
        private PlotModel _plotModel;
        private bool _showStatusBar = false;
        private string _statusText;

        public MainWindowViewModel()
        {
            CalculateCommand = ReactiveCommand.Create(Calculate);
            //Calculate();
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
        
        public PlotModel PlotModel 
        { 
            get => _plotModel; 
            private set => this.RaiseAndSetIfChanged(ref _plotModel, value); 
        }

        public bool ShowStatusBar
        {
            get => _showStatusBar;
            set => this.RaiseAndSetIfChanged(ref _showStatusBar, value);
        }

        public string StatusText
        {
            get => _statusText;
            set => this.RaiseAndSetIfChanged(ref _statusText, value);
        }

        public ReactiveCommand<Unit, Unit> CalculateCommand { get; }

        private void Calculate()
        {            
            Task.Run(GenerateChartData);
        }

        private void GenerateChartData()
        {
            ShowStatusBar = true;
            StatusText = "Calculating... please wait.";

            double[] speedtbl = {
               0.05, 0.075, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.25
            };

            string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9"};

            var winSeries = new LineSeries 
            {
                Title = "Win",
                MarkerType = MarkerType.Circle,   
                Color = OxyColor.FromRgb(0, 255, 0),
                StrokeThickness = 2.0F,          
            };
            var handStrengthSeries = new LineSeries
            {
                Title = "Hand Strength",
                MarkerType = MarkerType.Circle,
                StrokeThickness = 2.0F,
                Color = OxyColor.FromRgb(0, 0, 0),                
            };
            var positivePotential = new LineSeries
            {
                Title = "Positive Potential",
                MarkerType = MarkerType.Circle,
                StrokeThickness = 2.0F,
            };
            var negativePotential = new LineSeries
            {
                Title = "Negative Potential",
                MarkerType = MarkerType.Circle,                
                StrokeThickness = 2.0F,
                Color = OxyColor.FromRgb(255, 0, 0),
            };            

            // Validate Data
            try             
            {
                if (!PocketHands.ValidateQuery(PocketHand)) return;
                if (Board != "" && !Hand.ValidateHand(Board)) return;

                ulong pocketmask = Hand.ParseHand(PocketHand);
                ulong boardmask = Hand.ParseHand(Board);
                bool bCalcPot = (Potential && (Hand.BitCount(boardmask) == 3 || Hand.BitCount(boardmask) == 4));
                bool bCalcStrength = HandStrength;
                bool bCalcWin = Win;

                if (Hand.BitCount(pocketmask) != 2) return;
                if (Hand.BitCount(boardmask) > 5) return;
            

                // Calculate Data            
                for (int i = 1; i < 10; i++)
                {
                    if (bCalcPot) 
                    {
                        double npotv, ppotv;
                        Hand.HandPotential(PocketHand, Board, out ppotv, out npotv, i, speedtbl[AccuracyLevel]);
                        negativePotential.Points.Add(new DataPoint(i, 100.0 * npotv));
                        positivePotential.Points.Add(new DataPoint(i, 100.0 * ppotv));                                        
                    }

                    if (bCalcStrength)
                    {
                        handStrengthSeries.Points.Add(new DataPoint(i, 100.0 * Hand.HandStrength(PocketHand, Board, i, speedtbl[AccuracyLevel])));                    
                    }

                    if (bCalcWin)
                    {
                        winSeries.Points.Add(new DataPoint(i, Hand.WinOdds(PocketHand, Board, "", i, speedtbl[AccuracyLevel]) * 100.0));                                        
                    }
                }

                var tmp = new PlotModel 
                { 
                    Title = string.Format("Texas Holdem Odds [{0}{1}{2}]",PocketHand, Board != "" ? " - " : "", Board),                                 
                };
                if (bCalcPot) 
                {
                    tmp.Series.Add(negativePotential);
                    tmp.Series.Add(positivePotential);
                }               
                if (bCalcStrength) tmp.Series.Add(handStrengthSeries);         
                if (bCalcWin) tmp.Series.Add(winSeries);

                var xAxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Minimum = 1,
                    Maximum = 9,                
                    IntervalLength = 50
                };
                tmp.Axes.Add(xAxis);
                
                var valueAxis = new LinearAxis
                {
                    Position = AxisPosition.Left,
                    // Minimum = minimum - margin,
                    // Maximum = maximum + margin,
                };
                tmp.Axes.Add(valueAxis);


                this.PlotModel = tmp;
            }
            catch (Exception ex)
            {
                ShowStatusBar = true;
                StatusText = ex.Message;
                throw;
            }

            ShowStatusBar = false;

        }
    }
}
