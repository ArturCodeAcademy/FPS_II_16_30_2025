using System;
using UnityEngine.Events;

[Serializable]
public class UnityEventOxygen : UnityEvent<OxygenEventArgs> { }

public class OxygenEventArgs
{
	public float Delta { get; private set; }
	public float OxygenLevel { get; private set; }
	public float MaxOxygenLevel { get; private set; }

	public OxygenEventArgs(float delta, float oxygenLevel, float maxOxygenLevel)
	{
		Delta = delta;
		OxygenLevel = oxygenLevel;
		MaxOxygenLevel = maxOxygenLevel;
	}
}