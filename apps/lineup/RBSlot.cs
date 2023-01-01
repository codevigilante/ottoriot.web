using System;
namespace apps.lineup
{
	public class RBSlot : RosterMultiple
	{
        public RBSlot(int numSlots, PositionOption options)
            : base(LineupPositions.RB, numSlots, options)
        {
        }
    }
}

