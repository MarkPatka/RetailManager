using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        #region VAR
        private BindingList<ProductModel>? _products = new();
        private BindingList<CartItemModel>? _cart = new();
        private int _itemQuantity = 1;
        IProductEndpoint _productEndpoint;
        private ProductModel? _selectedProduct;
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
		public BindingList<CartItemModel>? Cart
		{
			get { return _cart; }
			set 
			{ 
				_cart = value; 
				NotifyOfPropertyChange(() => Cart);
			}
		}
        public ProductModel? SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }
        public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{ 
				_itemQuantity = value; 
				NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);

            }
        }
		public string SubTotal
		{
			get 
			{
                decimal subTotal = 0;
                foreach (var item in Cart!) 
                {
                    subTotal += (item.Product?.RetailPrice * item.QuantityInCart) 
                        ?? throw new ArgumentNullException("The Product is NULL");
                }
                return subTotal.ToString("C");
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
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }

				return output;

			}
		}
		public void AddToCart()
		{
            CartItemModel? existingItem = Cart?.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                Cart?.Remove(existingItem);
                Cart?.Add(existingItem);
            }
            else
            {
                CartItemModel item = new()
                {
                    Product = SelectedProduct!,
                    QuantityInCart = ItemQuantity
                };
                Cart?.Add(item);
            }
                        
            SelectedProduct!.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
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
            NotifyOfPropertyChange(() => SubTotal);
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
