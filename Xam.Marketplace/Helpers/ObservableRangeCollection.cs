using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace Xam.Marketplace.Helpers
{
    /// <summary>
    /// Based on James Montemagno helpers <see cref="https://github.com/jamesmontemagno/mvvm-helpers"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        #region Constructors

        public ObservableRangeCollection()
            : base() { }

        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection) { }

        #endregion

        #region Public Methods

        public void AddRange(IEnumerable<T> collection,
            NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                foreach (var i in collection)
                    Items.Add(i);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                return;
            }

            var startIndex = Count;
            var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);
            foreach (var i in changedItems)
                Items.Add(i);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var i in collection)
                Items.Remove(i);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Items.Clear();
            AddRange(collection, NotifyCollectionChangedAction.Reset);
        }

        public void Replace(T item) =>
            ReplaceRange(new T[] { item });

        #endregion
    }
}
