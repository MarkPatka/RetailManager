﻿using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly LoginViewModel _loginViewModel;

        #region ctor
        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginViewModel = loginVM;
            ActivateItemAsync(_loginViewModel);
        }
        #endregion
    }
}
