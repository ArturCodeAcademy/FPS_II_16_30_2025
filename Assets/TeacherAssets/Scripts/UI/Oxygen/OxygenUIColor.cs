using UnityEngine;
using UnityEngine.UI;

public class OxygenUIColor : MonoBehaviour
{
    [SerializeField] private Oxygen _oxygen;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;

    private void Start()
    {
        OxygenEventArgs args = new OxygenEventArgs
        (
            0,
            _oxygen.CurrentOxygen,
            _oxygen.MaxOxygen
        );
        UpdateUI(args);
    }

    private void OnEnable()
    {
        _oxygen.OnOxygenAmountChanged.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        _oxygen.OnOxygenAmountChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(OxygenEventArgs args)
    {
        _fill.color = _gradient.Evaluate(args.OxygenLevel / args.MaxOxygenLevel);
    }
}
