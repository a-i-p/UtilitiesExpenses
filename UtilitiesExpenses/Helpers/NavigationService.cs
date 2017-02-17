using System;
using System.Windows;

namespace UtilitiesExpenses.Helpers
{
    public class NavigationService : INavigationService
    {
        public void GoBack()
        {
            MessageBox.Show("GoBack");
        }

        public void NavigateTo(Uri uri)
        {
            MessageBox.Show(uri.ToString());
        }
    }
}