using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameBase : MonoBehaviour, IInteractable
{
	[field: SerializeField] public bool Active { get; set; }
	[field: SerializeField] public virtual string MainInformation { get; set; }
	[field: SerializeField] public virtual string Description { get; set; }
	[field: SerializeField] public virtual Sprite Icon { get; set; }

	public event EventHandler StateChanged;
	public UnityEvent GameWon;
	public UnityEvent GameLost;
	public UnityEvent GameStarted;
	public UnityEvent GameEnded;
	public UnityEvent GameDropped;

	[SerializeField] protected CinemachineVirtualCamera _virtualCamera;

	protected bool _gameInProgress = false;

	protected virtual void Awake()
	{
		_virtualCamera.Priority = 0;
	}

	public virtual void Interact()
	{
		_gameInProgress = true;
		GameStarted?.Invoke();
		Player.Instance.BlockControll();
		_virtualCamera.Priority = Player.Instance.MainCameraPriority + 1;
	}

	protected virtual void Update()
	{
		if (!_gameInProgress)
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			_gameInProgress = false;
			GameDropped?.Invoke();
			Player.Instance.UnblockControll();
			_virtualCamera.Priority = 0;
		}
	}
}
