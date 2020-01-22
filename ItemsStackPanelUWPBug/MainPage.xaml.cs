using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace ItemsStackPanelUWPBug
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<string>();
            for (int i = 0; i < 30000; i++)//Increase length here
            {
                Items.Add($"Item 1");
            }
        }
        public ObservableCollection<string> Items { get; }
    }
}

