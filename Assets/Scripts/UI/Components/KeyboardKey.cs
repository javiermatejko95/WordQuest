using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : Button
{
    [SerializeField] private string letter;
    [SerializeField] private TextMeshProUGUI letterText;

    protected override void Awake()
    {
        Init();
    }

    private void Init()
    {
        letterText.text = letter;

        onClick.AddListener(Press);
    }

    public void Press()
    {
        if (letter == "ENTER") GameEvents.OnSubmitWord?.Invoke();
        else if (letter == "DEL") GameEvents.OnDeleteLetter?.Invoke();
        else GameEvents.OnLetterEntered?.Invoke(letter);
    }
}
