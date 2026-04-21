using UnityEngine;

public class SlideByStateChange : MonoBehaviour
{
		[SerializeField] private bool _state;
		[SerializeField] private Vector3 _startPosition;
		[SerializeField] private Vector3 _endPosition;
		[SerializeField] private float _slideDuration = 1f;

		private float _slideTimer;

		private void Start()
		{
			_slideTimer = _state ? _slideDuration : 0f;
			transform.localPosition = _state ? _endPosition : _startPosition;
		}

		private void Update()
		{
			if (_state && _slideTimer < _slideDuration)
			{
				_slideTimer += Time.deltaTime;
				transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, _slideTimer / _slideDuration);
			}
			else if (!_state && _slideTimer > 0f)
			{
				_slideTimer -= Time.deltaTime;
				transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, _slideTimer / _slideDuration);
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

		[ContextMenu("Set Current Position As Start")]
		private void SetCurrentPositionAsStart()
		{
			_startPosition = transform.localPosition;
		}

		[ContextMenu("Set Current Position As End")]
		private void SetCurrentPositionAsEnd()
		{
			_endPosition = transform.localPosition;
		}

#endif

}
