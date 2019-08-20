using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using Xam.Marketplace.Android.ViewHolder;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.Model;
using Xam.Marketplace.ViewModel;
using AndroidViewHolder = Android.Support.V7.Widget.RecyclerView.ViewHolder;

namespace Xam.Marketplace.Android.Adapter
{
    public sealed class ProductAdapter : BaseAdapter<ProductViewModel>
    {
        #region Constructors

        public ProductAdapter(Context context, ObservableRangeCollection<ProductViewModel> products)
            : base(context, products)
        { }

        public ProductAdapter(Context context)
            : base(context)
        { }

        #endregion

        #region Override Methods

        public override AndroidViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.product_row, parent, false);

            return new CatalogViewHolder(itemView, this);
        }

        public override void OnBindViewHolder(AndroidViewHolder holder, int position)
        {
            if (holder is CatalogViewHolder productHolder)
            {
                var product = _itemSource.ElementAt(position);
                productHolder.UpdateViews(product);
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