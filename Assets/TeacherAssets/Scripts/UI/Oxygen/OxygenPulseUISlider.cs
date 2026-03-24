using UnityEngine;
using UnityEngine.UI;

public class OxygenPulseUISlider : MonoBehaviour
{
	[SerializeField] private Oxygen _oxygen;
	[SerializeField] private Image _fill;

	private void Start()
	{
		//TurnOffSlider();
	}

	private void OnEnable()
	{
		_oxygen.OnOxygenDepleted.AddListener(TurnOnSlider);
		_oxygen.OnOxygenAmountIncreased.AddListener(TurnOffSlider);
		_oxygen.OnOxygenPulseChanged.AddListener(UpdateSliderAmountUI);
	}

	private void OnDisable()
	{
		_oxygen.OnOxygenDepleted.RemoveListener(TurnOnSlider);
		_oxygen.OnOxygenAmountIncreased.RemoveListener(TurnOffSlider);
		_oxygen.OnOxygenPulseChanged.RemoveListener(UpdateSliderAmountUI);
	}

	private void TurnOnSlider()
	{
		_fill.enabled = true;
	}

	private void TurnOffSlider(OxygenEventArgs _)
	{
		_fill.enabled = false;
	}

	private void UpdateSliderAmountUI(OxygenPulseArgs args)
	{
		_fill.fillAmount = args.LeftTime / args.TotalTime;
	}
}