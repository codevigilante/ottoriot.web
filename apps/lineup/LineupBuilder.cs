using System;
using models;
using services.predictions;
using services.odds;
using apps.lineup.generators;

namespace apps.lineup
{
	public enum GeneratorVersion { RiotGenV0 }

	public class LineupBuilder
	{
		public LineupBuilder()
		{
		}

		public static ILineupGenerator Create(GeneratorVersion version = GeneratorVersion.RiotGenV0)
		{
			switch(version)
			{
				case GeneratorVersion.RiotGenV0:
					return (new RiotGenV0());
				default:
					throw new Exception("try again, idiot");
			}
		}
	}
}

