using System;
using ui.services.predictions;

namespace ui.services.odds
{
	public class OddsService : ServiceBase
	{
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/odds";

        public OddsService()
        {
        }

        public async Task<OddsModel> Fetch(int season, int week)
        {
            string endpoint = $"{EndpointBase}/{season}/odds_w{week}.json";
            OddsModel model = await base.Fetch<OddsModel>(endpoint);

            return (model);
        }
    }
}

