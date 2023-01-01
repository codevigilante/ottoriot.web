using System;
namespace apps.lineup
{
	public class TESlot : RosterMultiple
	{
        public TESlot(int numSlots, PositionOption options)
            : base(LineupPositions.TE, numSlots, options)
        {
        }
    }
}

