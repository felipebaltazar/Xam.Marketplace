using Xam.Marketplace.Helpers;
using Xam.Marketplace.Model;

namespace Xam.Marketplace.ViewModel
{
    public sealed class CategoryViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private ObservableRangeCollection<ProductViewModel> products;

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ObservableRangeCollection<ProductViewModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }
    }
}
