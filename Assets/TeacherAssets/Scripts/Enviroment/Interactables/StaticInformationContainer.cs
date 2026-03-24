using System;
using UnityEngine;

public class StaticInformationContainer : MonoBehaviour, IInformationContainer
{
	[field:SerializeField] public string MainInformation { get; private set; }
	[field:SerializeField, TextArea] public string Description { get; private set; }
	[field:SerializeField] public Sprite Icon { get; private set; }

	public event EventHandler StateChanged;
}
