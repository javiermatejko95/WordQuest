using TMPro;
using UnityEngine;

public class StatWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI numberText;

    public void SetData(string title, string number)
    {
        LocalizationEvents.OnRequestLocalizedText?.Invoke(title, text =>
        {
            titleText.text = text;
        });
        numberText.text = number;
    }
}
