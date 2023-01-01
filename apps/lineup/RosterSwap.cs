using System;
namespace apps.lineup
{
	public class RosterSwap
	{
        public int OnDeckIndex { get; set; }
        public int SelectedIndex { get; set; }
        public double MinValueDiff { get; set; } = 99.0;
        public double SalaryIncrease { get; set; }
    }
}

