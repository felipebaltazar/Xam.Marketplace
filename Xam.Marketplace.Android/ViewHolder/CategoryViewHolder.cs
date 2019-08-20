using Android.Views;
using Android.Widget;
using System.Linq;
using System.Windows.Input;
using AndroidViewHolder = Android.Support.V7.Widget.RecyclerView.ViewHolder;
using static Android.Views.View;
using Xam.Marketplace.Android.Adapter;
using Xam.Marketplace.Model;
using Android.Support.V7.Widget;
using Xam.Marketplace.ViewModel;

namespace Xam.Marketplace.Android.ViewHolder
{
    public class CategoryViewHolder : AndroidViewHolder
    {
        public TextView NameView { get; private set; }
        public RecyclerView ProductRecyclerView { get; private set; }

        private readonly LinearLayoutManager _layoutManager;
        private readonly CategoryAdapter _parentAdapter;
        private readonly ProductAdapter _childAdapter;
        private readonly View _parent;

        public CategoryViewHolder(View view, CategoryAdapter adapter) : base(view)
        {
            _parentAdapter = adapter;
            _childAdapter = new ProductAdapter(view.Context);
            _layoutManager = new LinearLayoutManager(view.Context);
            _parent = view;

            BindViews(_parent);
        }

        private void BindViews(View view)
        {
            NameView = view.FindViewById<TextView>(Resource.Id.categoryTextView);
            ProductRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.categoryRecyclerView);

            ProductRecyclerView.SetAdapter(_childAdapter);
            ProductRecyclerView.SetLayoutManager(_layoutManager);
        }

        public void UpdateViews(CategoryViewModel category)
        {
            NameView.Text = category.Name;

            if(category.Products?.Count > 0)
                _childAdapter.UpdateCollection(category.Products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _layoutManager?.Dispose();
                _childAdapter?.Dispose();
                NameView?.Dispose();
                ProductRecyclerView?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}