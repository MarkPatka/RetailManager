﻿using Caliburn.Micro;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using RMDesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new();

        public Bootstrapper() 
        { 
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty, 
                "Password", "PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container.Singleton<IWindowManager, WindowManager>()
                      .Singleton<IEventAggregator, EventAggregator>()
                      .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                      .Singleton<IAPIHelper, APIHelper>()
                      ;


            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => 
                    _container.RegisterPerRequest(viewModelType, 
                    viewModelType.ToString(), 
                    viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e) => 
            DisplayRootViewForAsync<ShellViewModel>();
        
        protected override object GetInstance(Type service, string key) => 
            _container.GetInstance(service, key);
        
        protected override IEnumerable<object> GetAllInstances(Type service) => 
            _container.GetAllInstances(service);
       
        protected  override void BuildUp(object instance) => 
            _container.BuildUp(instance);
    }
}
