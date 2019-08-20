using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Abstractions
{
    public interface IProductCallback
    {
        void AddToCart(ProductViewModel obj);
        void RemoveFromCart(ProductViewModel obj);
        void OnFavoriteChanged(ProductViewModel obj, bool favorite);
    }
}
