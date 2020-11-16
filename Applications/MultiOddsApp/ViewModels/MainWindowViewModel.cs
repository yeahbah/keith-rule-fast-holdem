using System.Reactive;
using ReactiveUI;

namespace MultiOddsApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _pocketHand = "AA, KK, QQ, JJ";
        private string _board = "Ts Qs 2d";
        private string _opponentHand = "99";

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

        public string OpponentHand
        {
            get => _opponentHand;
            set => this.RaiseAndSetIfChanged(ref _opponentHand, value);
        }

        public string Board
        {
            get => _board;
            set => this.RaiseAndSetIfChanged(ref _board, value);
             
        }

        public ReactiveCommand<Unit, Unit> ReloadOddsCommand { get; }
        
        public void ReloadOddsAction()
        {
            OddsGridViewModel?.ReloadHandOdds(PocketHand, Board);
        }

        public OddsGridViewModel OddsGridViewModel { get; set; }
    }
}
