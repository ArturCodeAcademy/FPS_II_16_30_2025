using TMPro;
using UnityEngine;

public class HealthUIText : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private string _format = "{0:0}/{1:0}";
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        HealthEventArgs args = new HealthEventArgs
        {
            CurrentHealth = _health.CurrentHealth,
            MaxHealth = _health.MaxHealth
        };
        UpdateUI(args);
    }

    private void OnEnable()
    {
        _health.OnHealthValueChanged.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        _health.OnHealthValueChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(HealthEventArgs args)
    {
        _text.text = string.Format(_format,
            args.CurrentHealth,
            args.MaxHealth,
            args.CurrentHealth / args.MaxHealth * 100);
    }
}
