using UnityEngine;
using UnityEngine.UI;

public class HealthUISlider : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fill;

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
        _fill.fillAmount = args.CurrentHealth / args.MaxHealth;
    }
}
