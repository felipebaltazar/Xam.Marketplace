using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Mappings.Category
{
    public sealed class FromDtoToViewModel : Mapping<CategoryDTO, CategoryViewModel>
    {
        protected override Expression<Func<CategoryDTO, CategoryViewModel>> BuildProjection()
        {
            return c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            };
        }
    }
}
