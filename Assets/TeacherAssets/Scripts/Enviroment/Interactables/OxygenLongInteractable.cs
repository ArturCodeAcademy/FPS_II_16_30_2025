using System;
using UnityEngine;

public class OxygenLongInteractable : MonoBehaviour, ILongInteractable
{
	public float Progress => _currentAmount / _maxAmount;
	[field: SerializeField] public bool Active { get; set; }
	[field: SerializeField] public string MainInformation { get; set; }
	public string Description => $"{_currentAmount:F1}/{_maxAmount:F1}";

	[field: SerializeField] public Sprite Icon { get; set; }

	public event EventHandler ProgressChanged;
	public event EventHandler StateChanged;

	[SerializeField, Min(0f)] private float _oxygenRestorationRate = 0;
	[SerializeField, Min(0.1f)] private float _maxAmount = 10f;
	[SerializeField, Min(0.1f)] private float _oxygenProvidingRate = 3;

	private float _currentAmount;
	private bool _isBeingUsed = false;

	private void Awake()
	{
		_currentAmount = _maxAmount;
	}

	private void Update()
	{
		if (_currentAmount == _maxAmount)
			return;

		if (!_isBeingUsed)
		{
			_currentAmount = Mathf.Min(_currentAmount + _oxygenRestorationRate * Time.deltaTime, _maxAmount);
			if (_currentAmount > 0)
			{
				StateChanged?.Invoke(this, EventArgs.Empty);
				ProgressChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public void Interact()
	{
		float amountToRestore = Mathf.Min(_oxygenProvidingRate * Time.deltaTime, _currentAmount);
		float restored = Player.Instance.Oxygen.AddOxygen(amountToRestore);
		_currentAmount -= restored;

		if (restored > 0)
		{
			ProgressChanged?.Invoke(this, EventArgs.Empty);
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		_isBeingUsed = true;
	}

	public void StopInteraction()
	{
		_isBeingUsed = false;
	}
}
