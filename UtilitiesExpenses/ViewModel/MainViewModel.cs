using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UtilitiesExpenses.Helpers;
using UtilitiesExpenses.Model;

namespace UtilitiesExpenses.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
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
            : this(System.Windows.Application.Current is App
                        ? new TariffsService()
                        : (ITariffsService)new Design.DesignTariffsService(),
                  new DialogService(),
                  new NavigationService())
        {
#if DEBUG
            Refresh();
            SelectedTariff = TariffList[2];
#endif
        }

        public event PropertyChangedEventHandler PropertyChanged = new PropertyChangedEventHandler((s, a) => { });

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                  ?? (_refreshCommand = new RelayCommand(() =>
                  {
                      Refresh();
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
                                                int result = _dataService.Save(tariff);

                                                if (result <= 0)
                                                {
                                                    _dialogService.ShowMessage("Error");
                                                }
                                            }));
            }
        }

        public Tariff SelectedTariff
        {
            get { return _selectedTariff; }
            set
            {
                if (_selectedTariff != value)
                {
                    _selectedTariff = value;
                    RaisePropertyChanged(nameof(SelectedTariff));
                }
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

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Refresh()
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
