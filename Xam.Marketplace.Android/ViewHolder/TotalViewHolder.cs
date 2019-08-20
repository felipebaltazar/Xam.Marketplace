using Android.Views;
using Android.Widget;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Android.ViewHolder
{
    public sealed class TotalViewHolder : BaseViewHolder
    {
        #region Properties

        public TextView TotalView
        {
            get;
            set;
        }

        public TextView TotalQuantity
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public TotalViewHolder(View view) : base(view)
        {
            TotalView = view.FindViewById<TextView>(Resource.Id.totalValueView);
            TotalQuantity = view.FindViewById<TextView>(Resource.Id.totalQuantityView);
        }

        #endregion

        #region Public Methods

        public void UpdateViews(CartViewModel cartViewModel)
        {
            TotalView.Text = $"R$ {cartViewModel.CalculateTotal():n2}";
            TotalQuantity.Text = $"{cartViewModel.Count()} UN";
        }

        #endregion
    }
}