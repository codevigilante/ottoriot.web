using System;
namespace apps.lineup
{
	public class WRSlot : RosterMultiple
	{
        public WRSlot(int numSlots, PositionOption options)
            : base(LineupPositions.WR, numSlots, options)
        {
        }
    }
}

