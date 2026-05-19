using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

	[field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }
	[field: SerializeField] public int MainCameraPriority { get; private set; } = 10;

	[field: Header("Components")]
	[field: SerializeField] public Camera Camera { get; private set; }
	[field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
	[field: SerializeField] public CharacterController CharacterController { get; private set; }
	[field: SerializeField] public CharacterGravityController GravityController { get; private set; }
	[field: SerializeField] public PlayerCrouch Crouch { get; private set; }
	[field: SerializeField] public PlayerMovement Movement { get; private set; }
	[field: SerializeField] public PlayerView View { get; private set; }
	[field: SerializeField] public Oxygen Oxygen { get; private set; }

	private void Awake()
    {
        Instance = this;
    }

	public void BlockControll()
	{
		GravityController.enabled = false;
		Crouch.enabled = false;
		View.enabled = false;
		Movement.enabled = false;
	}

	public void UnblockControll()
	{
		GravityController.enabled = true;
		Crouch.enabled = true;
		View.enabled = true;
		Movement.enabled = true;

		VirtualCamera.Priority = MainCameraPriority;
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		Camera = GetComponentInChildren<Camera>();
		CharacterController = GetComponent<CharacterController>();
		GravityController = GetComponent<CharacterGravityController>();
		Crouch = GetComponent<PlayerCrouch>();
		View = GetComponentInChildren<PlayerView>();
		Oxygen = GetComponentInChildren<Oxygen>();
		Movement = GetComponent<PlayerMovement>();
		VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
	}

#endif
}
