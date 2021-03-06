using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Basch.Api.Core.Extensions
{
    public class ApplicationExtensions
    {
        public static CloudBlobContainer ConfigureBlobContainer(string account, string key)
        {
            // Configures container based on credentials passed in.
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            return container;
        }

        public static async Task<T> Get<T>(string user, string secret)
        {
            using (var client = new HttpClient())
            {
                var authenticationString = $"{user}:{secret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                var result = await client.GetAsync("https://api.simplewebrtc.com/rooms/active");
                result.EnsureSuccessStatusCode();
                string resultString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultString);
                return resultContent;
            }
        }
    }
}
