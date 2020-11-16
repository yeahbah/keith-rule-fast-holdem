using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MultiOddsApp.Views
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
        }
    }
}