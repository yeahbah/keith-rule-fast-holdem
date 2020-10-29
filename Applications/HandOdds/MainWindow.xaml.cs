using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using HoldemHand;

namespace HandOdds
{
    public class MainWindow : Window
    {
        private readonly TextBlock txtResult;
        private readonly TextBox txtPlayer1;
        private readonly TextBox txtPlayer2;
        private readonly TextBox txtPlayer3;
        private readonly TextBox txtPlayer4;
        private readonly TextBlock player1Pct;
        private readonly TextBlock player2Pct;
        private readonly TextBlock player3Pct;
        private readonly TextBlock player4Pct;
        private readonly TextBox txtBoard;
        private readonly TextBox txtDeadCards;

        private string[] PlayerText
        {
            get
            {
                var player = new string[4];
                player[0] = txtPlayer1.Text;
                player[1] = txtPlayer2.Text;
                player[2] = txtPlayer3.Text;
                player[3] = txtPlayer4.Text;
                return player;
            }
        }
        
        private void SetPlayerValue(int index, double value)
        {
            switch (index)
            {
                case 0:
                    player1Pct.Text = $"{value * 100.0:#0.0}%";
                    break;
                case 1:
                    player2Pct.Text = $"{value * 100.0:#0.0}%";
                    break;
                case 2:
                    player3Pct.Text = $"{value * 100.0:#0.0}%";
                    break;
                case 3:
                    player4Pct.Text = $"{value * 100.0:#0.0}%";
                    break;
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            txtBoard = this.FindControl<TextBox>("txtBoard");
            txtDeadCards = this.FindControl<TextBox>("txtDeadCards");
            txtResult = this.FindControl<TextBlock>("txtResult");
            txtResult.Text = string.Empty;
            txtPlayer1 = this.FindControl<TextBox>("txtPlayer1");
            txtPlayer2 = this.FindControl<TextBox>("txtPlayer2");
            txtPlayer3 = this.FindControl<TextBox>("txtPlayer3");
            txtPlayer4 = this.FindControl<TextBox>("txtPlayer4");
            player1Pct = this.FindControl<TextBlock>("player1Pct");
            player1Pct.Text = string.Empty;
            player2Pct = this.FindControl<TextBlock>("player2Pct");
            player2Pct.Text = string.Empty;
            player3Pct = this.FindControl<TextBlock>("player3Pct");
            player3Pct.Text = string.Empty;
            player4Pct = this.FindControl<TextBlock>("player4Pct");
            player4Pct.Text = string.Empty;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            //HiPerfTimer timer = new HiPerfTimer();
            player1Pct.Text = "";
            player2Pct.Text = "";
            player3Pct.Text = "";
            player4Pct.Text = "";
            txtResult.Text = "";
            
            var timer = new Stopwatch();
            int count = 0, index = 0;
            int[] playerIndex = { -1, -1, -1, -1 };
            int[] pocketIndex = { -1, -1, -1, -1 };

            foreach (string pocket in PlayerText)
            {
                if (pocket != "")
                {
                    playerIndex[count] = count;
                    pocketIndex[count++] = index;
                }
                index++;
            }

            string[] pocketCards = new string[count];

            for (int i = 0; i < count; i++)
            {
                pocketCards[i] = PlayerText[pocketIndex[i]];
            }

            long[] wins = new long[count];
            long[] losses = new long[count];
            long[] ties = new long[count];
            long totalhands = 0;

            try
            {
                timer.Start();
                Hand.HandWinOdds(pocketCards, txtBoard.Text, txtDeadCards.Text, wins, ties, losses, ref totalhands);
                //double duration = timer.Duration;
                timer.Stop();
                var duration = timer.Elapsed;

                Debug.Assert(totalhands != 0);

                if (totalhands != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        SetPlayerValue(pocketIndex[i], (((double)wins[i]) + ((double) ties[i])/2.0) / ((double)totalhands));
                    }
                }

                txtResult.Text = $"{totalhands:###,###,###,###} hands evaluated in {duration.TotalSeconds} seconds.";
                txtResult.Foreground = Brushes.Black;

            }
            catch (ArgumentException ae)
            {
                txtResult.Text = "Unable to process: " + ae.Message + ".";
                txtResult.Foreground = Brushes.Red;
            }
            catch (Exception ex)
            {
                txtResult.Text = $"Unable to process: {ex.Message}";
                txtResult.Foreground = Brushes.Red;
            }
        }
    }
}