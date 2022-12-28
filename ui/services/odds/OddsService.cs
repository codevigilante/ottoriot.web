﻿using System;
using ui.services.predictions;
using models;

namespace ui.services.odds
{
	public class OddsService : ServiceBase<OddsModel>
	{
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/odds";

        public OddsService()
        {
        }

        public async Task<OddsModel?> Fetch(int season, int week)
        {

            string endpoint = $"{EndpointBase}/{season}/odds_w{week}.json";
            OddsModel? model = await base.Fetch(endpoint);

            return (model);
        }
    }
}

