﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace UDPReciever
{
    public class PostService
    {
        public async void PostItemHttpTask(TemperaturLib.Temperatur temperatur)
        {
            string EventWebApi = "https://goldenice20200505111435.azurewebsites.net/";
            // Measurement.Measurement newMeasure = new Measurement.Measurement(DateTime.Now, "D5.16", "Ozon", 9.12, "Rasp v.1.0");

            using (HttpClient client = new HttpClient())
            {
                string newItemJson = JsonConvert.SerializeObject(temperatur);
                Console.WriteLine("Serialized object: " + newItemJson);
                var content = new StringContent(newItemJson, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(EventWebApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                IEnumerable<string> headers;
                headers = client.DefaultRequestHeaders.GetValues("Accept");
                foreach (var VARIABLE in headers)
                {
                    Console.WriteLine(VARIABLE);
                }
                var response = client.PostAsync("api/Weathers", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseEvent = client.GetAsync("api/Weathers" + temperatur).Result;
                    if (responseEvent.IsSuccessStatusCode)
                    {
                        //var Event = responseEvent.Content.ReadAsStreamAsync<Event>().Result;
                        //   string saveEvent = await responseEvent.Content.ReadAsStringAsync<Event>().Result;

                    }
                }
            }

            //return await GetItemsHttpTask();
        }
    }
}
