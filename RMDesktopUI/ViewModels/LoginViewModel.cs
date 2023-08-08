using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using System;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
		private string _userName = "markP@gmail.com";
        private string _password = "Qazwsx1!";
        private string _errorMessage;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

		public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
		{
			_apiHelper = apiHelper;
            _events = events;
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

                // Capture more information about the user
                string? token = result.Access_Token;

                if (!string.IsNullOrEmpty(token))
                    await _apiHelper.GetLoggedInUserinfo(token);

                await _events.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
			{
                ErrorMessage = ex.Message;
            }
		}
	}
}
