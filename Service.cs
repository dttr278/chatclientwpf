using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Windows.Media;
using System.Windows.Controls;

namespace chatClient
{
    public static class Extensions
    {
        public static SolidColorBrush ToBrush(this string HexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(HexColorString));
        }
    }
    static class Service
    {
       
  
        public static string token;
        public static User user;
        public  static string URI = "http://localhost:8080/webchat1";
        public static async         Task
GetUserInfo()
        {
            GetUser getUser = await MakeRequestAsync<GetUser>("get", "info", null);
            user = getUser.result;
        }
        public static async Task<T> MakeRequestAsync<T>(string httpMethod, string route, HttpContent postParams/*HttpContent content = null*/)
        {
            using (var client = new HttpClient())
            {
                if(token!=null&&token.Trim()!="")
                     client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{URI}/{route}");
               
                if (postParams != null)
                {
                    requestMessage.Content = postParams;   // This is where your content gets added to the request body
                }

                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = response.Content.ReadAsStringAsync().Result;
                try
                {
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (apiResponse != "")
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }

        public async static Task<BitmapImage> LoadImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();

                        using (Stream file = await response.Content.ReadAsStreamAsync())
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = file;
                            bitmapImage.EndInit();
                        }
                    }
                }
                return bitmapImage;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Failed to load the image: "+ ex.Message);
            }
            return null;
        }
     
 
    }
}
