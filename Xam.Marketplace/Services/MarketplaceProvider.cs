using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xam.Marketplace.Abstractions;
using Xam.Marketplace.DAO;
using Xam.Marketplace.Extensions;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.Mappings;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Services
{
    public sealed class MarketplaceProvider
    {
        #region Fields

        private const string BASE_URL = "https://pastebin.com/raw";
        private readonly IMarketplaceApi _marketplaceApi;
        private readonly FavoriteDAO _favoriteDAO;

        #endregion

        #region Constructors

        public MarketplaceProvider(FavoriteDAO favoriteDAO)
        {
            _favoriteDAO = favoriteDAO;
            _marketplaceApi = RestService.For<IMarketplaceApi>(BASE_URL);
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(IProductCallback productCallback = null)
        {
            var productsDto = await _marketplaceApi.GetProductsAsync();
            var favorites = await _favoriteDAO.GetAllAsync();
            var viewModelCollection = productsDto.Select(p =>
                ProductMappings.FromDtoToViewModel.ProjectWith((vm) => CustomProjection(vm, productCallback, favorites), p));

            return viewModelCollection;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync(IProductCallback productCallback = null)
        {
            var categoryDtoCollection = await _marketplaceApi.GetCategoriesAsync();
            var categoryCollection = categoryDtoCollection.Select(c => CategoryMappings.FromDtoToViewModel.Project(c));
            var productColletion = await GetProductsAsync(productCallback);
            var offerDtoCollecion = await _marketplaceApi.GetOffersAsync();

            categoryCollection = categoryCollection.Foreach(c =>
                ComplementViewModel(c, productColletion, offerDtoCollecion));

            return categoryCollection;
        }

        #endregion

        #region Private Methods

        private static CategoryViewModel ComplementViewModel(CategoryViewModel viewModel, IEnumerable<ProductViewModel> productsDto, IEnumerable<OfferDTO> offerDtoCollection)
        {
            var offerDto = offerDtoCollection
                .FirstOrDefault(o => o.CategoryId == viewModel.Id);

            var categoryProducts = productsDto
                .Where(p => p.CategoryId == viewModel.Id)
                .Foreach(p => SetOffer(p, offerDto));

            viewModel.Products = new ObservableRangeCollection<ProductViewModel>(categoryProducts);
            return viewModel;
        }

        private static ProductViewModel SetOffer(ProductViewModel vm, OfferDTO offerDto)
        {
            vm.Offer = offerDto;
            return vm;
        }

        private static ProductViewModel CustomProjection(ProductViewModel vm,
            IProductCallback productCallback, IEnumerable<Favorite> favorites)
        {
            vm.IsFavorite = favorites.Any(f => f.Id == vm.Id);
            return vm.RegisterCallback(productCallback);
        }

        #endregion
    }
}
