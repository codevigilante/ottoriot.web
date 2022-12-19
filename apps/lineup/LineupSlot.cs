using System;
namespace apps.lineup
{
	public class LineupSlot
	{
		public string Name { get; set; }
		public string Position { get; set; }
		public string Team { get; set; }
		public double FP { get; set; }
		public double Salary { get; set; }
		public string Slot { get; set; } // QB, RB1, WR3, FLEX, etc

		public LineupSlot()
		{
		}
	}
}

