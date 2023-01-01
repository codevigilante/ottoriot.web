using System;
namespace models
{
	public interface IPlayerPool
	{
		List<PlayerData> Players { get; }
		StartTimes Slates { get; }

		void ToggleStartTime(StartTime start);

    }
}

