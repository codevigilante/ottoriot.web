using System;
using models;

namespace apps.lineup
{
	public class LineupBuilder
	{
        public int NumLineups { get; set; } = 100;

		public LineupBuilder()
		{
		}

        /// <summary>
        /// Accepts a pool of players for a slate and a budget and generates a sorted list of
        /// single game lineups that fit within the budget.
        /// </summary>
        /// <param name="playerPool"></param>
        /// <param name="budget"></param>
        /// <returns>Up to 100 possible combinations.</returns>
        /*public List<SingleGameModel> BuildSingleGame(List<PredictionData> playerPool, int budget = 120)
		{
            List<PredictionData> filtered = playerPool.Where(p => p.FP >= 5.0).OrderByDescending(p => p.Value).ToList();
            int count = 0;
            bool stillViable = true;

            while (count < NumLineups)
            {

            }
		}*/
	}
}

