using TMPro;
using UnityEngine;

public class OxygenUIText : MonoBehaviour
{
    [SerializeField] private Oxygen _oxygen;
    [SerializeField] private string _format = "{0:0}/{1:0}";
    [SerializeField] private TMP_Text _text;

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
        _text.text = string.Format(_format,
            args.OxygenLevel,
            args.MaxOxygenLevel,
            args.OxygenLevel / args.MaxOxygenLevel * 100);
    }
}
