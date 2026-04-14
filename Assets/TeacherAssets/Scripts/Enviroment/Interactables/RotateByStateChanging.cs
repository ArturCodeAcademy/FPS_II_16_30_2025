using UnityEngine;

public class RotateByStateChanging : MonoBehaviour
{
    [SerializeField] private bool _state;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Quaternion _endRotation;
    [SerializeField] private float _rotationDuration = 1f;

    private float _rotationTimer;

	private void Start()
    {
        _rotationTimer = _state ? _rotationDuration : 0f;
		transform.localRotation = _state ? _endRotation : _startRotation;
	}

    private void Update()
    {
        if (_state && _rotationTimer < _rotationDuration)
        {
            _rotationTimer += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, _rotationTimer / _rotationDuration);
        }
        else if (!_state && _rotationTimer > 0f)
        {
            _rotationTimer -= Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, _rotationTimer / _rotationDuration);
        }
    }
    public void SetState(bool newState)
    {
        _state = newState;
	}

    public void ToggleState()
    {
        _state = !_state;
	}

#if UNITY_EDITOR

    [ContextMenu("Set Current Rotation As Start")]
    private void SetCurrentRotationAsStart()
    {
        _startRotation = transform.localRotation;
    }

    [ContextMenu("Set Current Rotation As End")]
    private void SetCurrentRotationAsEnd()
    {
        _endRotation = transform.localRotation;
    }

#endif
}