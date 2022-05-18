using Imaginer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.ViewModel
{
    public class AppWindowViewModel : INotifyPropertyChanged
    {
        #region Variable and Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Dictionary<string, object> _inputFields { get; set; }
        //public RelayCommand SaveCommand { get; set; }

        //public RelayCommand MetaDataCommand { get; set; }
        public RelayCommand OpenHomeCommand { get; set; }
        public RelayCommand OpenMediaListCommand { get; set; }

        private object selectedViewModel;
        private bool showBusyIndicator;
        public bool ShowBusyIndicator
        {
            get
            {
                return showBusyIndicator;
            }

            set
            {
                showBusyIndicator = value;
                OnPropertyChanged("ShowBusyIndicator");
            }
        }

        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        public AppWindowViewModel()
        {
            OpenHomeCommand = new RelayCommand(OpenHomePage);
            OpenHomePage(null);
        }
        public AppWindowViewModel(Dictionary<string, object> inputFields)
        {
            _inputFields = inputFields;

            OpenHomeCommand = new RelayCommand(OpenHomePage);
            //OpenMediaListCommand = new RelayCommand(OpenMediaListPage);
            //if (_inputFields != null)
            //{
            //    if (_inputFields.ContainsKey("IsCDRIconClick"))
            //    {

            OpenHomePage(null);
            //    }

            //}
        }


        public void OpenHomePage(object obj)
        {
            SelectedViewModel = new HomeViewModel(_inputFields, this);
        }
        public void OpenMediaListPage(object obj)
        {
            SelectedViewModel = new MediaListViewModel(_inputFields, this);
        }
        #endregion
    }
}
