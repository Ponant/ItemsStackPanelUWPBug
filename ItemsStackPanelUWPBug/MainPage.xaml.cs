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
            for (int i = 0; i < 1000; i++)
            {
                Items.Add($"Item 1");
            }
            CollectionViewSource = new CollectionViewSource();
            WorksCollectionViewSource = new CollectionViewSource();
        }

        private StorageFolder _folder;
        private QueryOptions _queryOptions;
        private StorageFileQueryResult _query;
        private FileInformationFactory _fileInformationFactory;

        public CollectionViewSource CollectionViewSource { get; set; }

        public CollectionViewSource WorksCollectionViewSource { get; set; }

        public ObservableCollection<string> Items { get; }

        private async void FolderPickerButton_Click(object sender, RoutedEventArgs e)
        {
            var _pickedFolder = await PickFolderAsync();
            if (_pickedFolder == null)
            {
                return;
            }

            _folder = _pickedFolder;
            _queryOptions = new QueryOptions
            {
                FolderDepth = FolderDepth.Deep,
                IndexerOption = IndexerOption.UseIndexerWhenAvailable,
            };

            _query = _folder.CreateFileQueryWithOptions(_queryOptions);

            _fileInformationFactory = new FileInformationFactory(_query, ThumbnailMode.SingleItem, 160,
                ThumbnailOptions.UseCurrentScale, delayLoad: false);

            var _vector = _fileInformationFactory.GetVirtualizedFilesVector();

            CollectionViewSource.Source = _vector;

            WorksCollectionViewSource.Source = Items;
        }

        private static async Task<StorageFolder> PickFolderAsync()
        {
            var folderPicker = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop,
                ViewMode = PickerViewMode.Thumbnail
            };

            folderPicker.FileTypeFilter.Add("*");

            var _pickedFolder = await folderPicker.PickSingleFolderAsync();
            return _pickedFolder;
        }

    }
}

