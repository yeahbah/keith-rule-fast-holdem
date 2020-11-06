using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ReactiveUI;

namespace OddsGridApp.Models
{
    public class HandOddsItemModel : INotifyPropertyChanged
    {
        private string _description;
        private string _playerOdds;
        private string _opponentOdds;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string PlayerOdds
        {
            get => _playerOdds; 
            set
            {
                _playerOdds = value;
                OnPropertyChanged();
            }
        }

        public string OpponentOdds
        {
            get => _opponentOdds;
            set
            {
                _opponentOdds = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}