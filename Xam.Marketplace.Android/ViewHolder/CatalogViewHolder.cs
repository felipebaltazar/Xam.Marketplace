using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using System;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.ViewModel;
using static Android.Views.View;

namespace Xam.Marketplace.Android.ViewHolder
{
    public class CatalogViewHolder : ProductViewHolder, IOnClickListener
    {
        #region Fields

        private bool isFavorite;
        private Binding<string, string> photoBinding;
        private Binding<bool, bool> favoriteBinding;
        private Binding<bool, bool> hasDiscountBinding;
        private Binding<string, string> nameBinding;
        private Binding<float, float> priceBinding;
        private Binding<float, float> discountBinding;
        private Binding<int, int> quantityBinding;

        #endregion

        #region Properties

        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value, onChanged: OnFavoriteChanged);
        }

        public TextView FavoriteView
        {
            get;
            private set;
        }

        public Button AddButton
        {
            get;
            set;
        }

        public Button RemoveButton
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CatalogViewHolder(View view, ProductAdapter adapter) : base(view, adapter)
        {
            NameView = view.FindViewById<TextView>(Resource.Id.productName);
            PriceView = view.FindViewById<TextView>(Resource.Id.productPrice);
            DiscountView = view.FindViewById<TextView>(Resource.Id.productDiscount);
            DiscountMain = view.FindViewById<LinearLayout>(Resource.Id.discountView);
            ThumbnailView = view.FindViewById<ImageView>(Resource.Id.productPhoto);
            QuantityView = view.FindViewById<TextView>(Resource.Id.quantityTextview);
            FavoriteView = view.FindViewById<TextView>(Resource.Id.favoriteTextview);

            AddButton = view.FindViewById<Button>(Resource.Id.addButton);
            RemoveButton = view.FindViewById<Button>(Resource.Id.removeButton);

            AddButton.Click += AddButtonClick;
            RemoveButton.Click += RemoveButtonClick;
            view.SetOnClickListener(this);
        }

        #endregion

        #region Public Methods

        public void OnClick(View v) =>
            currentViewModel.SetFavorite(!IsFavorite);

        public override void UpdateViews(ProductViewModel product)
        {
            base.UpdateViews(product);
            OnFavoriteChanged();
        }

        #endregion

        #region Private Methods

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            if (sender is View v)
                v.StartAnimation(_buttonClick);

            currentViewModel.RemoveQuantity();
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            if (sender is View v)
                v.StartAnimation(_buttonClick);

            currentViewModel.AddQuantity();
        }

        protected override void ClearBindings()
        {
            nameBinding?.Detach();
            priceBinding?.Detach();
            photoBinding?.Detach();
            quantityBinding?.Detach();
            discountBinding?.Detach();
            favoriteBinding?.Detach();
            hasDiscountBinding?.Detach();
        }

        protected override void RegisterBindings(ProductViewModel product)
        {
            nameBinding = new Binding<string, string>(product, nameof(product.Name),
                this, nameof(Name), mode: BindingMode.OneWay);

            priceBinding = new Binding<float, float>(product, nameof(product.Price),
                this, nameof(Price), mode: BindingMode.OneWay);

            quantityBinding = new Binding<int, int>(product, nameof(product.Quantity),
                this, nameof(Quantity), mode: BindingMode.TwoWay);

            photoBinding = new Binding<string, string>(product, nameof(product.Photo),
                this, nameof(Photo), mode: BindingMode.OneWay);

            favoriteBinding = new Binding<bool, bool>(product, nameof(product.IsFavorite),
                this, nameof(IsFavorite), mode: BindingMode.TwoWay);

            discountBinding = new Binding<float, float>(product, nameof(product.Discount),
                this, nameof(Discount), mode: BindingMode.TwoWay);

            hasDiscountBinding = new Binding<bool, bool>(product, nameof(product.HasDiscount),
                this, nameof(HasDiscount), mode: BindingMode.TwoWay);
        }

        private void OnFavoriteChanged() =>
            FavoriteView.Visibility = IsFavorite
                ? ViewStates.Visible : ViewStates.Invisible;

        #endregion
    }
}