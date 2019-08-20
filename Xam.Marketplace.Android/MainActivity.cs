using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Ioc;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.Android.Views;
using Xam.Marketplace.Extensions;
using Xam.Marketplace.ViewModel;
using Debug = System.Diagnostics.Debug;
using Environment = System.Environment;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Xam.Marketplace.Android
{
    [Activity(Label = "Catálogo", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, INotifyPropertyChanged
    {
        #region Fields

        private const string DATABASE_NAME = "MarketPlace.db3";

        private bool isBusy = true;
        private Button purchaseButton;
        private LinearLayout filterMenu;
        private MainViewModel mainViewModel;
        private LinearLayout filterMenuList;
        private CategoryAdapter categoryAdapter;
        private LinearLayoutManager layoutManager;
        private Binding<string, string> purchaseBtnTxtBinding;
        private Binding<bool, bool> busyBinding;
        private AlertDialog activityIndicator;

        #endregion

        #region Private Methods

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value, onChanged: OnBusyStateChanged);
        }

        private void OnBusyStateChanged()
        {
            SetActivityIndicator(IsBusy);
        }

        private void SetActivityIndicator(bool show)
        {
            if (show)
                activityIndicator.Show();
            else
                activityIndicator.Dismiss();
        }

        #endregion

        #region Overrides

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var config = new FFImageLoading.Config.Configuration
            {
                ExecuteCallbacksOnUIThread = true
            };

            ImageService.Instance.Initialize(config);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            purchaseButton = FindViewById<Button>(Resource.Id.purchaseButton);
            purchaseButton.Click += PurchaseButtonClick;
            filterMenu = FindViewById<LinearLayout>(Resource.Id.filterMenu);
            filterMenuList = FindViewById<LinearLayout>(Resource.Id.filterMenuList);
            SetSupportActionBar(toolbar);

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DATABASE_NAME);
            SimpleIoc.Default.Register(() => new MainViewModel(new SQLiteAsyncConnection(dbPath)), true);
            mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
            mainViewModel.CompleteCategoryCollection.CollectionChanged += CategoriesCollectionChanged;

            categoryAdapter = new CategoryAdapter(this, mainViewModel.CurrentCategoryCollection);
            layoutManager = new LinearLayoutManager(this);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetAdapter(categoryAdapter);
            recyclerView.SetLayoutManager(layoutManager);

            using (var builder = new AlertDialog.Builder(this))
            {
                builder.SetView(Resource.Layout.progress);
                activityIndicator = builder.Create();
            }

            RegisterBindings();
        }

        private void PurchaseButtonClick(object sender, System.EventArgs e)
        {
            if (mainViewModel.CartViewModel.Products.Count > 0)
            {
                StartActivity(typeof(CartActivity));
            }
        }

        protected override void OnResume()
        {
            base.OnStart();
            mainViewModel
                .FillItemsAsync()
                .SafeFireAndForget(onException: ex => Debug.WriteLine(ex.Message));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            categoryAdapter?.Dispose();
            layoutManager?.Dispose();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                ChangeFilterMenuVisibility();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        #endregion

        #region Private Methods

        private void ChangeFilterMenuVisibility()
        {
            switch (filterMenu.Visibility)
            {
                case ViewStates.Gone:
                case ViewStates.Invisible:
                    filterMenu.Visibility = ViewStates.Visible;
                    break;
                case ViewStates.Visible:
                    filterMenu.Visibility = ViewStates.Gone;
                    break;
                default:
                    break;
            }
        }

        private void CategoriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (filterMenuList.ChildCount > 1)
            {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var defaultOption = new FilterOption(this)
                {
                    Name = "Todas as categorias",
                    IsSelected = true
                };

                defaultOption.OnOptionClicked = () => OnOptionClicked(defaultOption);
                filterMenuList.AddView(defaultOption);

                foreach (CategoryViewModel viewModel in e.NewItems)
                {
                    var categoryOption = new FilterOption(this);
                    categoryOption.SetViewModel(viewModel);
                    categoryOption.OnOptionClicked = () => OnOptionClicked(categoryOption);
                    filterMenuList.AddView(categoryOption);
                }
            }
        }

        private void OnOptionClicked(FilterOption categoryOption)
        {
            for (int i = 0; i < filterMenuList.ChildCount; i++)
            {
                if (filterMenuList.GetChildAt(i) is FilterOption option)
                {
                    option.IsSelected = false;
                }
            }

            categoryOption.IsSelected = true;
            mainViewModel.ApplyFilter(categoryOption.CategoryId);
            ChangeFilterMenuVisibility();
        }

        private void RegisterBindings()
        {
            purchaseBtnTxtBinding = this.SetBinding(() => mainViewModel.PurchaseButtonText,
                () => purchaseButton.Text, mode: BindingMode.OneWay);

            busyBinding = this.SetBinding(() => mainViewModel.IsBusy,
                () => IsBusy, mode: BindingMode.TwoWay);
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

