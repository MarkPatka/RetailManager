using Caliburn.Micro;
using System.ComponentModel;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
		private BindingList<string>? _products;
        private BindingList<string>? _cart;
        private int _itemQuantity;


        public BindingList<string>? Products
        {
			get { return _products; }
			set 
			{ 
				_products = value; 
				NotifyOfPropertyChange(() => Products);
			}
		}


		public BindingList<string>? Cart
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


    }
}
