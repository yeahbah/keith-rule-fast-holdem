using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;
using HoldemHand;

namespace HandEvaluatorTest
{
    public class UnitTest1
    {
        /// <summary>
        /// This function evaluates all possible 5 card poker hands and tallies the
        /// results. The results should come up with know values. If not there is either
        /// and error in the iterator function Hands() or the EvaluateType() function.
        /// </summary>
        [Fact]
        public void Test5CardHands()
        {
            int[] handtypes = [0, 0, 0, 0, 0, 0, 0, 0, 0];
            var count = 0;

            // Iterate through all possible 5 card hands
            foreach (var mask in Hand.Hands(5))
            {
                handtypes[(int)Hand.EvaluateType(mask, 5)]++;
                count++;
            }

            // Validate results.
            Assert.True(1302540 == handtypes[(int)Hand.HandTypes.HighCard], "HighCard Returned Incorrect Count");
            Assert.True(1098240 == handtypes[(int)Hand.HandTypes.Pair], "Pair Returned Incorrect Count");
            Assert.True(123552 == handtypes[(int)Hand.HandTypes.TwoPair], "TwoPair Returned Incorrect Count");
            Assert.True(54912 == handtypes[(int)Hand.HandTypes.Trips], "Trips Returned Incorrect Count");
            Assert.True(10200 == handtypes[(int)Hand.HandTypes.Straight], "Trips Returned Incorrect Count");
            Assert.True(5108 == handtypes[(int)Hand.HandTypes.Flush], "Flush Returned Incorrect Count");
            Assert.True(3744 == handtypes[(int)Hand.HandTypes.FullHouse], "FullHouse Returned Incorrect Count");
            Assert.True(624 == handtypes[(int)Hand.HandTypes.FourOfAKind], "FourOfAKind Returned Incorrect Count");
            Assert.True(40 == handtypes[(int)Hand.HandTypes.StraightFlush], "StraightFlush Returned Incorrect Count");
            Assert.True(2598960 == count, "Count Returned Incorrect Value");
        }

        /// <summary>
        /// This function evaluates all possible 7 card poker hands and tallies the
        /// results. The results should come up with know values. If not there is either
        /// and error in the iterator function Hands() or the EvaluateType() function.
        /// </summary>
        [Fact]
        public void Test7CardHands()
        {
            int[] handtypes = [0, 0, 0, 0, 0, 0, 0, 0, 0];
            int count = 0;

            // Iterate through all possible 7 card hands
            foreach (var mask in Hand.Hands(7))
            {
                handtypes[(int)Hand.EvaluateType(mask, 7)]++;
                count++;
            }

            Assert.True(58627800 == handtypes[(int)Hand.HandTypes.Pair], "Pair Returned Incorrect Value");
            Assert.True(31433400 == handtypes[(int)Hand.HandTypes.TwoPair], "TwoPair Returned Incorrect Value");
            Assert.True(6461620 == handtypes[(int)Hand.HandTypes.Trips], "Trips Returned Incorrect Value");
            Assert.True(6180020 == handtypes[(int)Hand.HandTypes.Straight], "Straight Returned Incorrect Value");
            Assert.True(4047644 == handtypes[(int)Hand.HandTypes.Flush], "Flush Returned Incorrect Value");
            Assert.True(3473184 == handtypes[(int)Hand.HandTypes.FullHouse], "FullHouse Returned Incorrect Value");
            Assert.True(224848 == handtypes[(int)Hand.HandTypes.FourOfAKind], "FourOfAKind Returned Incorrect Value");
            Assert.True(41584 == handtypes[(int)Hand.HandTypes.StraightFlush], "StraightFlush Returned Incorrect Value");
            Assert.True(133784560 == count, "Count Returned Incorrect Value");

        }
        /// <summary>
        /// Tests the Parser and the ToString for masks.
        /// </summary>
        [Fact]
        public void TestParserWith5Cards()
        {
            var count = 0;

            for (var i = 0; i < 52; i++)
            {
                Assert.True(Hand.ParseCard(Hand.CardTable[i]) == i, "Make sure parser and text match");
            }

            foreach (var mask in Hand.Hands(5))
            {
                var hand = Hand.MaskToString(mask);
                var testmask = Hand.ParseHand(hand);
                Assert.True(Hand.BitCount(testmask) == 5, "Parsed Results should be 5 cards");
 
                //System.Diagnostics.Debug.Assert(mask == testmask);

                Assert.True(mask == testmask, "Make sure that MaskToString() and ParseHand() return consistent results");
                count++;
            }
        }

        /// <summary>
        /// Tests the Parser and the ToString for masks.
        /// </summary>
        [Fact]
        public void TestParserWith7Cards()
        {
            foreach (var mask in Hand.RandomHands(7, 20.0))
            {
                var hand = Hand.MaskToString(mask);
                var testmask = Hand.ParseHand(hand);
                Assert.True(Hand.BitCount(testmask) == 7, "Parsed Results should be 7 cards");
                Assert.True(mask == testmask, "Make sure that MaskToString() and ParseHand() return consistant results");
            }
        }


        /// <summary>
        /// C# Interop call to Win32 QueryPerformanceCount. This functioQueryPerformanceCountern should be removed
        /// if you need an interop free class definition.
        /// </summary>
        /// <param name="lpPerformanceCount">returns performance counter</param>
        /// <returns>True if successful, false otherwise</returns>
        // [DllImport("Kernel32.dll")]
        // private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        /// <summary>
        /// C# Interop call to Win32 QueryPerformanceFrequency. This function should be removed
        /// if you need an interop free class definition.
        /// </summary>
        /// <param name="lpFrequency">returns performance frequence</param>
        /// <returns>True if successful, false otherwise</returns>
        // [DllImport("Kernel32.dll")]
        // private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestRandomIterators()
        {
            long freq;
            var count = Hand.RandomHands(7, 20000).Count();
            // Test Random Hand Trials Iteration
            Assert.True(count == 20000, "Should match the requested number of hands");
            
            freq = Stopwatch.Frequency;
            var sw = Stopwatch.StartNew();
            count = Hand.RandomHands(7, 2.5).Count();
            sw.Stop();
            Assert.True((sw.ElapsedTicks/ ((double)freq)) > 2.5, "Make sure ran the correct amount of time");
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestSuitConsistancy()
        {
            ulong mask = Hand.ParseHand("Ac Tc 2c 3c 4c");
            uint sc = (uint)((mask >> (Hand.CLUB_OFFSET)) & 0x1fffUL);
            Assert.True(Hand.BitCount(sc) == 5, "Club consistancy check");

            mask = Hand.ParseHand("Ad Td 2d 3d 4d");
            uint sd = (uint)((mask >> (Hand.DIAMOND_OFFSET)) & 0x1fffUL);
            Assert.True(Hand.BitCount(sd) == 5, "Diamond consistancy check");

            mask = Hand.ParseHand("Ah Th 2h 3h 4h");
            uint sh = (uint)((mask >> (Hand.HEART_OFFSET)) & 0x1fffUL);
            Assert.True(Hand.BitCount(sh) == 5, "Hearts consistancy check");

            mask = Hand.ParseHand("As Ts 2s 3s 4s");
            uint ss = (uint)((mask >> (Hand.SPADE_OFFSET)) & 0x1fffUL);
            Assert.True(Hand.BitCount(ss) == 5, "Spade consistancy check");
        }

      
       
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestBasicIterators()
        {
            long count = 0;
            foreach (ulong mask in Hand.Hands(1))
                count++;
            Assert.True(count == 52, "Check one card hand count");

            count = 0;
            foreach (ulong mask in Hand.Hands(2))
                count++;
            Assert.True(count == 1326, "Check two card hand count");

            count = 0;
            foreach (ulong mask in Hand.Hands(3))
                count++;
            Assert.True(count == 22100, "Check three card hand count");

            count = 0;
            foreach (ulong mask in Hand.Hands(4))
                count++;
            Assert.True(count == 270725, "Check four card hand count");

            count = 0;
            foreach (ulong mask in Hand.Hands(5))
                count++;
            Assert.True(count == 2598960, "Check five card hand count");

            count = 0;
            foreach (ulong mask in Hand.Hands(6))
                count++;
            Assert.True(count == 20358520, "Check six card hand count");
        }

        [Fact]
        public void TestAnalysis()
        {
            // The outs are: Aces (1), Queens (4), Kinds (3), Jacks (3), Tens (3), Spades (9)
            int outs = Hand.Outs(Hand.ParseHand("As Ks"), Hand.ParseHand("Js Ts Ad"));
            Assert.True(outs == 23, "Check the number of outs (23)");

            // The only outs are the remaining spades, but not the 5 of spades (7)
            outs = Hand.Outs(Hand.ParseHand("As Kd"), Hand.ParseHand("2s 3s 4s"), Hand.ParseHand("6s 5d"));
            Assert.True(outs == 7, "Check the number of outs (7)");

            // The outs are the remaining spades, aces, and kings (15)
            outs = Hand.Outs(Hand.ParseHand("As Ks"), Hand.ParseHand("2s 3s 4d"), Hand.ParseHand("2d 6c"));
            Assert.True(outs == 15, "Check the number of outs (15)");

            //
            // foreach (ulong mask in Hand.Hands(2))
            // {
            //     double sum = 0;
            //     double[] player = new double[9];
            //     double[] opponent = new double[9];
            //
            //     //Hand.HandPlayerOpponentOdds(mask, 0UL, ref player, ref opponent);
            //     Hand.HandPlayerMultiOpponentOdds(mask, 0UL, 9, 1, ref player, ref opponent);
            //
            //     Assert.True(player.Length == opponent.Length, "Player & Opponent Length must be equal");
            //     Assert.True(player.Length == 9, "Player length must equal 9");
            //     Assert.True(opponent.Length == 9, "Opponent length must equal 9");
            //
            //     for (int i = 0; i < 9; i++)
            //     {
            //         sum += player[i];
            //         sum += opponent[i];
            //     }
            //
            //     Assert.True(Math.Abs(sum - 1.0) < 0.00001, "Sum must be equal to 1.0 +/- some fudge");
            // }

            //Check a set of random three card boards
            // foreach (ulong mask in Hand.RandomHands(2, 25))
            // {
            //     foreach (ulong board in Hand.RandomHands(3, 0.05))
            //     {
            //         double sum = 0;
            //         double[] player = new double[9];
            //         double[] opponent = new double[9];
            //         Hand.HandPlayerOpponentOdds(mask, board, ref player, ref opponent);
            //
            //         Assert.True(player.Length == opponent.Length, "Player & Opponent Length must be equal");
            //         Assert.True(player.Length == 9, "Player length must equal 9");
            //         Assert.True(opponent.Length == 9, "Opponent length must equal 9");
            //
            //         for (int i = 0; i < 9; i++)
            //         {
            //             sum += player[i];
            //             sum += opponent[i];
            //         }
            //
            //         Assert.True(Math.Abs(sum - 1.0) < 0.00001, "Sum must be equal to 1.0 +/- some fudge");
            //     }
            // }

            //Check a set of random four card boards
            // foreach (ulong mask in Hand.RandomHands(2, 100))
            // {
            //     foreach (ulong board in Hand.RandomHands(4, 0.01))
            //     {
            //         double sum = 0;
            //         double[] player = new double[9];
            //         double[] opponent = new double[9];
            //         Hand.HandPlayerOpponentOdds(mask, board, ref player, ref opponent);
            //
            //         Assert.True(player.Length == opponent.Length, "Player & Opponent Length must be equal");
            //         Assert.True(player.Length == 9, "Player length must equal 9");
            //         Assert.True(opponent.Length == 9, "Opponent length must equal 9");
            //
            //         for (int i = 0; i < 9; i++)
            //         {
            //             sum += player[i];
            //             sum += opponent[i];
            //         }
            //
            //         Assert.True(Math.Abs(sum - 1.0) < 0.00001, "Sum must be equal to 1.0 +/- some fudge");
            //     }
            // }

            //Check a set of random four card boards
            // foreach (ulong mask in Hand.RandomHands(2, 100))
            // {
            //     foreach (ulong board in Hand.RandomHands(5, 0.1))
            //     {
            //         double sum = 0;
            //         double[] player = new double[9];
            //         double[] opponent = new double[9];
            //         Hand.HandPlayerOpponentOdds(mask, board, ref player, ref opponent);
            //
            //         Assert.True(player.Length == opponent.Length, "Player & Opponent Length must be equal");
            //         Assert.True(player.Length == 9, "Player length must equal 9");
            //         Assert.True(opponent.Length == 9, "Opponent length must equal 9");
            //
            //         for (int i = 0; i < 9; i++)
            //         {
            //             sum += player[i];
            //             sum += opponent[i];
            //         }
            //
            //         Assert.True(Math.Abs(sum - 1.0) < 0.00001, "Sum must be equal to 1.0 +/- some fudge");
            //     }
            // }
                
        }

        [Fact]
        public void TestInstanceOperators()
        {
            foreach (ulong pocketmask in Hand.Hands(2))
            {
                string pocket = Hand.MaskToString(pocketmask);
                foreach (ulong boardmask in Hand.RandomHands(0UL, pocketmask, 5, 0.001))
                {
                    string board = Hand.MaskToString(boardmask);
                    Hand hand1 = new Hand(pocket, board);
                    Hand hand2 = new Hand();
                    hand2.PocketMask = pocketmask;
                    hand2.BoardMask = boardmask;
                    Assert.True(hand1 == hand2, "Equality test failed");
                }
            }
        }

        [Fact]
        public void TestMoreInstanceOperators()
        {
            string board = "2d kh qh 3h qc";
            // Create a hand with AKs plus board
            Hand h1 = new Hand("ad kd", board);
            // Create a hand with 23 unsuited plus board
            Hand h2 = new Hand("2h 3d", board);
       
            Assert.True(h1 > h2);
            Assert.True(h1 >= h2);
            Assert.True(h2 <= h1);
            Assert.True(h2 < h1);
            Assert.True(h1 != h2);
        }

        [Fact]
        public void TestEvaluate()
        {
        //    var hand = Hand.Evaluate("2s 3c 4s 5d 6h As Ks");
        //    var handType = Hand.HandType(hand);
        //    Assert.True(handType == (uint)Hand.HandTypes.Straight);

        //    hand = THand.Evaluate('2s 3c 4s 5d 6s As Ks');
        //    handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.Flush));

        //hand:= THand.Evaluate('2s 3c 8s 5d 7s Ac Jc');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.HighCard));

            var hand = Hand.Evaluate("2s 3c 8s 5d 7s Ac As");
            var handType = Hand.HandType(hand);
            Assert.True(handType == (uint)Hand.HandTypes.Pair);

        //hand:= THand.Evaluate('2s 3c 8s 7d 7s Ac As');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.TwoPair));

        //hand:= THand.Evaluate('2s 3c 8s 7d Ad Ac As');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.Trips));

        //hand:= THand.Evaluate('2s 3c 8s Ah Ad Ac As');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.FourOfAKind));

        //hand:= THand.Evaluate('2s 3c 7s 7h 7d Ac As');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.FullHouse));

        //hand:= THand.Evaluate('2s 3c Qs Ts Js As Ks');
        //handType:= THand.HandType(hand);
        //    Assert.IsTrue(handType = integer(THandTypes.StraightFlush));
        }
    }
}