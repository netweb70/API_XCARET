using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Domain
{
    public class GenericRepositoryBase : Utilities, IGenericRepositoryBase
    {
        private readonly ILogger<GenericRepositoryBase> _logger;

        public GenericRepositoryBase(ILogger<GenericRepositoryBase> logger)
        {
            _logger = logger;
        }

        public async Task<List<Entrie>> GetNodoHTTPS(bool boleano, string apiUrl)
        {
            List<Entrie> entries = new List<Entrie>();
            List<Entrie> items = new List<Entrie>();

            try
            {
                HttpResponseMessage response = await API_Response(apiUrl);

                var responseContent = await response.Content.ReadAsStringAsync();
                MainJson mainJson = JsonConvert.DeserializeObject<MainJson>(responseContent);
                entries = mainJson.entries;

                items = entries.Where(a => a.HTTPS == boleano).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return items;
        }

        public async Task<List<string>> GetCategory(string apiUrl)
        {
            List<Entrie> entries = new List<Entrie>();
            List<Entrie> items = new List<Entrie>();
            List<string> list = new List<string>();

            try
            {
                HttpResponseMessage response = await API_Response(apiUrl);

                var responseContent = await response.Content.ReadAsStringAsync();

                MainJson mainJson = JsonConvert.DeserializeObject<MainJson>(responseContent);
                entries = mainJson.entries;

                items = entries.DistinctBy(a => a.Category).ToList();
                foreach (Entrie entrie in items)
                {
                    list.Add(entrie.Category);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return list;
        }

        //private async Task<HttpResponseMessage> API_Response(string apiUrl)
        //{
        //    string sLogErr = string.Empty;

        //    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = null;

        //        response = await client.GetAsync(apiUrl);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            sLogErr = (int)response.StatusCode + "-" + response.StatusCode.ToString();
        //            _logger.LogError(sLogErr);
        //            throw new Exception(sLogErr);
        //        }
        //        return response;
        //    }
        //}

    }
}
