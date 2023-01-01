using System;
using models;
namespace apps.lineup
{
	public class Lineup : ILineup
	{
        public List<PlayerData> Players
        {
            get;
            set;
        }
        public virtual double TotalSalary { get; set; }
        public virtual double TotalProjectedFP { get; set; }
        public int SalaryCap { get; set; } = 200;
        public virtual string Description { get; set; }
    }
}

