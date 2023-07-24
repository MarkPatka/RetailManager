using System.Windows;
using System.Windows.Controls;

namespace RMDesktopUI.Helpers
{
    internal static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject d)
        {
            if (d is PasswordBox box)
            {
                box.PasswordChanged -= PasswordChanged;
                box.PasswordChanged += PasswordChanged;
            }
            return d.GetValue(BoundPasswordProperty)?.ToString() ?? string.Empty;
        }

        public static void SetBoundPassword(DependencyObject d, string value)
        {
            if (string.Equals(value, GetBoundPassword(d)))
                return;

            d.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox box || d is null)
                return;

            box.Password = GetBoundPassword(d);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox password = sender as PasswordBox ?? new PasswordBox();
            SetBoundPassword(password, password.Password);

            password?.GetType()?
                     .GetMethod("Select", 
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic)?
                     .Invoke(password,                    
                        new object[] { password.Password.Length, 0}); 
        }


    }
}
