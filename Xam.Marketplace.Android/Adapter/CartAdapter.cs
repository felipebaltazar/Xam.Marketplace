using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using Xam.Marketplace.Android.ViewHolder;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.ViewModel;
using AndroidViewHolder = Android.Support.V7.Widget.RecyclerView.ViewHolder;

namespace Xam.Marketplace.Android.Adapter
{
    public sealed class CartAdapter : BaseAdapter<ProductViewModel>
    {
        #region Fields

        private const int TYPE_FOOTER = 1;
        private readonly CartViewModel _cartViewModel;

        #endregion

        #region Properties

        public override int ItemCount => base.ItemCount + 1;

        #endregion

        #region Constructors

        public CartAdapter(Context context, CartViewModel cartViewModel, ObservableRangeCollection<ProductViewModel> products)
            : base(context, products)
        {
            _cartViewModel = cartViewModel;
        }

        public CartAdapter(Context context, CartViewModel cartViewModel)
            : base(context)
        {
            _cartViewModel = cartViewModel;
        }

        #endregion

        #region Override Methods

        public override int GetItemViewType(int position)
        {
            if (position == ItemSource.Count)
                return TYPE_FOOTER;

            return base.GetItemViewType(position);
        }

        public override AndroidViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == TYPE_FOOTER)
            {
                var totalView = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.total_cart, parent, false);

                return new TotalViewHolder(totalView);
            }

            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.cart_product_row, parent, false);

            return new CartViewHolder(itemView, this);
        }

        public override void OnBindViewHolder(AndroidViewHolder holder, int position)
        {
            if (holder is CartViewHolder cartHolder)
            {
                var product = _itemSource.ElementAt(position);
                cartHolder.UpdateViews(product);
            }
            else if (holder is TotalViewHolder totalViewHolder)
            {
                totalViewHolder.UpdateViews(_cartViewModel);
            }
        }

        public void UpdateCollection(IEnumerable<ProductViewModel> products)
        {
            _itemSource.Clear();
            _itemSource.AddRange(products);
        }

        #endregion
    }
}