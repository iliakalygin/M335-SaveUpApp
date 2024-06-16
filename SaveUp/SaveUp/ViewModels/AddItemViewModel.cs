using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SaveUp
{
    public class AddItemViewModel : BaseViewModel
    {
        private string description;
        private string price;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public ICommand SaveItemCommand { get; }

        public AddItemViewModel()
        {
            SaveItemCommand = new Command(OnSaveItem);
        }

        private async void OnSaveItem()
        {
            if (decimal.TryParse(price, out decimal priceValue))
            {
                var newItem = new Item
                {
                    Description = description,
                    Price = priceValue,
                    Date = DateTime.Now
                };

                // Save item to a persistent storage (JSON)
                await ItemService.SaveItemAsync(newItem);

                // Access MainViewModel and update items
                if (Application.Current.MainPage.BindingContext is MainViewModel mainViewModel)
                {
                    mainViewModel.Items.Add(newItem);
                }

                await Shell.Current.GoToAsync("..");
            }
            else
            {
                // Handle invalid price input
                await Application.Current.MainPage.DisplayAlert("Fehler", "Ungültiger Preis", "OK");
            }
        }
    }
}
