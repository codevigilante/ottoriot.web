using System;
namespace models
{
	public class PlayerData
	{
		public PredictionData Projection { get; set; }
		public double ITT { get; set; }
		public bool Locked { get; set; }
		public bool Excluded { get; set; }
		public bool Available { get; set; }

		public PlayerData()
		{
		}
	}
}

