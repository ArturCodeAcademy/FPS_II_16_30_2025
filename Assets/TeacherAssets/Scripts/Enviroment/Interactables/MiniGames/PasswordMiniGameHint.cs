using TMPro;
using UnityEngine;

public class PasswordMiniGameHint : MonoBehaviour
{
	[SerializeField] private PasswordMiniGame _miniGame;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		_text.text = _miniGame.GetPassword();
	}
}
