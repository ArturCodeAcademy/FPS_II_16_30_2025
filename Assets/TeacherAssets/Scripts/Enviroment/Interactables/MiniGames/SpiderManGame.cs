using UnityEngine;

public class SpiderManGame : MiniGameBase
{
    [Space(2)]
	[SerializeField] private float _aMin = 0f;
    [SerializeField] private float _aMax = 1f;
    [SerializeField] private float _bMin = 3f;
    [SerializeField] private float _bMax = 7f;
    [SerializeField, Min(0.001f)] private float _aproximationThreshold = 0.1f;
	[SerializeField] private float _stepAMultiplier = 1f;
	[SerializeField] private float _stepBMultiplier = 1f;
	[SerializeField, Min(3)] private int _segmentAmount = 25;
    [SerializeField, Min(0.1f)] private float _lineWidth = 2f;

    [Space(2)]
    [SerializeField] private LineRenderer _mainLine;
    [SerializeField] private LineRenderer _controllableLine;

	private float _aMain;
    private float _bMain;
    private float _aControllable;
    private float _bControllable;
	private float _time = 0f;
    private float _segmentLength = 0f;

	public override void Interact()
	{
		base.Interact();

		_aMain = Random.Range(_aMin, _aMax);
		_bMain = Random.Range(_bMin, _bMax);
		_mainLine.positionCount = _segmentAmount;
		_controllableLine.positionCount = _segmentAmount;
		_segmentLength = _lineWidth / (_segmentAmount - 1);

		UpdateLine();
	}

	protected override void Update()
	{
		base.Update();

		if (!_gameInProgress)
			return;

		ApplyChanges();
		UpdateLine();
		
		if (Mathf.Abs(_aControllable - _aMain) < _aproximationThreshold && Mathf.Abs(_bControllable - _bMain) < _aproximationThreshold)
		{
			GameWon?.Invoke();
			Player.Instance.UnblockControll();
			_virtualCamera.Priority = 0;
			Active = false;
			_gameInProgress = false;
		}
	}

	private void ApplyChanges()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_bControllable += Time.deltaTime * _stepBMultiplier;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			_bControllable -= Time.deltaTime * _stepBMultiplier;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_aControllable -= Time.deltaTime * _stepAMultiplier;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			_aControllable += Time.deltaTime * _stepAMultiplier;
		}

		_aControllable = Mathf.Clamp(_aControllable, _aMin, _aMax);
		_bControllable = Mathf.Clamp(_bControllable, _bMin, _bMax);
	}

	private void UpdateLine()
    {
        _time += Time.deltaTime;
        for (int i = 0; i < _segmentAmount; i++)
        {
            float x = i * _segmentLength;
			float y = _aMain * Mathf.Sin(x * _bMain + _time) ;
            _mainLine.SetPosition(i, new Vector3(x, y, 0f));
            
            y = _aControllable * Mathf.Sin(x * _bControllable + _time);
            _controllableLine.SetPosition(i, new Vector3(x, y, 0f));
		}
	}
}
