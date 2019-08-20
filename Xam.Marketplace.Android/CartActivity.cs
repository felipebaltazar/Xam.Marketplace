using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using GalaSoft.MvvmLight.Ioc;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.Helpers;
using Xam.Marketplace.ViewModel;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Xam.Marketplace.Android
{
    [Activity(Label = "Carrinho", Theme = "@style/AppTheme.NoActionBar")]
    public sealed class CartActivity : AppCompatActivity
    {
        #region Fields

        private Button buyButton;
        private CartAdapter cartAdapter;
        private LinearLayoutManager layoutManager;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_cart);

            var toolbar = FindViewById<Toolbar>(Resource.Id.cartToolbar);
            buyButton = FindViewById<Button>(Resource.Id.buyButton);
            SetSupportActionBar(toolbar);

            var mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
            var observableCollection = new ObservableRangeCollection<ProductViewModel>(mainViewModel.CartViewModel.Products);
            cartAdapter = new CartAdapter(this, mainViewModel.CartViewModel, observableCollection);
            layoutManager = new LinearLayoutManager(this);

            var recyvlerView = FindViewById<RecyclerView>(Resource.Id.cartRecyclerView);
            recyvlerView.SetLayoutManager(layoutManager);
            recyvlerView.SetAdapter(cartAdapter);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return base.OnSupportNavigateUp();
        }
    }
}