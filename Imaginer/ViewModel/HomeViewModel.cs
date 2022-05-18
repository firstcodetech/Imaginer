using Imaginer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        #region Variable & Properties
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _SearchText;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                _SearchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        private RelayCommand _SearchTextCommand = null;
        public RelayCommand SearchTextCommand
        {
            get
            {
                if (_SearchTextCommand == null) {
                    _SearchTextCommand = new RelayCommand(SearchTextMethod);
                }
                return _SearchTextCommand;
            }
        }
        private Dictionary<string, object> _inputFields = null;
        private AppWindowViewModel parentReference = null;


        #endregion
        #region Methods
        public HomeViewModel(Dictionary<string, object> inputFields = null, AppWindowViewModel parentReferenceObj = null)
        {
            _inputFields = inputFields;
            parentReference = parentReferenceObj;
            

        }
        public void SearchTextMethod(object obj)
        {
            if(_inputFields == null)
            {
                _inputFields = new Dictionary<string, object>();
            }
            if (_inputFields.ContainsKey("SearchText"))
            {
                _inputFields["SearchText"] = SearchText;

            }
            else
            {
                _inputFields.Add("SearchText", SearchText);
            }
            parentReference._inputFields = _inputFields;
            parentReference.OpenMediaListPage(null);

        }
        #endregion

    }
}
