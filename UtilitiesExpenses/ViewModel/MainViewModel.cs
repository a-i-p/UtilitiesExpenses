using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UtilitiesExpenses.Helpers;
using UtilitiesExpenses.Model;

namespace UtilitiesExpenses.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ITariffsService _dataService;
        private IDialogService _dialogService;
        private INavigationService _navigationService;

        private RelayCommand _refreshCommand;
        private RelayCommand<Tariff> _saveCommand;
        private Tariff _selectedTariff;
        private RelayCommand<Tariff> _showDetailsCommand;

        public MainViewModel(ITariffsService dataServise, IDialogService dialogService, INavigationService navigationService)
        {
            _dataService = dataServise;
            _dialogService = dialogService;
            _navigationService = navigationService;
            TariffList = new ObservableCollection<Tariff>();
        }

        public MainViewModel()
            : this(IsInDesignModeStatic
                        ? (ITariffsService)new Design.DesignTariffsService()
                        : new TariffsService(),
                  new DialogService(),
                  new NavigationService())
        {
#if DEBUG
            if (IsInDesignModeStatic)
            {
                refresh();
                SelectedTariff = TariffList[2];
            }
#endif
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                  ?? (_refreshCommand = new RelayCommand(() =>
                  {
                      refresh();
                  }));
            }
        }

        public RelayCommand<Tariff> SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand<Tariff>(
                                            tariff =>
                                            {
                                                try
                                                {
                                                    int result = _dataService.Save(tariff);

                                                    if (result <= 0)
                                                    {
                                                        _dialogService.ShowMessage("Error");
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    _dialogService.ShowMessage(ex.Message);
                                                }
                                            }));
            }
        }

        public Tariff SelectedTariff
        {
            get { return _selectedTariff; }
            set
            {
                Set(nameof(SelectedTariff), ref _selectedTariff, value);
            }
        }

        public RelayCommand<Tariff> ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand
                ?? (_showDetailsCommand = new RelayCommand<Tariff>(
                    tariff =>
                    {
                        SelectedTariff = new Tariff { Id = tariff.Id, Name = tariff.Name, Rate = tariff.Rate };
                        _navigationService.NavigateTo(new Uri(tariff.Name, UriKind.Relative));
                    }));
            }
        }

        public ObservableCollection<Tariff> TariffList { get; private set; }

        private void refresh()
        {
            TariffList.Clear();

            IEnumerable<Tariff> tariffs = _dataService.Refresh();

            foreach (Tariff tariff in tariffs)
            {
                TariffList.Add(tariff);
            }
        }
    }
}
