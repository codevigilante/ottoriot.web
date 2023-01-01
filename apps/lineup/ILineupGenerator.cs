using System;
using models;

namespace apps.lineup
{
    public enum LineupPositions { QB, RB, WR, TE, FLEX, DST }

    public class PositionOption
    {
        public double Weight { get; set; } = 1.0;
        public int Priority { get; set; } = 1;
        public double MinFantasyPoints { get; set; } = 9.0;
    }

    public class YahooOptions
    {
        public PositionOption QB { get; set; } = new PositionOption()
        {
            Priority = 3,
            MinFantasyPoints = 15.0
            //Weight = 0.5
        };
        public PositionOption RB { get; set; } = new PositionOption()
        {
            Priority = 1,
            MinFantasyPoints = 12.0
            //Weight = 0.1
        };
        public PositionOption WR { get; set; } = new PositionOption()
        {
            Priority = 2,
            MinFantasyPoints = 10.0
        };
        public PositionOption TE { get; set; } = new PositionOption()
        {
            Priority = 4,
            MinFantasyPoints = 8.0
            //Weight = 1.5
        };
        public PositionOption DST { get; set; } = new PositionOption()
        {
            Priority = 5,
            MinFantasyPoints = 5.0
        };
    }

    public interface ILineupGenerator
	{
		ILineup Generate(IPlayerPool playerPool);
	}
}

