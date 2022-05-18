using Imaginer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.Services.Interface
{
    public interface IApiService
    {
        PhotoListModel GetImageListBySearchText(string searchText,int pageNo);
    }
}
