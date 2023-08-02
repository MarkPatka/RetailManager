using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        #region VAR
        private BindingList<ProductModel>? _products;
        private BindingList<ProductModel>? _cart;
        private int _itemQuantity;
        IProductEndpoint _productEndpoint;
        #endregion

        #region PROPERTIES
        public BindingList<ProductModel>? Products
        {
			get { return _products; }
			set 
			{ 
				_products = value; 
				NotifyOfPropertyChange(() => Products);
			}
		}

		public BindingList<ProductModel>? Cart
		{
			get { return _cart; }
			set 
			{ 
				_cart = value; 
				NotifyOfPropertyChange(() => Cart);
			}
		}

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{ 
				_itemQuantity = value; 
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		public string SubTotal
		{
			get 
			{
				return "$0.00";
			}
		}

        public string Total
        {
            get
            {
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                return "$0.00";
            }
        }
        #endregion

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;            
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        #region PUBLIC METHODS
        public bool CanAddToCart
		{
			get
			{
				bool output = false;
				return output;

			}
		}

		public void AddToCart()
		{

		}


        public bool CanRemoveToCart
        {
            get
            {
                bool output = false;
                return output;

            }
        }

        public void RemoveToCart()
        {

        }


        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                return output;

            }
        }

        public void Checkout()
        {

        }
        #endregion
    }
}
