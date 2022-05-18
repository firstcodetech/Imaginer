using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.Constants
{
    public enum HttpMethod
    {
        GET,
        POST

    }
    public class ApiConstants
    {
        private readonly int id;
        private readonly string name;
        private readonly string methodName;
        private readonly HttpMethod type;

        ApiConstants(int id, string name, string methodName, HttpMethod type)
        {
            this.id = id;
            this.name = name;
            this.methodName = methodName;
            this.type = type;
        }
        public HttpMethod Type { get { return type; } }
        public string MethodName { get { return methodName; } }
        public static readonly string API_KEY = "5fd14c0164b8cf443695b157a13f99aa";
        public static readonly string API_SECRET = "91721cc3b31aa1aa";
        public static readonly string API_BASE_URL = "https://www.flickr.com/services/rest/";
        public static readonly string PHOTO_BASE_URL = "http://live.staticflickr.com/";

        public static readonly ApiConstants GET_SEARCH_RESULT = new ApiConstants(1, "getSearchResult", "flickr.photos.search", HttpMethod.GET);
        public static readonly ApiConstants GET_PHOTO_METADATA = new ApiConstants(1, "getPhotoMetadata", "", HttpMethod.GET);

        //public static IEnumerable<ApiConstants> Values
        //{
        //    get
        //    {
        //        yield return GET_SEARCH_RESULT;
        //        yield return GET_PHOTO_METADATA;
        //    }
        //}
        
    }
}
