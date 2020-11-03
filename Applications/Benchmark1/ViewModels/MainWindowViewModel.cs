using System.Reactive;
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
            FiveCardHandIteratorResult = "Hello World";
        }
    }
}