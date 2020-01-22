using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ItemsStackPanelUWPBug
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<string>();
            for (int i = 0; i < 24000; i++)
            {
                Items.Add($"Item 1");
            }
        }

        public ObservableCollection<string> Items { get; }

    }
}

