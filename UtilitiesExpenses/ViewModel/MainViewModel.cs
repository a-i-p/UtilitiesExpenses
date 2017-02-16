using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;
using UtilitiesExpenses.Helpers;
using UtilitiesExpenses.Model;

namespace UtilitiesExpenses.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ITariffsService _dataService;
        private IDialogService _dialogService;
        private INavigationService _navigationService;


        public ObservableCollection<Tariff> TariffList { get; private set; }

        private Tariff _selectedTariff;
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

        private RelayCommand _refreshCommand;
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

        private RelayCommand<Tariff> _saveCommand;
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

        private RelayCommand<Tariff> _showDetailsCommand;
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


        private void Refresh()
        {
            TariffList.Clear();

            IEnumerable<Tariff> tariffs = _dataService.Refresh();

            foreach (Tariff tariff in tariffs)
            {
                TariffList.Add(tariff);
            }
        }

        public MainViewModel(ITariffsService dataServise, IDialogService dialogService, INavigationService navigationService)
        {
            _dataService = dataServise;
            _dialogService = dialogService;
            _navigationService = navigationService;
            TariffList = new ObservableCollection<Tariff>();
        }

        public MainViewModel()
            : this(new Design.DesignTariffsService(), new DialogService(), new NavigationService())
        {
#if DEBUG
            Refresh();
            SelectedTariff = TariffList[2];
#endif
        }

        public event PropertyChangedEventHandler PropertyChanged = new PropertyChangedEventHandler((s, a) => { });

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
