using System;
using System.Linq.Expressions;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Mappings.Product
{
    public sealed class FromDtoToViewModel : Mapping<ProductDTO, ProductViewModel>
    {
        protected override Expression<Func<ProductDTO, ProductViewModel>> BuildProjection()
        {
            return p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Photo = p.Photo
            };
        }
    }
}
