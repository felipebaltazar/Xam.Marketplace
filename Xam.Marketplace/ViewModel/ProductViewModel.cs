using System;
using System.Linq;
using Xam.Marketplace.Abstractions;
using Xam.Marketplace.Model;

namespace Xam.Marketplace.ViewModel
{
    public sealed class ProductViewModel : BaseViewModel
    {
        #region Fields

        private int? categoryId;
        private string description;
        private int id;
        private string name;
        private string photo;
        private float price;
        private float discount;
        private bool hasDiscount;
        private bool isFavorite;
        private int quantity;
        private IProductCallback callback;

        #endregion

        #region Properties

        public int? CategoryId
        {
            get => categoryId;
            set => SetProperty(ref categoryId, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }

        public OfferDTO Offer
        {
            get;
            set;
        }

        public float Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public float Discount
        {
            get => discount;
            set => SetProperty(ref discount, value);
        }

        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }

        public bool HasDiscount
        {
            get => hasDiscount;
            set => SetProperty(ref hasDiscount, value);
        }

        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value, onChanged: OnQuantityChanged);
        }

        #endregion

        #region Public Methods

        public void AddQuantity() => callback?.AddToCart(this);

        public void RemoveQuantity() => callback?.RemoveFromCart(this);

        public void SetFavorite(bool favorite)
        {
            IsFavorite = favorite;
            callback.OnFavoriteChanged(this, favorite);
        }

        public ProductViewModel RegisterCallback(IProductCallback productCallback)
        {
            callback = productCallback;
            return this;
        }

        public float CalculeValue() =>
            Quantity * ProcessDiscount();

        public float ProcessDiscount()
        {
            var policy = GetCurrentDiscount();
            if (policy == null)
                return price;

            return price - ((policy.Discount / 100) * price);
        }

        private PolicyDTO GetCurrentDiscount()
        {
            return Offer?.Policies?
                .OrderByDescending(p => p.Min)?
                .FirstOrDefault(p => p.Min <= Quantity);
        }

        #endregion

        #region Private Methods

        private void OnQuantityChanged()
        {
            var policy = GetCurrentDiscount();
            if (policy != null)
            {
                Discount = policy.Discount;
                HasDiscount = true;
            }
            else
            {
                HasDiscount = false;
            }

        }

        #endregion
    }
}
