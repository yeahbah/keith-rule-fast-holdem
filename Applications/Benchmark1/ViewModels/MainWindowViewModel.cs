using System.Diagnostics;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using HoldemHand;
using ReactiveUI;

namespace Benchmark.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            StartBenchmarkCommand = ReactiveCommand.Create(StartBenchmark);
        }
        
        private string _fiveCardHandIteratorResult = "N/A";
        private string _fiveCardHandInlinedResult  = "N/A";
        private string _sevenCardHandIteratorResult = "N/A";
        private string _sevenCardHandInlinedResult = "N/A";
        private string _fiveCardHandEvaluateIteratorResult = "N/A";
        private string _fiveCardHandEvaluateInlinedResult = "N/A";
        private string _sevenCardHandEvaluateIteratorResult = "N/A";
        private string _sevenCardHandEvaluateInlinedResult = "N/A";

        public string FiveCardHandIteratorResult
        {
            get => _fiveCardHandIteratorResult; 
            set => this.RaiseAndSetIfChanged(ref _fiveCardHandIteratorResult, value);
        }

        public string FiveCardHandInlinedResult
        {
            get => _fiveCardHandInlinedResult; 
            set => this.RaiseAndSetIfChanged(ref _fiveCardHandInlinedResult, value);
        }

        public string SevenCardHandIteratorResult
        {
            get => _sevenCardHandIteratorResult; 
            set => this.RaiseAndSetIfChanged(ref _sevenCardHandIteratorResult, value);
        }

        public string SevenCardHandInlinedResult
        {
            get => _sevenCardHandInlinedResult; 
            set => this.RaiseAndSetIfChanged(ref _sevenCardHandInlinedResult, value);
        }

        public string FiveCardHandEvaluateIteratorResult
        {
            get => _fiveCardHandEvaluateIteratorResult;
            set => this.RaiseAndSetIfChanged(ref _fiveCardHandEvaluateIteratorResult, value);
        }

        public string FiveCardHandEvaluateInlinedResult
        {
            get => _fiveCardHandEvaluateInlinedResult;
            set => this.RaiseAndSetIfChanged(ref _fiveCardHandEvaluateInlinedResult, value);
        }

        public string SevenCardHandEvaluateIteratorResult
        {
            get => _sevenCardHandEvaluateIteratorResult;
            set => this.RaiseAndSetIfChanged(ref _sevenCardHandEvaluateIteratorResult, value);
        }

        public string SevenCardHandEvaluateInlinedResult
        {
            get => _sevenCardHandEvaluateInlinedResult;
            set => this.RaiseAndSetIfChanged(ref _sevenCardHandEvaluateInlinedResult, value);
        }

        public ReactiveCommand<Unit, Unit> StartBenchmarkCommand { get; }

        private void StartBenchmark()
        {
            Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Thread.CurrentThread.IsBackground = true;
                RunFiveCardHandIteration();
                RunFiveCardHandInlinedIteration();
                
                RunSevenCardHandIteration();
                RunSevenCardInlinedIteration();
                
                RunFiveCardEvaluateIteration();
                RunFiveCardEvaluateInlinedIteration();

                RunSevenCardEvaluateIteration();
                RunSevenCardEvaluateInlinedIteration();
            });
        }

        private void RunSevenCardEvaluateInlinedIteration()
        {
            SevenCardHandEvaluateInlinedResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var count = SevenCardEvaluateInlinedBenchmark();
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            SevenCardHandEvaluateInlinedResult = output;
        }

        private void RunSevenCardEvaluateIteration()
        {
            SevenCardHandEvaluateIteratorResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var count = SevenCardEvaluateBenchmark();
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            SevenCardHandEvaluateIteratorResult = output;
        }

        private void RunFiveCardEvaluateInlinedIteration()
        {
            FiveCardHandEvaluateInlinedResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var count = FiveCardEvaluateBenchmarkInline();
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            FiveCardHandEvaluateInlinedResult = output;
        }

        private void RunFiveCardEvaluateIteration()
        {
            FiveCardHandEvaluateIteratorResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var count = FiveCardEvaluateBenchmark();
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            FiveCardHandEvaluateIteratorResult = output;
        }

  //       public void TestThread()
		// {
  //           var lockValue = new object();
		// 	lock (lockValue)
		// 	{
		// 		HiPerfTimer timer = new HiPerfTimer();
		// 		string output = "";
  //
  //               timer.Start();
  //               long count1 = ValidateEnumerate5();
  //               double duration1 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count1 / duration1);
  //               FiveCardHandIteratorResult = output;
  //
  //               timer.Start();
  //               long count2 = InlineValidateEnumerate5();
  //               double duration2 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count2 / duration2);
  //               FiveCardHandInlinedResult = output;
  //
  //               timer.Start();
  //               long count3 = ValidateEnumerate7();
  //               double duration3 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count3 / duration3);
  //               SevenCardHandIteratorResult = output;
  //
  //               timer.Start();
  //               long count4 = InlineValidateEnumerate7();
  //               double duration4 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count4 / duration4);
  //               SevenCardHandInlinedResult = output;
  //
  //               timer.Start();
  //               count4 = FiveCardEvaluateBenchmark();
  //               duration4 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count4 / duration4);
  //               FiveCardHandEvaluateIteratorResult = output;
  //
  //               timer.Start();
  //               count4 = FiveCardEvaluateBenchmarkInline();
  //               duration4 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count4 / duration4);
  //               FiveCardHandEvaluateInlinedResult = output;
  //
  //               timer.Start();
  //               count4 = SevenCardEvaluateBenchmark();
  //               duration4 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count4 / duration4);
  //               SevenCardHandEvaluateIteratorResult = output;
  //
  //               timer.Start();
  //               count4 = SevenCardEvaluateInlinedBenchmark();
  //               duration4 = timer.Duration;
  //               output = string.Format("{0:###,###,###,###} hands per second", count4 / duration4);
  //               SevenCardHandEvaluateInlinedResult = output;
  //           }
		// }

        private long SevenCardEvaluateInlinedBenchmark()
        {
            int _i1, _i2, _i3, _i4, _i5, _i6, _i7;
            ulong _card1, _n2, _n3, _n4, _n5, _n6;
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;
            uint handval;

            // Iterate through all possible 7 card hands
            for (_i1 = Hand.NumberOfCards - 1; _i1 >= 0; _i1--)
            {
                _card1 = Hand.CardMasksTable[_i1];
                for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                {
                    _n2 = _card1 | Hand.CardMasksTable[_i2];
                    for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                    {
                        _n3 = _n2 | Hand.CardMasksTable[_i3];
                        for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                        {
                            _n4 = _n3 | Hand.CardMasksTable[_i4];
                            for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                            {
                                _n5 = _n4 | Hand.CardMasksTable[_i5];
                                for (_i6 = _i5 - 1; _i6 >= 0; _i6--)
                                {
                                    _n6 = _n5 | Hand.CardMasksTable[_i6];
                                    for (_i7 = _i6 - 1; _i7 >= 0; _i7--)
                                    {
                                        handval = Hand.Evaluate(_n6 | Hand.CardMasksTable[_i7], 7);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        private long SevenCardEvaluateBenchmark()
        {
            long count = 0;
            uint handval = 0U;
            foreach (ulong handmask in Hand.Hands(7))
            {
                handval = Hand.Evaluate(handmask);
                count++;
            }
            return count;
        }

        private long FiveCardEvaluateBenchmarkInline()
        {
            int _i1, _i2, _i3, _i4, _i5;
            ulong _card1, _n2, _n3, _n4;
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;
            uint handval;

            // Iterate through all possible 5 card hands.
            for (_i1 = Hand.NumberOfCards - 1; _i1 >= 0; _i1--)
            {
                _card1 = Hand.CardMasksTable[_i1];
                for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                {
                    _n2 = _card1 | Hand.CardMasksTable[_i2];
                    for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                    {
                        _n3 = _n2 | Hand.CardMasksTable[_i3];
                        for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                        {
                            _n4 = _n3 | Hand.CardMasksTable[_i4];
                            for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                            {
                                handval = Hand.Evaluate(_n4 | Hand.CardMasksTable[_i5], 5);
                                count++;
                            }
                        }
                    }
                }
            }

            return count;
        }

        private long FiveCardEvaluateBenchmark()
        {
            long count = 0;
            uint handval = 0U;
            foreach (ulong handmask in Hand.Hands(5))
            {
                handval = Hand.Evaluate(handmask, 5);
                count++;
            }
            return count;
        }

        private void RunSevenCardInlinedIteration()
        {
            SevenCardHandInlinedResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            // HiPerfTimer timer = new HiPerfTimer();
            // timer.Start();
            var count4 = InlineValidateEnumerate7();
            stopWatch.Stop();
            //var duration = timer.Duration;
            var output = string.Format("{0:###,###,###,###} hands per second", count4 / stopWatch.Elapsed.TotalSeconds);
            SevenCardHandInlinedResult = output;
        }

        private void RunSevenCardHandIteration()
        {
            SevenCardHandIteratorResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            // HiPerfTimer timer = new HiPerfTimer();
            // timer.Start();
            var count3 = ValidateEnumerate7();
            //var duration = timer.Duration;
            stopWatch.Stop();
                
            var output = string.Format("{0:###,###,###,###} hands per second", count3 / stopWatch.Elapsed.TotalSeconds);
            SevenCardHandIteratorResult = output;
        }


        private void RunFiveCardHandIteration()
        {
            FiveCardHandIteratorResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var count = ValidateEnumerate5();
            //double duration1 = timer.Duration;
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            
            FiveCardHandIteratorResult = output;
        }

        private void RunFiveCardHandInlinedIteration()
        {
            FiveCardHandInlinedResult = "working...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            //HiPerfTimer timer = new HiPerfTimer();
            //timer.Start();
            var count = InlineValidateEnumerate5();
            //var duration = timer.Duration;
            stopWatch.Stop();
            var output = string.Format("{0:###,###,###,###} hands per second", count / stopWatch.Elapsed.TotalSeconds);
            
            FiveCardHandInlinedResult = output;
        }

        /// <summary>
        /// Equivalent to ValidateEnumerate7 with much of the code manually inlined.
        /// Surprisingly this makes about a significant difference is speed.
        /// </summary>
        /// <summary>
        /// Equivent to ValidateEnumerate7 with much of the code manually inlined.
        /// Surprisingly this makes about a significant difference is speed.
        /// </summary>
        public static long InlineValidateEnumerate7()
        {
            int _i1, _i2, _i3, _i4, _i5, _i6, _i7;
            ulong _card1, _n2, _n3, _n4, _n5, _n6;
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;

            // Iterate through all possible 7 card hands
            for (_i1 = Hand.NumberOfCards - 1; _i1 >= 0; _i1--)
            {
                _card1 = Hand.CardMasksTable[_i1];
                for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                {
                    _n2 = _card1 | Hand.CardMasksTable[_i2];
                    for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                    {
                        _n3 = _n2 | Hand.CardMasksTable[_i3];
                        for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                        {
                            _n4 = _n3 | Hand.CardMasksTable[_i4];
                            for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                            {
                                _n5 = _n4 | Hand.CardMasksTable[_i5];
                                for (_i6 = _i5 - 1; _i6 >= 0; _i6--)
                                {
                                    _n6 = _n5 | Hand.CardMasksTable[_i6];
                                    for (_i7 = _i6 - 1; _i7 >= 0; _i7--)
                                    {
                                        handtypes[(int)Hand.EvaluateType(_n6 | Hand.CardMasksTable[_i7], 7)]++;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Validate results
            Debug.Assert(handtypes[(int)Hand.HandTypes.HighCard] == 23294460 &&
                handtypes[(int)Hand.HandTypes.Pair] == 58627800 &&
                handtypes[(int)Hand.HandTypes.TwoPair] == 31433400 &&
                handtypes[(int)Hand.HandTypes.Trips] == 6461620 &&
                handtypes[(int)Hand.HandTypes.Straight] == 6180020 &&
                handtypes[(int)Hand.HandTypes.Flush] == 4047644 &&
                handtypes[(int)Hand.HandTypes.FullHouse] == 3473184 &&
                handtypes[(int)Hand.HandTypes.FourOfAKind] == 224848 &&
                handtypes[(int)Hand.HandTypes.StraightFlush] == 41584 &&
                count == 133784560);

            return count;
        }

        /// <summary>
        /// This method iterates through all possible 7 card hands and validates the
        /// result with known hand distributions. This is used to validate the hand evaluation
        /// functions.
        /// </summary>
        /// <returns>count of hands evaluated</returns>
        public static long ValidateEnumerate7()
        {
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;

            // Iterate through all possible 7 card hands
            foreach (ulong mask in Hand.Hands(7))
            {
                handtypes[(int)Hand.EvaluateType(mask, 7)]++;
                count++;
            }

            // Validate results.
            Debug.Assert(handtypes[(int)Hand.HandTypes.HighCard] == 23294460 &&
                         handtypes[(int)Hand.HandTypes.Pair] == 58627800 &&
                         handtypes[(int)Hand.HandTypes.TwoPair] == 31433400 &&
                         handtypes[(int)Hand.HandTypes.Trips] == 6461620 &&
                         handtypes[(int)Hand.HandTypes.Straight] == 6180020 &&
                         handtypes[(int)Hand.HandTypes.Flush] == 4047644 &&
                         handtypes[(int)Hand.HandTypes.FullHouse] == 3473184 &&
                         handtypes[(int)Hand.HandTypes.FourOfAKind] == 224848 &&
                         handtypes[(int)Hand.HandTypes.StraightFlush] == 41584 &&
                         count == 133784560);
            return count;
        }
        
        /// <summary>
        /// This method iterates through all possible 5 card hands and validates the
        /// result with known hand distributions. This is used to validate the hand evaluation
        /// functions.
        /// </summary>
        /// <returns>count of hands evaluated</returns>
        private static long ValidateEnumerate5()
        {
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;

            // Iterate through all possible 5 card hands
            foreach (ulong mask in Hand.Hands(5))
            {
                handtypes[(int)Hand.EvaluateType(mask, 5)]++;
                count++;
            }

            // Validate results.
            Debug.Assert(handtypes[(int)Hand.HandTypes.HighCard] == 1302540);
            Debug.Assert(handtypes[(int)Hand.HandTypes.Pair] == 1098240);
            Debug.Assert(handtypes[(int)Hand.HandTypes.TwoPair] == 123552);
            Debug.Assert(handtypes[(int)Hand.HandTypes.Trips] == 54912);
            Debug.Assert(handtypes[(int)Hand.HandTypes.Straight] == 10200);
            Debug.Assert(handtypes[(int)Hand.HandTypes.Flush] == 5108);
            Debug.Assert(handtypes[(int)Hand.HandTypes.FullHouse] == 3744);
            Debug.Assert(handtypes[(int)Hand.HandTypes.FourOfAKind] == 624);
            Debug.Assert(handtypes[(int)Hand.HandTypes.StraightFlush] == 40);
            Debug.Assert(count == 2598960);
            return count;
        }
        
        /// <summary>
        /// Equivalent to ValidateEnumerate5 with much of the code manually inlined.
        /// Surprisingly this makes about a significant difference is speed. 
        /// </summary>
        /// <returns>count of hands evaluated</returns>
        public static long InlineValidateEnumerate5()
        {
            int _i1, _i2, _i3, _i4, _i5;
            ulong _card1, _n2, _n3, _n4, _n5;
            int[] handtypes = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int count = 0;

            // Iterate through all possible 5 card hands.
            for (_i1 = Hand.NumberOfCards - 1; _i1 >= 0; _i1--)
            {
                _card1 = Hand.CardMasksTable[_i1];
                for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                {
                    _n2 = _card1 | Hand.CardMasksTable[_i2];
                    for (_i3 = _i2 - 1; _i3 >= 0; _i3--)
                    {
                        _n3 = _n2 | Hand.CardMasksTable[_i3];
                        for (_i4 = _i3 - 1; _i4 >= 0; _i4--)
                        {
                            _n4 = _n3 | Hand.CardMasksTable[_i4];
                            for (_i5 = _i4 - 1; _i5 >= 0; _i5--)
                            {
                                _n5 = _n4 | Hand.CardMasksTable[_i5];
                                handtypes[(int)Hand.EvaluateType(_n5, 5)]++;
                                count++;
                            }
                        }
                    }
                }
            }

            // Validate Results
            Debug.Assert(handtypes[(int)Hand.HandTypes.HighCard] == 1302540 &&
                handtypes[(int)Hand.HandTypes.Pair] == 1098240 &&
                handtypes[(int)Hand.HandTypes.TwoPair] == 123552 &&
                handtypes[(int)Hand.HandTypes.Trips] == 54912 &&
                handtypes[(int)Hand.HandTypes.Straight] == 10200 &&
                handtypes[(int)Hand.HandTypes.Flush] == 5108 &&
                handtypes[(int)Hand.HandTypes.FullHouse] == 3744 &&
                handtypes[(int)Hand.HandTypes.FourOfAKind] == 624 &&
                handtypes[(int)Hand.HandTypes.StraightFlush] == 40 &&
                count == 2598960);
            return count;
        }
    }
}