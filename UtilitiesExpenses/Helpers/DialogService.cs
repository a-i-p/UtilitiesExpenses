using System.Windows;

namespace UtilitiesExpenses.Helpers
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}