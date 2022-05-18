using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.Model
{
    public class PhotoListModel
    {
        public Photos photos { get; set; }
        public string stat { get; set; }
    }
    public class Photo
    {
        public string id { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public string title { get; set; }
        [JsonIgnore]
        public string url { get; set; }
        [JsonIgnore]
        public string thumbnailUrl { get; set; }
        [JsonIgnore]
        public bool isOpened { get; set; }

    }

    public class Photos : INotifyPropertyChanged
    {
        public int pages { get; set; }
        [JsonIgnore]
        private int _page;
        public int page
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged("page");
            }
        }
        public int perpage { get; set; }
        public int total { get; set; }
        public ObservableCollection<Photo> photo { get; set; }
        [JsonIgnore]
        public string SearchText { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
