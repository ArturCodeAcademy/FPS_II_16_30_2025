using System;
using UnityEngine;
using UnityEngine.Events;

public class SwitchInteractable : MonoBehaviour, IInteractable
{
	[field: SerializeField] public bool Active { get; set; }
	[field: SerializeField] public string MainInformation { get; private set; } = "Switch";
	public string Description => _isOn ? _onInformation : _offInformation;
	[field: SerializeField] public Sprite Icon { get; private set; }

	public event EventHandler StateChanged;

	public UnityEvent OnStateChanged;
	public UnityEvent OnTurnedOn;
	public UnityEvent OnTurnedOff;

	[SerializeField] private bool _isOn;
	[SerializeField] private string _onInformation = "The switch is currently ON.";
	[SerializeField] private string _offInformation = "The switch is currently OFF.";

	public void Interact()
	{
		_isOn = !_isOn;
		StateChanged?.Invoke(this, EventArgs.Empty);

		OnStateChanged?.Invoke();

		if (_isOn)
			OnTurnedOn?.Invoke();
		else
			OnTurnedOff?.Invoke();
	}
}
