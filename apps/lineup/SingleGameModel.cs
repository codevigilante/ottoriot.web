using System;
namespace apps.lineup
{
	public class SingleGameModel
	{
		public int SlateId { get; set; }
		public int Budget { get; set; }
		public LineupSlot Superstar { get; set; }
		public List<LineupSlot> FlexSlots { get; set; } = new List<LineupSlot>();

		public SingleGameModel()
		{
		}
	}
}

