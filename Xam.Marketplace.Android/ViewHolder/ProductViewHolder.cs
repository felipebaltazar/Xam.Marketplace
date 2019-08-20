using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using FFImageLoading;
using GalaSoft.MvvmLight.Helpers;
using System;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.ViewModel;
using static Android.Views.View;

namespace Xam.Marketplace.Android.ViewHolder
{
    public abstract class ProductViewHolder : BaseViewHolder
    {
        #region Fields

        protected const int ONE_WEEK = 7;

        protected readonly AlphaAnimation _buttonClick;
        protected readonly Adapter.BaseAdapter<ProductViewModel> _adapter;
        protected readonly View _parent;

        protected string name;
        protected string photo;
        protected float price;
        protected int quantity;
        protected ProductViewModel currentViewModel;
        private bool hasDiscount = true;
        private float discount = 0f;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, onChanged: OnNameChanged);
        }

        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value, onChanged: OnQuantityChanged);
        }

        public float Price
        {
            get => price;
            set => SetProperty(ref price, value, onChanged: OnPriceChanged);
        }

        public float Discount
        {
            get => discount;
            set => SetProperty(ref discount, value, onChanged: OnDiscountChanged);
        }

        public string Photo
        {
            get => photo;
            set => SetProperty(ref photo, value, onChanged: OnPhotoChanged);
        }

        public bool HasDiscount
        {
            get => hasDiscount;
            set => SetProperty(ref hasDiscount, value, onChanged: OnHasDiscountChanged);
        }

        public TextView NameView
        {
            get;
            protected set;
        }

        public TextView PriceView
        {
            get;
            protected set;
        }

        public TextView QuantityView
        {
            get;
            protected set;
        }

        public TextView DiscountView
        {
            get;
            protected set;
        }

        public LinearLayout DiscountMain
        {
            get;
            protected set;
        }

        public ImageView ThumbnailView
        {
            get;
            protected set;
        }

        #endregion

        #region Constructors

        protected ProductViewHolder(View view, Adapter.BaseAdapter<ProductViewModel> adapter) : base(view)
        {
            _parent = view;
            _adapter = adapter;
            _buttonClick = new AlphaAnimation(1F, 0.7F);
        }

        #endregion

        #region Public Methods

        public virtual void UpdateViews(ProductViewModel product)
        {
            currentViewModel = product;
            ClearBindings();
            RegisterBindings(product);
            OnQuantityChanged();
        }

        #endregion

        #region Private Methods

        protected abstract void ClearBindings();

        protected abstract void RegisterBindings(ProductViewModel product);

        protected void OnNameChanged() =>
            NameView.Text = Name;

        protected void OnQuantityChanged() =>
            QuantityView.Text = Quantity.ToString();

        protected void OnPriceChanged() =>
            PriceView.Text = $"R$ {Price:n2}";

        protected void OnPhotoChanged() =>
            ImageService.Instance
                .LoadUrl(Photo, TimeSpan.FromDays(ONE_WEEK))
                .Into(ThumbnailView);

        private void OnDiscountChanged() =>
            DiscountView.Text = $"{Discount}%";

        private void OnHasDiscountChanged() =>
            DiscountMain.Visibility = HasDiscount
            ? ViewStates.Visible : ViewStates.Invisible;

        #endregion
    }
}