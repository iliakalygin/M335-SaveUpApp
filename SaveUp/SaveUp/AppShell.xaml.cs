using Microsoft.Maui.Controls;

namespace SaveUp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
            Routing.RegisterRoute(nameof(ItemsListPage), typeof(ItemsListPage));
        }
    }
}
