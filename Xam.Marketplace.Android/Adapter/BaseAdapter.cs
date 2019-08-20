using Android.Content;
using Android.Support.V7.Widget;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xam.Marketplace.Helpers;

namespace Xam.Marketplace.Android.Adapter
{
    public abstract class BaseAdapter<TSource> : RecyclerView.Adapter, INotifyPropertyChanged
    {
        #region Fields

        protected readonly Context _context;
        protected ObservableRangeCollection<TSource> _itemSource;

        #endregion

        #region Properties

        public override int ItemCount => ItemSource.Count;

        public ObservableRangeCollection<TSource> ItemSource
        {
            get => _itemSource;
            set => SetProperty(ref _itemSource, value, onChanged: OnItemSourceChanged);
        }

        #endregion

        #region Constructors

        protected BaseAdapter(Context context)
            : this(context, new ObservableRangeCollection<TSource>())
        { }

        protected BaseAdapter(Context context, ObservableRangeCollection<TSource> itemSource)
        {
            _context = context;
            ItemSource = itemSource;
            ItemSource.CollectionChanged += ItemSourceCollectionChanged;
        }

        #endregion

        #region Private Methods

        private void ItemSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }

        private void OnItemSourceChanged()
        {
            ItemSource.CollectionChanged += ItemSourceCollectionChanged;
            NotifyDataSetChanged();
        }

        #endregion

        #region Public Methods

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

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