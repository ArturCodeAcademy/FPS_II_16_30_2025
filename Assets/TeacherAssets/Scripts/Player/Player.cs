using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

	[field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }

	
	[field: Header("Components")]
	[field: SerializeField] public Camera Camera { get; private set; }
	[field: SerializeField] public CharacterController CharacterController { get; private set; }

	private void Awake()
    {
        Instance = this;
    }

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		Camera = GetComponentInChildren<Camera>();
		CharacterController = GetComponent<CharacterController>();
	}

#endif
}
