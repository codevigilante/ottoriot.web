using System;
using models;

namespace apps.lineup.generators
{
	public class BaseGenerator
	{
        public YahooOptions Options { get; set; } = new YahooOptions();

        public BaseGenerator()
		{
		}

		public BaseGenerator(YahooOptions options)
		{
			Options = options;
		}
	}
}

