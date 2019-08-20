using Android.Views;
using Android.Widget;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Android.ViewHolder
{
    public class CartViewHolder : ProductViewHolder
    {
        #region Constructors

        public CartViewHolder(View view, CartAdapter adapter) : base(view, adapter)
        {
            NameView = view.FindViewById<TextView>(Resource.Id.cartProductName);
            PriceView = view.FindViewById<TextView>(Resource.Id.cartProductPrice);
            DiscountView = view.FindViewById<TextView>(Resource.Id.productDiscount);
            DiscountMain = view.FindViewById<LinearLayout>(Resource.Id.discountView);
            ThumbnailView = view.FindViewById<ImageView>(Resource.Id.cartProductPhoto);
            QuantityView = view.FindViewById<TextView>(Resource.Id.cartQuantityTextview);
        }

        protected override void ClearBindings()
        {
            Name = string.Empty;
            Price = 0f;
            Photo = string.Empty;
            Quantity = 0;
        }

        protected override void RegisterBindings(ProductViewModel product)
        {
            Name = product.Name;
            Price = product.Price;
            Photo = product.Photo;
            Quantity = product.Quantity;
            HasDiscount = product.HasDiscount;
            Discount = product.Discount;
        }

        #endregion
    }
}