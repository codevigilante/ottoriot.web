using System;

namespace ui.services.predictions
{
	public class PredictionsService : ServiceBase
	{
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/predictions";

        public PredictionsService()
		{
		}

		public async Task<PredictionModel> Fetch(int season, int week)
		{
            string endpoint = $"{EndpointBase}/{season}/pred_w{week}.json";
            PredictionModel model = await base.Fetch<PredictionModel>(endpoint);
            
            return (model);
        }
	}
}

