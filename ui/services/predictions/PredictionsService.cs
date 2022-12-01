using System;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

namespace ui.services.predictions
{
	public class PredictionsService
	{
		private static readonly HttpClient Client = new HttpClient();
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/predictions";

        public PredictionsService()
		{
		}

		public async Task<PredictionModel> Fetch(int season, int week)
		{
            string endpoint = $"{EndpointBase}/{season}/pred_w{week}.json";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "ottoriot web");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Access-Control-Allow-Credentials", "false");
            request.Headers.Add("Access-Control-Allow-Origin", "true");

            HttpResponseMessage response = await Client.SendAsync(request);
            PredictionModel model = JsonConvert.DeserializeObject<PredictionModel>(await response.Content.ReadAsStringAsync());

            return (model);
        }
	}
}

