using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Utilities
    {
        public async Task<HttpResponseMessage> API_Response(string apiUrl)
        {
            string sLogErr = string.Empty;

            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = null;

                response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    sLogErr = (int)response.StatusCode + "-" + response.StatusCode.ToString();
                    //_logger.LogError(sLogErr);
                    throw new Exception(sLogErr);
                }
                return response;
            }
        }
    }
}
