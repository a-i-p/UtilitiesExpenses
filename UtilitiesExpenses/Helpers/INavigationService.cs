using System;

namespace UtilitiesExpenses.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(Uri uri);
    }
}
