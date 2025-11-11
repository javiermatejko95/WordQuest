using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterTileView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image background;

    public void SetLetter(string letter)
    {
        label.text = letter;
    }

    public void SetState(LetterState state)
    {
        background.color = state switch
        {
            LetterState.Correct => Color.green,
            LetterState.Present => Color.yellow,
            _ => Color.gray
        };
    }
}
