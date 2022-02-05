using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Enums;
using Tura.AddressBook.Interfaces.Repositories;

namespace Tura.AddressBook.Repositories
{
    public class RabbitMQApiRepository : IRabbitMQApiRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RabbitMQApiRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void SendLocationReport()
        {
            using (var client = _httpClientFactory.CreateClient(HttpClients.RabbitMQAPI))
            {

                using (var response = client.PostAsync($"{client.BaseAddress}LocationReports", null))
                {
                    if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"SendReport - RMQ Api Servis Hatası : {JsonConvert.SerializeObject(response.Result.Content)}");
                    }
                }
            }
        }
    }
}