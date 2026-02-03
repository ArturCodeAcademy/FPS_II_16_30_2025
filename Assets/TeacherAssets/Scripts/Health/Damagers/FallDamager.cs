using UnityEngine;

[RequireComponent(typeof(CharacterGravityController))]
public class FallDamager : MonoBehaviour
{
	[SerializeField, Min(0)] private float _minFallHeight = 5f;
	[SerializeField, Min(0)] private float _damagePerUnit = 10f;

	[SerializeField] private CharacterGravityController _gravityController;
	[SerializeField] private Health _health;

	private void OnEnable()
	{
		_gravityController.Landed += OnLanded;
	}

	private void OnDisable()
	{
		_gravityController.Landed -= OnLanded;
	}

	private void OnLanded(object sender, LandEventArgs args)
	{
		float heightFallen = args.FallStartPosition.y - args.LandPosition.y;
		if (heightFallen >= _minFallHeight)
		{
			float damage = (heightFallen - _minFallHeight) * _damagePerUnit;
			_health.Hit(damage);
		}
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		_health ??= GetComponent<Health>();
		_gravityController ??= GetComponent<CharacterGravityController>();
	}

#endif
}
