using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsInfoUI : MonoBehaviour
{
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private TMP_Text _infoText;
	[SerializeField] private Image _iconImage;

	[Space(3)]
	[SerializeField] private PlayerInteractor _playerInteractor;

	private IInformationContainer? _informationContainer;

	private void Awake()
	{
		UpdateUI();
	}

	private void OnEnable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.ObjectWithInfoChanged += OnInformationContainerChanged;
	}

	private void OnDisable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.ObjectWithInfoChanged -= OnInformationContainerChanged;
	}

	private void OnInformationContainerChanged(IInformationContainer? obj)
	{
		if (_informationContainer is not null)
			_informationContainer.StateChanged -= UpdateUI;

		_informationContainer = obj;

		if (_informationContainer is not null)
			_informationContainer.StateChanged += UpdateUI;

		UpdateUI(obj);
	}

	private void UpdateUI(object sender, EventArgs e)
	{
		UpdateUI(_informationContainer);
	}

	private void UpdateUI(IInformationContainer? obj = null)
	{
		_nameText.text = obj?.MainInformation ?? string.Empty;
		_infoText.text = obj?.Description ?? string.Empty;
		_iconImage.sprite = obj?.Icon;
	}
}
