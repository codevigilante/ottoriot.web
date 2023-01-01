using System;
namespace models
{
	public class PlayerPool : IPlayerPool
	{
        public List<PlayerData> Players { get; private set; }
        public StartTimes Slates { get; private set; }

        public PlayerPool()
		{
			Players = new List<PlayerData>();
			Slates = new StartTimes();
		}

		public PlayerPool(OddsModel odds, PredictionModel predictions)
			: this()
		{
			Populate(odds, predictions);
		}

		public void Populate(OddsModel odds, PredictionModel predictions)
		{
			if (odds == null || predictions == null)
			{
				return;
			}

			Slates = odds.FilterStartTimes();
			Players = new List<PlayerData>();

			foreach(PredictionData projection in predictions.Projections)
			{
				OddsData? gameData = odds.GetData(projection.Team);

				if (gameData == null)
				{
					continue;
				}

				PlayerData player = new PlayerData()
				{
					Projection = projection,
					ITT = (gameData.Home == projection.Team) ? gameData.HomeITT : gameData.AwayITT,
					Locked = false,
					Excluded = false,
					Available = true
				};

				Players.Add(player);
			}

			UpdateAvailable();
        }

		public void ToggleStartTime(StartTime start)
		{
			start.Selected = !start.Selected;
			UpdateAvailable();
		}

		protected void UpdateAvailable()
		{
			Players.ForEach(p =>
			{
				StartTime? gameTime = Slates.SingleOrDefault(s => s.Start == p.Projection.GameTime);

				if (gameTime != null && gameTime.Selected)
				{
					p.Available = true;
				}
				else
				{
					p.Available = false;
				}
			});
		}
    }
}

