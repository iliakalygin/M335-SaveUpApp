namespace SaveUp
{
    public partial class ItemsListPage : ContentPage
    {
        public ItemsListPage()
        {
            InitializeComponent();
            BindingContext = new ItemsListViewModel();
        }
    }
}
