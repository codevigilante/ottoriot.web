using System;
using models;

namespace apps.lineup
{
	public abstract class RosterPosition
	{
        public LineupPositions Position { get; set; }
        public List<PlayerData> Selected { get; set; } = new List<PlayerData>();
        public List<PlayerData> OnDeck { get; set; } = new List<PlayerData>();
        public int Priority { get; set; }  // priority is for breaking ties
        public double Weight { get; set; } = 1.0; // weight is for favoring one position or another in the value chain
        public double MinFP { get; set; } = 9.0;
        public int RosterSlots { get; set; }
        public double TotalFP
        {
            get => Selected.Sum(p => p.Projection.FP);
        }
        public double TotalSalary
        {
            get => Selected.Sum(p => p.Projection.Salary);
        }

        public RosterSwap Swap
        {
            get;
            private set;
        }

        public RosterPosition(LineupPositions position, int numSlots, PositionOption options)
        {
            Priority = options.Priority;
            Position = position;
            MinFP = options.MinFantasyPoints;
            Weight = options.Weight;
            RosterSlots = numSlots;
        }

        public abstract bool Stage(PlayerData row);
        public abstract RosterSwap OptimalChoice();
        public abstract void Optimize(RosterSwap swap);
        public abstract bool Refine(double remainingSalary);
    }
}

