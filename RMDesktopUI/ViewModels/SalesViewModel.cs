using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Helpers;
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
        private readonly IProductEndpoint _productEndpoint;
        private readonly IConfigHelper _configHelper;
        
        private BindingList<ProductModel>? _products = new();
        private BindingList<CartItemModel>? _cart = new();
        private ProductModel? _selectedProduct;
        private int _itemQuantity = 1;
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
		
        public string SubTotal => CalculateSubTotal().ToString("C");
        
        public string Total => 
            (CalculateSubTotal() + CalculateTax()).ToString("C");

        public string Tax => CalculateTax().ToString("C");
        #endregion

        #region CTOR
        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
        }
        #endregion

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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);

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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
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

        #region PRIVATE
        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart!)
            {
                subTotal += (item.Product?.RetailPrice * item.QuantityInCart)
                    ?? throw new ArgumentNullException("The Product is NULL");
            }
            return subTotal;
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate() * 0.01m;

            foreach (var item in Cart!)
            {
                if (item.Product != null && item.Product.IsTaxable)
                {
                    taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
                }
            }
            return taxAmount;
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        #endregion

        #region OVERRIDE
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        #endregion
    }
}
