using System;
using models;
namespace apps.lineup
{
	public interface ILineup
	{
        List<PlayerData> Players { get; set; }
        double TotalSalary { get; set; }
        double TotalProjectedFP { get; set; }
        int SalaryCap { get; set; }
        public string Description { get; set; }
    }
}

