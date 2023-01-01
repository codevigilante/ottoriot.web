using System;
using models;
namespace apps.lineup
{
	public class SlateLineup : Lineup
	{
        public override string Description { get; set; } = "Just a regular, typical Yahoo daily lineup";
        public RosterPosition QB { get; set; }
        public RosterPosition RB { get; set; }
        public RosterPosition WR { get; set; }
        public RosterPosition TE { get; set; }
        public RosterPosition DST { get; set; }
        public override double TotalProjectedFP
        {
            get => QB.TotalFP + RB.TotalFP + WR.TotalFP + TE.TotalFP + DST.TotalFP;
            set => base.TotalProjectedFP = value;
        }
        public override double TotalSalary
        {
            get => QB.TotalSalary + RB.TotalSalary + WR.TotalSalary + TE.TotalSalary + DST.TotalSalary;
            set => base.TotalSalary = value;
        }

        public SlateLineup()
        {
        }

        public bool Optimize()
        {
            bool optimal = false;
            //int count = 0;

            while (!optimal)
            {
                //Console.WriteLine($"************* Optimize pass {count++} *****************");

                //Print();

                List<Tuple<RosterPosition, RosterSwap>> swaps = new List<Tuple<RosterPosition, RosterSwap>>();

                AddToSwap(QB, swaps);
                AddToSwap(RB, swaps);
                AddToSwap(WR, swaps);
                AddToSwap(TE, swaps);
                AddToSwap(DST, swaps);

                swaps = swaps.OrderBy(s => s.Item2.MinValueDiff).ThenBy(s => s.Item1.Priority).ToList();

                int index = 0;
                bool swapFound = false;

                while (swaps.Count > 0 && index < swaps.Count)
                {
                    Tuple<RosterPosition, RosterSwap> candidate = swaps[index++];

                    if (candidate.Item2 == null)
                    {
                        continue;
                    }

                    double newSalary = TotalSalary + candidate.Item2.SalaryIncrease;

                    if (newSalary > SalaryCap)
                    {
                        swaps.Remove(candidate);

                        continue;
                    }

                    candidate.Item1.Optimize(candidate.Item2);
                    swapFound = true;

                    //Console.WriteLine($"Swapping position {candidate.Item1.Position}");

                    break;
                }

                if (TotalSalary == SalaryCap || !swapFound)
                {
                    optimal = true;
                }
            }

            List<RosterPosition> positions = new List<RosterPosition>()
            {
                QB, RB, WR, TE, DST
            };

            positions = positions.OrderBy(p => p.Priority).ToList();

            bool refined = false;

            while (!refined)
            {
                double salaryLeft = SalaryCap - TotalSalary;

                if (salaryLeft > 0)
                {
                    bool success = false;

                    foreach (RosterPosition pos in positions)
                    {
                        success = pos.Refine(salaryLeft);

                        if (success)
                        {
                            break;
                        }
                    }

                    if (success)
                    {
                        continue;
                    }
                }

                refined = true;
            }

            Players = new List<PlayerData>();
            Players.AddRange(QB.Selected);
            Players.AddRange(RB.Selected);
            Players.AddRange(WR.Selected);
            Players.AddRange(TE.Selected);
            Players.AddRange(DST.Selected);

            return (optimal);
        }

        public bool Stage(PlayerData row)
        {
            switch (row.Projection.Position)
            {
                case "QB":
                    return (QB.Stage(row));
                case "RB":
                    return (RB.Stage(row));
                case "WR":
                    return (WR.Stage(row));
                case "TE":
                    return (TE.Stage(row));
                case "DST":
                    return (DST.Stage(row));
                default:
                    throw new Exception($"Unrecognized position in projection row: '{row.Projection.Position}'");
            }
        }

        public void Print()
        {
            Console.WriteLine($"<<<<< {Description} >>>>>");
            Print(QB);
            Print(RB);
            Print(WR);
            Print(TE);
            Print(DST);
            Console.WriteLine($"Total FP: {TotalProjectedFP:0.##} @ ${TotalSalary}");
            Console.WriteLine();
        }

        protected void Print(RosterPosition slot)
        {
            foreach (PlayerData p in slot.Selected)
            {
                Console.WriteLine($"{slot.Position,-3}: {p.Projection.Name,-25} {p.Projection.Team,-3} {p.Projection.FP,-6} ${p.Projection.Salary,-4} {p.Projection.Value,-6:0.###}");
            }
        }

        private void AddToSwap(RosterPosition position, List<Tuple<RosterPosition, RosterSwap>> swaps)
        {
            RosterSwap swap = position.OptimalChoice();

            if (swap == null)
            {
                return;
            }

            swaps.Add(new Tuple<RosterPosition, RosterSwap>(position, swap));
        }
    }
}

