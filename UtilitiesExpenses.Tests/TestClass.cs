using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilitiesExpenses.Helpers;
using UtilitiesExpenses.Model;
using UtilitiesExpenses.ViewModel;

namespace UtilitiesExpenses.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestShowingErrorWhenSaving()
        {
            var dialogService = new TestDialogService();
            var vm = new MainViewModel(new TestTariffsService(), dialogService, new TestNavigationService());

            Assert.IsNull(dialogService.MessageShown);

            vm.SaveCommand.Execute(new Tariff { Id = 1 });

            Assert.AreEqual(TestTariffsService.ErrorMessage, dialogService.MessageShown);
        }
    }

    internal sealed class TestDialogService : IDialogService
    {
        public string MessageShown { get; private set; }

        public void ShowMessage(string message)
        {
            MessageShown = message;
        }
    }

    internal sealed class TestNavigationService : INavigationService
    {
        public void GoBack()
        {
        }

        public void NavigateTo(Uri uri)
        {
        }
    }

    internal sealed class TestTariffsService : ITariffsService
    {
        public const string ErrorMessage = "Test error message.";

        public IEnumerable<Tariff> Refresh()
        {
            return Enumerable.Empty<Tariff>();
        }

        public int Save(Tariff updatedTariff)
        {
            throw new Exception(ErrorMessage);
        }
    }
}
