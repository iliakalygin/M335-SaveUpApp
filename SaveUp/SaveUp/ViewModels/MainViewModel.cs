using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SaveUp
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                if (SetProperty(ref items, value))
                {
                    items.CollectionChanged += (sender, args) => UpdateTotalSaved();
                    UpdateTotalSaved();
                }
            }
        }

        private decimal totalSaved;
        public decimal TotalSaved
        {
            get => totalSaved;
            set => SetProperty(ref totalSaved, value);
        }

        public ICommand NavigateToAddItemCommand { get; }
        public ICommand NavigateToListCommand { get; }

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>(ItemService.LoadItems());
            Items.CollectionChanged += (sender, args) => UpdateTotalSaved();

            NavigateToAddItemCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddItemPage)));
            NavigateToListCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(ItemsListPage)));

            UpdateTotalSaved();
        }

        public void UpdateItems()
        {
            Items = new ObservableCollection<Item>(ItemService.LoadItems());
            UpdateTotalSaved();
        }

        private void UpdateTotalSaved()
        {
            TotalSaved = Items.Sum(item => item.Price);
        }
    }
}
