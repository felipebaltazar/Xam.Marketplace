using Xam.Marketplace.Mappings.Category;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Mappings
{
    public static class CategoryMappings
    {
        public static Mapping<CategoryDTO, CategoryViewModel> FromDtoToViewModel = new FromDtoToViewModel();
    }
}
