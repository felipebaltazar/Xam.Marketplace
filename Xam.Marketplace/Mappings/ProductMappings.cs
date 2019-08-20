using Xam.Marketplace.Mappings.Product;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Mappings
{
    public static class ProductMappings
    {
        public static Mapping<ProductDTO, ProductViewModel> FromDtoToViewModel = new FromDtoToViewModel();
    }
}
