using System.Collections.ObjectModel;
using OddsGridApp.Models;
using HoldemHand;
using System.Threading.Tasks;

namespace OddsGridApp.ViewModels
{
    public class OddsGridViewModel : ViewModelBase
    {
        public OddsGridViewModel()
        {
            HandOdds = new ObservableCollection<HandOddsItemModel>();
            InitializeHandOddsList();
        }

        private const int WinSplitIndex = 9;
        
        private void InitializeHandOddsList()
        {
             HandOdds.Add(new HandOddsItemModel { Description = "High Card", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Pair", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Two Pair", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "3 of a Kind", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Straight", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Flush", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Full House", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "4 of a Kind", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Straight Flush", OpponentOdds = "0", PlayerOdds = "0" });
             HandOdds.Add(new HandOddsItemModel { Description = "Win / Split", OpponentOdds = "0", PlayerOdds = "0" });
        }
        
        public ObservableCollection<HandOddsItemModel> HandOdds { get; }
        
        private string FormatPercent(double v)
        {
            if (v != 0.0)
            {
                if (v * 100.0 >= 1.0)
                    return string.Format("{0:##0.0}%", v * 100.0);
                else
                    return "<1%";
            }
            return "n/a";
        }
        
        public void ReloadHandOdds(string pocketHand, string board)
        {
            if (string.IsNullOrEmpty(pocketHand.Trim()) || string.IsNullOrEmpty(board.Trim())) return;

            Task.Run(() => {
                int count = 0;
                double playerwins = 0.0;
                double opponentwins = 0.0;
                double[] player = new double[9];
                double[] opponent = new double[9];

                if (!Hand.ValidateHand(pocketHand + " " + board))
                {
                    return;
                }

                Hand.ParseHand(pocketHand + " " + board, ref count);

                // Don't allow these configurations because of calculation time.
                if (count == 0 || count == 1 || count == 3 || count == 4 || count > 7)
                {
                    return;
                }

                //Hand.HandPlayerOpponentOdds(Pocket, Board, ref player, ref opponent);
                Hand.HandPlayerMultiOpponentOdds(Hand.ParseHand(pocketHand), Hand.ParseHand(board), 1, 1, ref player, ref opponent);

                for (int i = 0; i < 9; i++)
                {
                    HandOdds[i].PlayerOdds = FormatPercent(player[i]);
                    HandOdds[i].OpponentOdds = FormatPercent(opponent[i]);
                    
                    playerwins += player[i] * 100.0;
                    opponentwins += opponent[i] * 100.0;
                }

                HandOdds[WinSplitIndex].PlayerOdds = string.Format("{0:##0.0}%", playerwins);
                HandOdds[WinSplitIndex].OpponentOdds = string.Format("{0:##0.0}%", opponentwins);
            });
            
        }
    }
}