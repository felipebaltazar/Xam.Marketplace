using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Xam.Marketplace.ViewModel
{
    public sealed class CartViewModel : BaseViewModel
    {
        private IList<ProductViewModel> products;

        public IReadOnlyCollection<ProductViewModel> Products
        {
            get => new ReadOnlyCollection<ProductViewModel>(products);
        }

        public CartViewModel()
        {
            products = new List<ProductViewModel>();
        }

        public void AddToCart(ProductViewModel productToAdd)
        {
            ExecuteBusyAction(() =>
            {
                var productInCart = products.FirstOrDefault(p => p.Id == productToAdd.Id);
                if (productInCart == null) {
                    productInCart = productToAdd;
                    products.Add(productToAdd);
                }

                productToAdd.Quantity++;
            });
        }

        public float CalculateTotal()
        {
            return Products.Sum(p => p.CalculeValue());
        }

        public int Count()
        {
            return Products.Sum(p => p.Quantity);
        }

        public void RemoveFromCart(ProductViewModel productToRemove)
        {
            ExecuteBusyAction(() =>
            {
                var productInCart = products.FirstOrDefault(p => p.Id == productToRemove.Id);
                if (productInCart == null) return;

                productInCart.Quantity--;
                if(productInCart.Quantity < 1)
                    products.Remove(productToRemove);
            });
        }
    }
}
