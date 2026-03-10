using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractIconUI : MonoBehaviour
{
	[SerializeField] private Image _interactIcon;
	[SerializeField] private Color _activeColor = Color.white;
	[SerializeField] private Color _inactiveColor = Color.gray;

	[Space(3)]
	[SerializeField] private PlayerInteractor _playerInteractor;

	private IInteractable _interactable;

	private void Awake()
	{
		UpdateUI();
	}

	private void OnEnable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.InteractableChanged += UpdateUI;
	}

	private void OnDisable()
	{
		if (_playerInteractor is not null)
			_playerInteractor.InteractableChanged -= UpdateUI;
	}

	private void UpdateUI(IInteractable? interactable = null)
	{
		_interactIcon.gameObject.SetActive(interactable is not null);

		if (_interactable is not null)
		{
			_interactable.StateChanged -= SetColor;
		}

		_interactable = interactable;
		if (_interactable is not null)
		{
			_interactable.StateChanged += SetColor;
			SetColor();
		}
	}

	private void SetColor(object sender = null, EventArgs e = null)
	{
		if (_interactable is not null)
			_interactIcon.color = _interactable?.Active ?? false ? _activeColor : _inactiveColor;
	}
}