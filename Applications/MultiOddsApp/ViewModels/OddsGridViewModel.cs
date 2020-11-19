using System.Collections.ObjectModel;
using MultiOddsApp.Models;
using HoldemHand;
using System.Threading.Tasks;

namespace MultiOddsApp.ViewModels
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
        
        private string FormatPercent(double v, bool montecarlo)
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
        
        public void ReloadHandOdds(string pocketHand, string board, int numberOfOpponents)
        {
            if (string.IsNullOrEmpty(pocketHand.Trim()) || string.IsNullOrEmpty(board.Trim())) return;

            Task.Run(() => 
            {
                ResetResult();
                double playerwins = 0.0;
                double opponentwins = 0.0;
                double[] player = new double[9];
                double[] opponent = new double[9];
                ulong pocketmask = 0UL;
                int cards = 0;
                
                    
                pocketmask = Hand.ParseHand(pocketHand);
                cards = Hand.BitCount(pocketmask);
                                    
                if ((board != "" && !Hand.ValidateHand(board)) || cards != 2)
                {                
                    return;
                }
                    
                Hand.HandWinOdds(Hand.ParseHand(pocketHand), Hand.ParseHand(board), out player, out opponent, numberOfOpponents, 2.0);
                bool montecarlo = true;
    
                for (int i = 0; i < 9; i++)
                {
                    HandOdds[i].PlayerOdds = FormatPercent(player[i], montecarlo);
                    HandOdds[i].OpponentOdds = FormatPercent(opponent[i], montecarlo);
                    
                    playerwins += player[i] * 100.0;
                    opponentwins += opponent[i] * 100.0;
                }

                HandOdds[WinSplitIndex].PlayerOdds = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", playerwins);
                HandOdds[WinSplitIndex].OpponentOdds = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", opponentwins);
            });
        }

        public void ResetResult()
        {
            foreach(var odds in HandOdds)
            {
                odds.PlayerOdds = "0";
                odds.OpponentOdds = "0";
            }
        }
    }
}