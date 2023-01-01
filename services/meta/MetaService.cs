using System;
using models;

namespace services.meta
{
	public class MetaService : ServiceBase<MetaModel>
	{
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/meta";

        public MetaService()
        {
        }

        public async Task<MetaModel?> Fetch()
        {
            string endpoint = $"{EndpointBase}/meta.json";
            MetaModel? meta = await base.Fetch(endpoint);            

            return (meta);
        }
    }
}

