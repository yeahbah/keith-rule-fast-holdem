using System.Reactive;
using ReactiveUI;

namespace MultiOddsApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _pocketHand = "As Ac";
        private string _board = "Ts Qs 2d";
        private int _numberOfOpponentsIndex = 0;

        public MainWindowViewModel()
        {
            OddsGridViewModel = new OddsGridViewModel();
            ReloadOddsCommand = ReactiveCommand.Create(ReloadOddsAction);
        }

        public string PocketHand
        {
            get => _pocketHand;
            set => this.RaiseAndSetIfChanged(ref _pocketHand, value);             
        }

        public int NumberOfOpponentsIndex
        {
            get => _numberOfOpponentsIndex;
            set => this.RaiseAndSetIfChanged(ref _numberOfOpponentsIndex, value);
        }

        public string Board
        {
            get => _board;
            set => this.RaiseAndSetIfChanged(ref _board, value);
             
        }

        public ReactiveCommand<Unit, Unit> ReloadOddsCommand { get; }
        
        public void ReloadOddsAction()
        {
            OddsGridViewModel?.ReloadHandOdds(PocketHand, Board, NumberOfOpponentsIndex + 1);
        }        

        public OddsGridViewModel OddsGridViewModel { get; set; }
    }
}
