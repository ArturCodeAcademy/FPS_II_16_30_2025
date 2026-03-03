using UnityEngine;
using UnityEngine.UI;

public class OxygenUISlider : MonoBehaviour
{
    [SerializeField] private Oxygen _oxygen;
    [SerializeField] private Image _fill;

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
        _fill.fillAmount = args.OxygenLevel / args.MaxOxygenLevel;
    }
}
