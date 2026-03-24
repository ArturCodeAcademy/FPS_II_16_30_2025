using System;
using UnityEngine.Events;

[Serializable]
public class UnityEventOxygenPulse : UnityEvent<OxygenPulseArgs> { }

public class OxygenPulseArgs
{
	public float TimeDelta { get; private set; }
	public float TotalTime { get; private set; }
	public float LeftTime { get; private set; }

	public OxygenPulseArgs(float timeDelta, float totalTime, float leftTime)
	{
		TimeDelta = timeDelta;
		TotalTime = totalTime;
		LeftTime = leftTime;
	}
}