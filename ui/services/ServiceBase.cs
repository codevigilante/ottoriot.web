﻿using System;
using Newtonsoft.Json;
using ui.services.predictions;

namespace ui.services
{
	public class ServiceBase
	{
        private static readonly HttpClient Client = new HttpClient();

        public ServiceBase()
		{
		}

        public async Task<T> Fetch<T>(string endpoint)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "ottoriot web");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Access-Control-Allow-Credentials", "false");
            request.Headers.Add("Access-Control-Allow-Origin", "true");

            HttpResponseMessage response = await Client.SendAsync(request);
            T? model = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            return (model);
        }
    }
}
