using Caliburn.Micro;
using RMDesktopUI.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
		private string _userName = string.Empty;
        private string _password;

        private bool _isErrorVisible;
        private string _errorMessage;

        private IAPIHelper _apiHelper;

		public LoginViewModel(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    output = true;
                }
                return output; 
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;

                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public string UserName
		{
			get { return _userName; }
			set 
			{ 
				_userName = value; 
				NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value; 
				NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

		public bool CanLogIn
		{
			get
			{
                bool output = false;
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                    output = true;

                return output;
            }
		}

		public async Task LogIn()
		{
			try
			{
                ErrorMessage = string.Empty;
                var result = await _apiHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
			{
                ErrorMessage = ex.Message;
            }
		}
	}
}
