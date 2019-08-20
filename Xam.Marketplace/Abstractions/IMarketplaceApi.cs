using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xam.Marketplace.Model;

namespace Xam.Marketplace.Abstractions
{
    public interface IMarketplaceApi
    {
        [Get("/YNR2rsWe")]
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();

        [Get("/eVqp7pfX")]
        Task<IEnumerable<ProductDTO>> GetProductsAsync();

        [Get("/R9cJFBtG")]
        Task<IEnumerable<OfferDTO>> GetOffersAsync();
    }
}
