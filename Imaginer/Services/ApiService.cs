using Imaginer.Constants;
using Imaginer.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using Imaginer.Model;

namespace Imaginer.Services
{
    public class ApiService : IApiService
    {
        private static HttpClient httpClient = new HttpClient();

        public PhotoListModel GetImageListBySearchText(string searchText,int pageNo)
        {
            PhotoListModel response = null;
            var builder = new UriBuilder(ApiConstants.API_BASE_URL);
            var query = HttpUtility.ParseQueryString(builder.Query);
            string payload = string.Empty;
            payload = null;
            query["method"] = ApiConstants.GET_SEARCH_RESULT.MethodName;
            query["api_key"] = ApiConstants.API_KEY;
            query["text"] = searchText;
            query["format"] = "json";
            query["page"] = pageNo.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();
            response = CreateRequest(url, response, ApiConstants.GET_SEARCH_RESULT.Type, payload);
            return response;
        }

        public static T CreateRequest<T>(string url, T responseValue, Constants.HttpMethod type, string payload = null)
        {
            if (CheckInetConnection())
            {
                try
                {
                    Task reqTask = null;
                    if (type == Constants.HttpMethod.GET)
                    {
                        reqTask = httpClient.GetAsync(url)
                          .ContinueWith((taskwithresponse) =>
                          {
                              try
                              {
                                  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                  var response = taskwithresponse.Result;
                                  response.EnsureSuccessStatusCode();
                                  var jsonString = response.Content.ReadAsStringAsync();
                                  jsonString.Wait();
                                  String result = jsonString.GetAwaiter().GetResult();
                                  result = result.Replace("jsonFlickrApi(","");
                                  result = result.Replace(")", "");
                                  responseValue = JsonConvert.DeserializeObject<T>(result);
                              }
                              catch (AggregateException ae)
                              {
                                  if (ae.InnerException is WebException)
                                  {
                                      throw ae.InnerException as WebException;
                                  }
                                  else if (ae.InnerException != null && ae.InnerException.InnerException != null
                                           && ae.InnerException.InnerException is WebException)
                                  {
                                      WebException we = ae.InnerException.InnerException as WebException;
                                      throw new WebException(we.Message, we.InnerException, we.Status, null);
                                  }
                                  throw ae;
                              }
                          });
                        reqTask.Wait();

                    }

                    return responseValue;
                }
                catch (WebException webEx)
                {
                    var webResponse = webEx.Response as HttpWebResponse;
                    if (webResponse != null &&
                        webResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                    }
                    else if (webResponse == null && webEx.Status == WebExceptionStatus.ConnectFailure || (webResponse != null && webResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                    }
                    else if (webResponse == null && webEx.Status == WebExceptionStatus.Timeout)
                    {
                    }
                    else
                        return default(T);
                }
                catch (Exception e)
                {
                   
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
            return default(T);
        }

        public static bool CheckInetConnection()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                return true;
            }
            return false;
        }
    }
}
