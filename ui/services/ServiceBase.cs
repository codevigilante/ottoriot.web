using System;
using Newtonsoft.Json;
using ui.services.predictions;

namespace ui.services
{
	public class ServiceBase<T> where T : class
	{
        private static readonly HttpClient Client = new HttpClient();

        public double ExpiresMinutes { get; set; } = 30;

        protected T? Model = null;
        protected DateTime Expires = DateTime.Now;

        public ServiceBase(double minutesUntilExpire = 30)
		{
            ExpiresMinutes = minutesUntilExpire;
		}

        public async Task<T?> Fetch(string endpoint)
        {
            if (Model == null || Expires <= DateTime.Now)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("User-Agent", "ottoriot web");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Access-Control-Allow-Credentials", "false");
                request.Headers.Add("Access-Control-Allow-Origin", "true");

                HttpResponseMessage response = await Client.SendAsync(request);
                Model = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                Expires = DateTime.Now.AddMinutes(ExpiresMinutes);
            }

            return (Model);
        }
    }
}

