using Android.Content;
using Android.Views;
using System.Linq;
using Xam.Marketplace.Android.ViewHolder;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.ViewModel;
using AndroidViewHolder = Android.Support.V7.Widget.RecyclerView.ViewHolder;

namespace Xam.Marketplace.Android.Adapter
{
    public sealed class CategoryAdapter
        : BaseAdapter<CategoryViewModel>
    {
        #region Constructors

        public CategoryAdapter(Context context, ObservableRangeCollection<CategoryViewModel> itemSource)
            : base(context, itemSource)
        { }

        #endregion

        #region Override Methods

        public override AndroidViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                        .Inflate(Resource.Layout.category_main, parent, false);

            return new CategoryViewHolder(itemView, this);
        }

        public override void OnBindViewHolder(AndroidViewHolder holder, int position)
        {
            if (position < 0) return;

            if (holder is CategoryViewHolder productHolder)
            {
                var category = _itemSource.ElementAt(position);
                productHolder.UpdateViews(category);
            }
        }

        #endregion
    }
}