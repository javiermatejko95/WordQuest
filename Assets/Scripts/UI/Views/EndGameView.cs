using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Image icon;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI wordFeebackText;

    private const string LOC_WON = "LOC_WON";
    private const string LOC_LOST = "LOC_LOST";
    private const string LOC_WORD_WAS = "LOC_WORD_WAS";

    private void Awake()
    {
        GameEvents.OnGameFinished += HandleOnGameFinished;
        GameEvents.OnGameRestart += HandleOnGameRestart;

        restartButton.onClick.AddListener(RestartGame);

        wordFeebackText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.OnGameFinished -= HandleOnGameFinished;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
    }

    private void HandleOnGameFinished(bool hasWon)
    {
        LocalizationEvents.OnRequestLocalizedText?.Invoke(hasWon ? LOC_WON : LOC_LOST, text =>
        {
            container.SetActive(true);
            feedbackText.text = text;
            icon.gameObject.SetActive(hasWon);
        });

        if(!hasWon)
        {
            LocalizationEvents.OnRequestLocalizedText?.Invoke(LOC_WORD_WAS, text =>
            {
                string word = GameEvents.OnGetCurrentWord?.Invoke();
                wordFeebackText.text = string.Format(text, word);
                wordFeebackText.gameObject.SetActive(true);
            });
        }        

        restartButton.gameObject.SetActive(true);
    }

    private void HandleOnGameRestart()
    {
        container.SetActive(false);

        restartButton.gameObject.SetActive(false);
        wordFeebackText.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        GameEvents.OnGameRestart?.Invoke();
    }
}
