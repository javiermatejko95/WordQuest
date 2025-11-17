using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterTileView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image background;

    public void Init(LetterTileThemeConfig letterTileThemeConfig)
    {
        background.color = letterTileThemeConfig.DefaultLetterTileViewBackgroundColor;
        label.text = "";
        label.color = letterTileThemeConfig.DefaultLetterColor;
    }

    public void SetLetter(string letter)
    {
        label.text = letter;
    }

    public void SetState(LetterState state, LetterTileThemeConfig letterTileThemeConfig)
    {
        background.color = state switch
        {
            LetterState.Correct => letterTileThemeConfig.CorrectBackgroundColor,
            LetterState.Present => letterTileThemeConfig.PresentBackgroundColor,
            _ => letterTileThemeConfig.AbsentBackgroundColor
        };

        label.color = letterTileThemeConfig.SelectedLetterColor;
    }
}
