using System;
using models;

namespace ui.services.meta
{
	public class MetaService : ServiceBase
	{
        private MetaModel _meta = null;
        private static readonly string EndpointBase = "https://ottoriot.blob.core.windows.net/meta";

        public MetaService()
        {
        }

        public async Task<MetaModel> Fetch()
        {
            if (_meta == null)
            {
                string endpoint = $"{EndpointBase}/meta.json";
                _meta = await base.Fetch<MetaModel>(endpoint);
            }            

            return (_meta);
        }
    }
}

