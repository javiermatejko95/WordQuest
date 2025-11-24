using TMPro;
using UnityEngine;

public class StatWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI numberText;

    public void SetData(string title, string number)
    {
        titleText.text = title;
        numberText.text = number;
    }
}
