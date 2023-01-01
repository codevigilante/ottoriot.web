using System;
using models;

namespace apps.lineup.generators
{
	public class RiotGenV0 : BaseGenerator, ILineupGenerator
	{
        public RiotGenV0()
		{			
		}

        public ILineup Generate(IPlayerPool playerPool)
        {
            IEnumerable<PlayerData> sorted = playerPool.Players
                                                       .Where(p => p.Available && !p.Excluded && p.Projection.FP > 0 && p.Projection.Salary > 0)
                                                       .OrderByDescending(p => p.Projection.Value);
            ILineup lineup = new Lineup();
            List<SlateLineup> slateLineup = new List<SlateLineup>()
            {
                new SlateLineup()
                {
                    Description = "RB FLEX",
                    QB = new SingleSlot(LineupPositions.QB, Options.QB),
                    RB = new RBSlot(3, Options.RB),
                    WR = new WRSlot(3, Options.WR),
                    TE = new SingleSlot(LineupPositions.TE, Options.TE),
                    DST = new SingleSlot(LineupPositions.DST, Options.DST)
                },
                new SlateLineup()
                {
                    Description = "WR FLEX",
                    QB = new SingleSlot(LineupPositions.QB, Options.QB),
                    RB = new RBSlot(2, Options.RB),
                    WR = new WRSlot(4, Options.WR),
                    TE = new SingleSlot(LineupPositions.TE, Options.TE),
                    DST = new SingleSlot(LineupPositions.DST, Options.DST)
                },
                new SlateLineup()
                {
                    Description = "TE FLEX",
                    QB = new SingleSlot(LineupPositions.QB, Options.QB),
                    RB = new RBSlot(2, Options.RB),
                    WR = new WRSlot(3, Options.WR),
                    TE = new TESlot(2, Options.TE),
                    DST = new SingleSlot(LineupPositions.DST, Options.DST)
                }
            };

            foreach (PlayerData row in sorted)
            {
                foreach (SlateLineup sl in slateLineup)
                {
                    sl.Stage(row);
                }
            }

            foreach (SlateLineup sl in slateLineup)
            {
                sl.Optimize();
            }

            lineup = slateLineup.OrderByDescending(l => l.TotalProjectedFP).Select(l => l as ILineup).First();

            return (lineup);
        }
    }
}

