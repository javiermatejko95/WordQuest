using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [SerializeField] private string letter;
    [SerializeField] private TextMeshProUGUI letterText;
    
    private Button button;

    protected void Awake()
    {
        Init();
    }

    private void Init()
    {
        button = GetComponent<Button>();

        letterText.text = letter;

        button.onClick.AddListener(Press);
    }

    public void Press()
    {
        if (letter == "ENTER") GameEvents.OnSubmitWord?.Invoke();
        else if (letter == "DEL") GameEvents.OnDeleteLetter?.Invoke();
        else GameEvents.OnLetterEntered?.Invoke(letter);
    }
}