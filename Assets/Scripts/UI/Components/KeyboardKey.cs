using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [SerializeField] private string letter;
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image backgroundImage;

    private Button button;

    public string Letter => letter;

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

    public void ResetState(LetterTileThemeConfig letterTileThemeConfig)
    {
        backgroundImage.color = letterTileThemeConfig.DefaultKeyboardColor;
        letterText.color = letterTileThemeConfig.DefaultLetterColor;
    }

    public void Press()
    {
        if (letter == "ENTER") GameEvents.OnSubmitWord?.Invoke();
        else if (letter == "DEL") GameEvents.OnDeleteLetter?.Invoke();
        else GameEvents.OnLetterEntered?.Invoke(letter);
    }

    public void SetState(LetterState state, LetterTileThemeConfig letterTileThemeConfig)
    {
        backgroundImage.color = state switch
        {
            LetterState.Correct => letterTileThemeConfig.CorrectBackgroundColor,
            LetterState.Present => letterTileThemeConfig.PresentBackgroundColor,
            _ => letterTileThemeConfig.AbsentBackgroundColor
        };

        letterText.color = letterTileThemeConfig.SelectedLetterColor;
    }
}