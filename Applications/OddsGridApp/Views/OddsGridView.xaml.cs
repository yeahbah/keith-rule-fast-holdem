using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OddsGridApp.ViewModels;

namespace OddsGridApp.Views
{
    public class OddsGridView : UserControl
    {
        public OddsGridView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            //DataContext = new OddsGridViewModel();
        }
    }
}