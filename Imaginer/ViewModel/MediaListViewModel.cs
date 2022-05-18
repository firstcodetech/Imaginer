using Imaginer.Constants;
using Imaginer.Model;
using Imaginer.Services;
using Imaginer.Services.Interface;
using Imaginer.Services.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Imaginer.ViewModel
{
    public class MediaListViewModel : INotifyPropertyChanged
    {
        #region Variable & Properties
        public event PropertyChangedEventHandler PropertyChanged;
        IApiService apiSvcObj = null;
        private bool isBgWorkerBusy;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Dictionary<string, object> _inputFields = null;
        private AppWindowViewModel parentReference = null;
       // public string SearchText = "";
        private RelayCommand _SearchTextCommand;
        public RelayCommand SearchTextCommand
        {
            get
            {
                if(_SearchTextCommand == null)
                {
                    _SearchTextCommand = new RelayCommand(SearchImage);
                }
                return _SearchTextCommand;
            }
        }
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        private Photos _photoMetdataList;
        public Photos PhotoMetadataList
        {
            get { return _photoMetdataList; }
            set
            {
                _photoMetdataList = value;
                OnPropertyChanged("PhotoMetadataList");
            }
        }
        private Photo _SelectedImage;
        public Photo SelectedImage
        {
            get { return _SelectedImage; }
            set { _SelectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }
        private RelayCommand _OpenSelectedImageCommand;
        public RelayCommand OpenSelectedImageCommand
        {
            get
            {
                if(_OpenSelectedImageCommand == null)
                {
                    _OpenSelectedImageCommand = new RelayCommand(OpenSelectedImage);
                }
                return _OpenSelectedImageCommand;
            }
        }
        
            private RelayCommand _CloseOpenImageCommand;
        public RelayCommand CloseOpenImageCommand
        {
            get
            {
                if (_CloseOpenImageCommand == null)
                {
                    _CloseOpenImageCommand = new RelayCommand(CloseOpenImage);
                }
                return _CloseOpenImageCommand;
            }
        }
        
             private RelayCommand _BackButtonCommand;
        public RelayCommand BackButtonCommand
        {
            get
            {
                if (_BackButtonCommand == null)
                {
                    _BackButtonCommand = new RelayCommand(BackButtonClick);
                }
                return _BackButtonCommand;
            }
        }

        
        private Stack<Photos> StackPreviousSearch;
       
        #endregion

        #region Methods
        public MediaListViewModel(Dictionary<string, object> inputFields = null, AppWindowViewModel parentReferenceObj = null)
        {
            _inputFields = inputFields;
            parentReference = parentReferenceObj;
            apiSvcObj = ServiceFactory.GetInstance<IApiService>();
            StackPreviousSearch = new Stack<Photos>();
        }
        public void Load()
        {
            if(_inputFields == null)
            {
                return;
            }
            if (_inputFields.ContainsKey("SearchText"))
            {
                SearchText = _inputFields["SearchText"].ToString();

            }
            SetSearchResult(SearchText);
        }
        public void SetSearchResult(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return;
            }
            int pageNo = 1;
            PhotoListModel photoListObj = apiSvcObj.GetImageListBySearchText(SearchText,pageNo);
            if(photoListObj?.photos?.photo != null)
            {
                photoListObj.photos.photo = SetUrlofPhotosInModel(photoListObj?.photos?.photo);
                PhotoMetadataList = photoListObj.photos;
                PhotoMetadataList.SearchText = SearchText;
            }
            
        }
        public ObservableCollection<Photo> SetUrlofPhotosInModel(ObservableCollection<Photo> photoList)
        {
            if(photoList?.Count == 0)
            {
                return photoList;
            }
            foreach(var photo in photoList)
            {
                string imageUrlWithoutExt = string.Format("{0}/{1}/{2}_{3}", ApiConstants.PHOTO_BASE_URL, photo.server, photo.id, photo.secret);
                photo.url = string.Format("{0}{1}", imageUrlWithoutExt, CommonConstant.ImageExtension);
                photo.thumbnailUrl = string.Format("{0}{1}{2}", imageUrlWithoutExt,CommonConstant.ThumbnailCharacter, CommonConstant.ImageExtension);
            }
            return photoList;
        }
        public void SearchImage(object obj)
        {
            StackPreviousSearch.Push(PhotoMetadataList);
            SetSearchResult(SearchText);
            
        }
        private void OpenSelectedImage(object obj)
        {
            Photo selectedPhoto = obj as Photo;
            if(selectedPhoto == null)
            {
                SelectedImage = new Photo();
            }
            else
            {
                selectedPhoto.isOpened = true;
                SelectedImage = selectedPhoto;
                //SelectedImage.isOpened = true;
            }
        }
        private void CloseOpenImage(object obj)
        {
            SelectedImage = new Photo();
        }
        private void BackButtonClick(object obj)
        {
            if(StackPreviousSearch.Count > 0)
            {
                PhotoMetadataList = StackPreviousSearch.Pop();
                SearchText = PhotoMetadataList.SearchText;
            }
            else
            {
                parentReference.OpenHomePage(null);
            }
        }
        public void FetchNextPage()
        {
            
            if (PhotoMetadataList.page < PhotoMetadataList.pages && !isBgWorkerBusy)
            {
                isBgWorkerBusy = true;
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                PhotoListModel photoList = null;
                worker.DoWork += (s, o) =>
                {
                    
                    photoList = apiSvcObj.GetImageListBySearchText(SearchText, PhotoMetadataList.page + 1);
                };
                worker.RunWorkerCompleted += (d, objct) =>
                {

                    if(photoList?.photos != null && photoList.photos.photo?.Count > 0)
                    {
                        foreach(var photo in photoList.photos.photo)
                        {
                            PhotoMetadataList.photo.Add(photo);
                        }
                        PhotoMetadataList.page = photoList.photos.page;
                    }
                    isBgWorkerBusy = false;
                };
                worker.RunWorkerAsync();
                

            }
        }

        #endregion
    }
}
