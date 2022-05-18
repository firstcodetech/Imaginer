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
                    //logger.Debug("URL :" + url);
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

                                  //responseValue = (T)Convert.ChangeType(result, typeof(T));
                                  responseValue = JsonConvert.DeserializeObject<T>(result);
                              }
                              catch (AggregateException ae)
                              {
                                  if (ae.InnerException is WebException)
                                  {
                                      //logger.Error(ae.InnerException.Message, "AggregateException Caught WebException raised in setResponseForGetRequest()");
                                      throw ae.InnerException as WebException;
                                  }
                                  else if (ae.InnerException != null && ae.InnerException.InnerException != null
                                           && ae.InnerException.InnerException is WebException)
                                  {
                                      //logger.Error(ae.InnerException.Message, "AggregateException Caught WebException raised in setResponseForGetRequest()");
                                      WebException we = ae.InnerException.InnerException as WebException;
                                      throw new WebException(we.Message, we.InnerException, we.Status, null);
                                  }
                                  //logger.Error(ae.Message, "Aggregate Exception raised in setResponseForGetRequest()");
                                  throw ae;
                              }
                          });
                        reqTask.Wait();

                    }
                    else if (type == Constants.HttpMethod.POST)
                    {
                        bool isReRun = false;//UPDATE THE FLAG CAREFULLY...used for recursive call. Placement must always be above GOTO label.
                        ReRunPostWithChangedConfig:
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        //Below settings work for larger data. Will move to read on basis of content length once corrected. Response not returning correct content-length.
                        //Default protocol version is 11. Not moving all calls to version 10 as most of the cases are executed with default value only.
                        if (isReRun)
                        {
                            httpWebRequest.KeepAlive = false;
                            httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                        }
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.UserAgent = httpClient.DefaultRequestHeaders.UserAgent.ToString();
                        //httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, SirionLoginService.token);
                        httpWebRequest.Accept = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(payload);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string result = string.Empty;
                            try
                            {
                                result = streamReader.ReadToEnd();
                            }
                            catch (IOException exReader)
                            {
                                //Hadling for Exception : Unable to read data from the transport connection: The connection was closed.
                                //logger.Error(exReader.Message + "**Stack Trace: " + exReader.StackTrace);
                                if (!isReRun)
                                {
                                    isReRun = true;
                                    goto ReRunPostWithChangedConfig;
                                }
                                else
                                    throw exReader;//handle as previously handled exception cases below in outter catch.
                            }

                            
                            responseValue = JsonConvert.DeserializeObject<T>(result);
                        }
                    }


                    return responseValue;
                }
                catch (WebException webEx)
                {
                    var webResponse = webEx.Response as HttpWebResponse;
                    // logger.Error(webEx.Message + " STATUS: " + webEx.Status + " **STACKTRACE: " + webEx.StackTrace);
                    if (webResponse != null &&
                        webResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //throw new SirionException(ResourceManagerService.GetResourceString(24005), true);
                    }
                    else if (webResponse == null && webEx.Status == WebExceptionStatus.ConnectFailure || (webResponse != null && webResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        //throw new SirionException(ResourceManagerService.GetResourceString(24006), webEx.Status);
                    }
                    else if (webResponse == null && webEx.Status == WebExceptionStatus.Timeout)
                    {
                        //throw new SirionException(webEx.Message, webEx.Status);
                    }
                    else
                        return default(T);
                }
                catch (Exception e)
                {
                    //if (e.InnerException != null && !string.IsNullOrEmpty(e.InnerException.Message))
                    //{
                    //    //suppress message as plugin will be installed before application changes Jan2019 release
                    //    if (url.Contains("latestVersionInfo") && e.InnerException.Message.Contains("404"))
                    //    {
                    //        return default(T);
                    //    }
                    //    if (e.Message.Contains("Unable to connect to the remote server") || e.InnerException.Message.Contains("Unable to connect to the remote server"))
                    //    {
                    //        //throw new SirionException(ResourceManagerService.GetResourceString(27592), true);
                    //    }
                    //    else if (e.InnerException.Message == "One or more errors occurred." || e.Message == "One or more errors occurred.")
                    //    {
                    //        //throw new SirionException(ResourceManagerService.GetResourceString(24005), true);
                    //    }
                    //    else
                    //        MessageBox.Show(e.InnerException.Message, "Sirion", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}
                    //else
                    //    MessageBox.Show(e.Message, "Sirion", MessageBoxButton.OK, MessageBoxImage.Error);
                    //if (e.Message == "One or more errors occurred.")
                    //{
                    //    //throw new SirionException(ResourceManagerService.GetResourceString(24005), true);
                    //}
                    //logger.Error(e.Message + "**Stack Trace: " + e.StackTrace, "Exception raised in setResponseForGetRequest()");
                    //logger.Debug("RESPONSE : " + responseValue);
                    return default(T);
                }
            }
            else
            {
                //MessageBox.Show(ResourceManagerService.GetResourceString(24007), "Sirion", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);

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
