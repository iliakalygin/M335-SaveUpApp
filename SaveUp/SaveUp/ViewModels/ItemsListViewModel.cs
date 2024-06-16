using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SaveUp
{
    public class ItemsListViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ICommand ClearAllCommand { get; }

        public ItemsListViewModel()
        {
            Items = new ObservableCollection<Item>(ItemService.LoadItems());
            ClearAllCommand = new Command(OnClearAll);
        }

        private void OnClearAll()
        {
            Items.Clear();
            ItemService.ClearAllItems();

            // Access MainViewModel and clear items
            if (Application.Current.MainPage.BindingContext is MainViewModel mainViewModel)
            {
                mainViewModel.Items.Clear();
            }
        }
    }
}
