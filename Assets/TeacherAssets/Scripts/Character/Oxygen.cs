using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Oxygen : MonoBehaviour
{
	public float CurrentOxygen => _currentOxygen;
	public float MaxOxygen => _maxOxygenAmount;

    [Header("Oxygen Params")]
    [SerializeField, Min(1)] private float _maxOxygenAmount = 100f;
	[SerializeField, Min(0.1f)] private float _oxygenDepletionRate = 100f / 30f;
	[SerializeField, Min(0.1f)] private float _oxygenReplenishmentRate = 100f / 10f;
	[SerializeField, Min(0f)] private float _pulmonarySpasmPeriod = 2f;
	[SerializeField] private LayerMask[] _noOxygenLayers;

	[Header("Oxygen Events")]
	public UnityEventOxygenPulse OnOxygenPulseChanged;
	public UnityEventOxygen OnOxygenAmountChanged;
	public UnityEventOxygen OnOxygenAmountIncreased;
	public UnityEventOxygen OnOxygenAmountDecreased;
	public UnityEvent OnOxygenDepleted;
	public UnityEvent OnOxygenReplenished;
	public UnityEvent OnPulmonarySpasm;

	private float _currentOxygen;
	private float _spasmTimer = 0;
	private bool _isInNoOxygenZone = false;
	private HashSet<Transform> _noOxygenZones = new HashSet<Transform>();

	private void Awake()
	{
		_currentOxygen = _maxOxygenAmount;
	}

	private void Update()
	{
		if (_isInNoOxygenZone)
		{
			float delta = -_oxygenDepletionRate * Time.deltaTime;
			float previousOxygen = _currentOxygen;
			_currentOxygen = Mathf.Max(0, _currentOxygen + delta);

			if (previousOxygen > 0 && _currentOxygen <= 0)
			{
				OnOxygenDepleted.Invoke();
			}

			if (_currentOxygen <= 0)
			{
				_spasmTimer += Time.deltaTime;

				if (_spasmTimer >= _pulmonarySpasmPeriod)
				{
					OnPulmonarySpasm.Invoke();
					_spasmTimer = 0;
				}

				OnOxygenPulseChanged?.Invoke(new OxygenPulseArgs(Time.deltaTime, _pulmonarySpasmPeriod, _spasmTimer));
			}

			if (delta != 0)
			{
				OnOxygenAmountChanged.Invoke(new OxygenEventArgs(delta, _currentOxygen, _maxOxygenAmount));
				OnOxygenAmountDecreased.Invoke(new OxygenEventArgs(delta, _currentOxygen, _maxOxygenAmount));
			}
		}
		else
		{
			_spasmTimer = 0;
			OnOxygenPulseChanged?.Invoke(new OxygenPulseArgs(0, _pulmonarySpasmPeriod, _spasmTimer));
			float delta = _oxygenReplenishmentRate * Time.deltaTime;
			_currentOxygen = Mathf.Min(_maxOxygenAmount, _currentOxygen + delta);

			if (delta != 0)
			{
				OnOxygenAmountChanged.Invoke(new OxygenEventArgs(delta, _currentOxygen, _maxOxygenAmount));
				OnOxygenAmountIncreased.Invoke(new OxygenEventArgs(delta, _currentOxygen, _maxOxygenAmount));

				if (_currentOxygen >= _maxOxygenAmount)
				{
					OnOxygenReplenished.Invoke();
				}
			}	
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		foreach (var layer in _noOxygenLayers)
		{
			if (((1 << other.gameObject.layer) & layer) != 0)
			{
				_noOxygenZones.Add(other.transform);
				_isInNoOxygenZone = true;
				return;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		foreach (var layer in _noOxygenLayers)
		{
			if (((1 << other.gameObject.layer) & layer) != 0)
			{
				_noOxygenZones.Remove(other.transform);
				if (_noOxygenZones.Count == 0)
					_isInNoOxygenZone = false;
				return;
			}
		}
	}
}
