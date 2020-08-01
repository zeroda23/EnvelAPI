using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public class CloudantRepository
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _dbName;

        public CloudantRepository(IHttpClientFactory factory, string dbName)
        {
            _factory = factory;
            _dbName = dbName;
        }

        public async Task<dynamic> GetAll()
        {
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.GetAsync(_client.BaseAddress + _dbName + "/_all_docs?include_docs=true");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + _dbName, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try GET request again
                {
                    response = await _client.GetAsync(_client.BaseAddress + _dbName + "/_all_docs?include_docs=true");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }

            }

            string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }

        public async Task<dynamic> GetByKey(string key)
        {
            var _client = _factory.CreateClient("cloudant");
            var response = await _client.GetAsync(_client.BaseAddress + _dbName + "/" + key);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + _dbName, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try GET request again
                {
                    response = await _client.GetAsync(_client.BaseAddress + _dbName + "/" + key);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }

            }

            string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = msg });
        }

        public async Task<dynamic> Create(string jsonInString)
        {
            var _client = _factory.CreateClient("cloudant");

            var response = await _client.PostAsync(_client.BaseAddress + _dbName, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + _dbName, contents); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try POST request again
                {
                    response = await _client.PostAsync(_client.BaseAddress + _dbName, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        return responseJson;
                    }
                }

            }

            string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
        }

        public async Task<dynamic> Update(string jsonInString)
        {
            var _client = _factory.CreateClient("cloudant");
            var contents = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response =  await _client.PostAsync(_client.BaseAddress + _dbName, contents);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }
            else if (Equals(response.ReasonPhrase, "Object Not Found")) //need to create database
            {
                var contents1 = new StringContent("", Encoding.UTF8, "application/json");
                response = await _client.PutAsync(_client.BaseAddress + _dbName, contents1); //creating database using PUT request
                if (response.IsSuccessStatusCode) //if successful, try POST request again
                {
                    response = await _client.PostAsync(_client.BaseAddress + _dbName, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        return responseJson;
                    }
                }

            }

            string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
            Console.WriteLine(msg);
            return JsonConvert.SerializeObject(new { msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase });
        }
    }
}
