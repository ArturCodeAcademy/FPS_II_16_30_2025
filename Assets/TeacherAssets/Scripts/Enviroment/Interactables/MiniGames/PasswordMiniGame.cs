using System.Linq;
using TMPro;
using UnityEngine;

public class PasswordMiniGame : MiniGameBase
{
    [SerializeField] private bool _randomPassword;
	[SerializeField, Range(4, 12)] private int _passwordLength = 6;
	[SerializeField] private string _password;

	[Space(3)]
	[SerializeField] TMP_Text _passwordText;

	private string _inputPassword = string.Empty;

	protected override void Awake()
	{
		base.Awake();

		if (_randomPassword)
		{
			_password = string.Join("", Enumerable.Range(0, _passwordLength)
				.Select(_ => Random.Range(0, 10).ToString()));		
		}

		_passwordText.text = string.Empty;
	}

	protected override void Update()
	{
		base.Update();

		if (!_gameInProgress)
			return;

		int lastInputLength = _inputPassword.Length;

		if (Input.GetKeyUp(KeyCode.Keypad1) || Input.GetKeyUp(KeyCode.Alpha1))
		{
			_inputPassword += "1";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.Alpha2))
		{
			_inputPassword += "2";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad3) || Input.GetKeyUp(KeyCode.Alpha3))
		{
			_inputPassword += "3";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad4) || Input.GetKeyUp(KeyCode.Alpha4))
		{
			_inputPassword += "4";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad5) || Input.GetKeyUp(KeyCode.Alpha5))
		{
			_inputPassword += "5";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad6) || Input.GetKeyUp(KeyCode.Alpha6))
		{
			_inputPassword += "6";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad7) || Input.GetKeyUp(KeyCode.Alpha7))
		{
			_inputPassword += "7";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad8) || Input.GetKeyUp(KeyCode.Alpha8))
		{
			_inputPassword += "8";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad9) || Input.GetKeyUp(KeyCode.Alpha9))
		{
			_inputPassword += "9";
		}
		else if (Input.GetKeyUp(KeyCode.Keypad0) || Input.GetKeyUp(KeyCode.Alpha0))
		{
			_inputPassword += "0";
		}
		else if (Input.GetKeyUp(KeyCode.Backspace) && _inputPassword.Length > 0)
		{
			_inputPassword = _inputPassword[..^1];
		}

		if (_inputPassword == _password)
		{
			_gameInProgress = false;
			GameWon?.Invoke();
			Player.Instance.UnblockControll();
			_virtualCamera.Priority = 0;
			_inputPassword = string.Empty;
		}
		else if (_inputPassword.Length == _password.Length)
		{
			_inputPassword = string.Empty;
			GameLost?.Invoke();
		}

		if (lastInputLength != _inputPassword.Length)
		{
			_passwordText.text = _inputPassword;
		}
	}

	public string GetPassword()
	{
		return _password;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (_randomPassword)
		{
			_password = string.Empty;
		}
		else
		{
			for (int i = 0; i < _password.Length; i++)
			{
				if (!char.IsLetterOrDigit(_password[i]))
				{
					_password = _password[..i];
				}
			}
		}
	}

#endif
}
