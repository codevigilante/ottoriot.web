using System;
namespace apps.lineup
{
	public class SingleSlot : RosterMultiple
	{
        public SingleSlot(LineupPositions position, PositionOption options)
            : base(position, 1, options)
        {
        }
    }
}

