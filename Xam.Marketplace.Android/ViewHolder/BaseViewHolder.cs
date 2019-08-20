using Android.Runtime;
using Android.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AndroidViewHolder = Android.Support.V7.Widget.RecyclerView.ViewHolder;

namespace Xam.Marketplace.Android.ViewHolder
{
    public abstract class BaseViewHolder : AndroidViewHolder, INotifyPropertyChanged
    {
        #region Constructors

        protected BaseViewHolder(View itemView)
            : base(itemView)
        { }

        protected BaseViewHolder(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        { }

        #endregion

        #region Public Methods

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}