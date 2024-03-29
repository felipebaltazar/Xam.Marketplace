﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xam.Marketplace.Helpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
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
