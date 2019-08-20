using SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xam.Marketplace.Abstractions;
using Xam.Marketplace.DAO;
using Xam.Marketplace.Extensions;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.Model;
using Xam.Marketplace.Services;

namespace Xam.Marketplace.ViewModel
{
    public sealed class MainViewModel : BaseViewModel, IProductCallback
    {
        #region Fields

        private readonly FavoriteDAO _favoriteDAO;
        private readonly MarketplaceProvider _provider;
        private string purchaseButtonText;
        private bool purchaseButtonVisible;
        private CartViewModel cartViewModel;

        #endregion

        #region Properties

        public ObservableRangeCollection<CategoryViewModel> CurrentCategoryCollection
        {
            get;
            set;
        }

        public ObservableRangeCollection<CategoryViewModel> CompleteCategoryCollection
        {
            get;
            set;
        }

        public CartViewModel CartViewModel
        {
            get => cartViewModel;
            set => SetProperty(ref cartViewModel, value);
        }

        public string PurchaseButtonText
        {
            get => purchaseButtonText;
            set => SetProperty(ref purchaseButtonText, value);
        }

        public bool PurchaseButtonVisible
        {
            get => purchaseButtonVisible;
            set => SetProperty(ref purchaseButtonVisible, value);
        }

        #endregion

        #region Constructors

        public MainViewModel(SQLiteAsyncConnection connection)
        {
            _favoriteDAO = new FavoriteDAO(connection);
            _provider = new MarketplaceProvider(_favoriteDAO);
            CartViewModel = new CartViewModel();
            CurrentCategoryCollection = new ObservableRangeCollection<CategoryViewModel>();
            CompleteCategoryCollection = new ObservableRangeCollection<CategoryViewModel>();
            UpdatePurchaseButton();
        }

        #endregion

        #region Public Methods

        public async Task FillItemsAsync()
        {
            await ExecuteBusyActionAsync(async () =>
            {
                if (CurrentCategoryCollection?.Count > 0) return;

                var result = await _provider.GetCategoriesAsync(this);
                if (result != null)
                {
                    CurrentCategoryCollection.AddRange(result);
                    CompleteCategoryCollection.AddRange(result);
                }
            });
        }

        public void ApplyFilter(int? categoryId) {
            var currentCategory = CompleteCategoryCollection.FirstOrDefault(c => c.Id == categoryId);
            if (currentCategory != null)
            {
                CurrentCategoryCollection.Clear();
                CurrentCategoryCollection.Add(currentCategory);
            }
            else if(CurrentCategoryCollection.Count != CompleteCategoryCollection.Count)
            {
                CurrentCategoryCollection.Clear();
                CurrentCategoryCollection.AddRange(CompleteCategoryCollection);
            }
        }

        #endregion

        #region IProductCallback

        public void AddToCart(ProductViewModel obj)
        {
            CartViewModel.AddToCart(obj);
            UpdatePurchaseButton();
        }

        public void RemoveFromCart(ProductViewModel obj)
        {
            CartViewModel.RemoveFromCart(obj);
            UpdatePurchaseButton();
        }

        public void OnFavoriteChanged(ProductViewModel obj, bool isFavorite)
        {
            Task action = null;
            if (isFavorite)
            {
                var favoriteModel = new Favorite
                {
                    Id = obj.Id,
                    Name = obj.Name
                };

                action = _favoriteDAO.SaveAsync(favoriteModel);
            }
            else {
                action = _favoriteDAO.DeleteAsync(obj.Id);
            }

            ExecuteBusyActionAsync(()=> action)
                .SafeFireAndForget(onException: (ex) => Debug.WriteLine(ex.Message));
        }

        #endregion

        #region Private Methods

        private void UpdatePurchaseButton() =>
            PurchaseButtonText = $"Comprar ► R$ {(cartViewModel?.CalculateTotal() ?? 0.0):n2}";

        #endregion
    }
}
