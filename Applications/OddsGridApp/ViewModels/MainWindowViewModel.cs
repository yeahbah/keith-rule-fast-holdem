using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace OddsGridApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _pocketHand = "As Ks";
        private string _board = "Ts Qs 2d";

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
