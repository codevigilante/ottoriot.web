using System;
using models;
namespace apps.lineup
{
	public abstract class RosterMultiple : RosterPosition
	{
        public RosterMultiple(LineupPositions position, int numSlots, PositionOption options)
            : base(position, numSlots, options)
        {

        }

        public override bool Stage(PlayerData row)
        {
            if (row.Projection.FP <= MinFP)
            {
                return (false);
            }

            if (Selected.Count == RosterSlots)
            {
                OnDeck.Add(row);
            }
            else
            {
                Selected.Add(row);
            }

            if (Selected.Count == RosterSlots)
            {
                MinFP = Selected.Min(p => p.Projection.FP);
            }

            return (true);
        }

        public override RosterSwap OptimalChoice()
        {
            if (OnDeck == null || OnDeck.Count == 0)
            {
                return (null);
            }

            RosterSwap swap = new RosterSwap();

            //Console.WriteLine($"---- {Position} ----");

            OnDeck = OnDeck.OrderByDescending(p => p.Projection.Value).ThenBy(p => p.Projection.FP).ToList();

            foreach (PlayerData onDeck in OnDeck)
            {
                //Console.WriteLine($"OnDeck: {onDeck.Name} {onDeck.FantasyPoints} {onDeck.CalculatedValue:0.###}");
                foreach (PlayerData selected in Selected)
                {
                    //Console.WriteLine($"-- {selected.Name} {selected.FantasyPoints} {selected.CalculatedValue:0.###}");
                    if (onDeck.Projection.FP > selected.Projection.FP)
                    {
                        double valueDiff = (selected.Projection.Value - onDeck.Projection.Value) * Weight;

                        //Console.WriteLine($"==> {valueDiff:0.###}");

                        if (valueDiff <= swap.MinValueDiff)
                        {
                            swap.MinValueDiff = valueDiff;
                            swap.SelectedIndex = Selected.IndexOf(selected);
                            swap.OnDeckIndex = OnDeck.IndexOf(onDeck);
                            swap.SalaryIncrease = onDeck.Projection.Salary - selected.Projection.Salary;
                        }
                    }
                }
            }

            if (swap != null)
            {
                PlayerData current = Selected[swap.SelectedIndex];
                PlayerData incoming = OnDeck[swap.OnDeckIndex];

                //Console.WriteLine($"BEST SWAP: {current.Projection.Name} for {incoming.Projection.Name} {swap.MinValueDiff:0.###}");
            }

            return (swap);
        }

        public override void Optimize(RosterSwap swap)
        {
            MakeSwap(swap.SelectedIndex, swap.OnDeckIndex);
        }

        public override bool Refine(double remainingSalary)
        {
            OnDeck = OnDeck.OrderByDescending(p => p.Projection.Value).ThenBy(p => p.Projection.FP).ToList();

            foreach (PlayerData onDeck in OnDeck)
            {
                foreach (PlayerData selected in Selected)
                {
                    if (onDeck.Projection.FP > selected.Projection.FP)
                    {
                        double salaryDiff = onDeck.Projection.Salary - selected.Projection.Salary;

                        if (salaryDiff <= remainingSalary)
                        {
                            //Console.WriteLine($"Swapping: {selected.Projection.Name} for {onDeck.Projection.Name} {salaryDiff} <= {remainingSalary}");

                            MakeSwap(Selected.IndexOf(selected), OnDeck.IndexOf(onDeck));

                            return (true);
                        }
                    }
                }
            }

            return (false);
        }

        private void MakeSwap(int selectedIndex, int onDeckIndex)
        {
            PlayerData old = Selected[selectedIndex];

            Selected.RemoveAt(selectedIndex);
            Selected.Add(OnDeck[onDeckIndex]);
            OnDeck.RemoveAt(onDeckIndex);
            OnDeck.Add(old);

            MinFP = Selected.Min(p => p.Projection.FP);
            OnDeck = OnDeck.Where(p => p.Projection.FP > MinFP)?.ToList();
        }
    }
}

