using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xam.Marketplace.ViewModel;
using static Android.Views.View;

namespace Xam.Marketplace.Android.Views
{
    public class FilterOption : RelativeLayout, IOnClickListener, INotifyPropertyChanged
    {
        #region Fields

        private AlphaAnimation onClick;
        private TextView nameTextView;
        private TextView selectedTextView;
        private string name;
        private bool isSelected;

        #endregion

        #region Properties

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, onChanged: OnNameChanged);
        }

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value, onChanged: OnIsSelectedChanged);
        }

        public Action OnOptionClicked
        {
            get;
            set;
        }

        public int? CategoryId
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public FilterOption(Context context)
            : base(context)
        {
            Init();
        }

        public FilterOption(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init();
        }

        public FilterOption(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        public FilterOption(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init();
        }

        protected FilterOption(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Init();
        }

        #endregion

        #region IOnClickListener

        public void OnClick(View v)
        {
            StartAnimation(onClick);
            OnOptionClicked?.Invoke();
        }

        #endregion

        #region Public Methods

        public void SetViewModel(CategoryViewModel categoryViewModel)
        {
            Name = categoryViewModel.Name;
            CategoryId = categoryViewModel.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Methods

        private void Init()
        {
            onClick = new AlphaAnimation(1F, 0.7F);
            Inflate(Context, Resource.Layout.filter_option, this);
            SetOnClickListener(this);

            nameTextView = FindViewById<TextView>(Resource.Id.optionName);
            selectedTextView = FindViewById<TextView>(Resource.Id.optionSelected);

            nameTextView.Text = Name;
        }

        private void OnIsSelectedChanged() => 
            selectedTextView.Visibility =
                IsSelected ? ViewStates.Visible : ViewStates.Invisible;

        private void OnNameChanged() =>
            nameTextView.Text = Name;

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