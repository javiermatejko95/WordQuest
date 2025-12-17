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

    private void Awake()
    {
        GameEvents.OnGameFinished += HandleOnGameFinished;
        GameEvents.OnGameRestart += HandleOnGameRestart;

        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDestroy()
    {
        GameEvents.OnGameFinished -= HandleOnGameFinished;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
    }

    private void HandleOnGameFinished(bool hasWon)
    {
        container.SetActive(true);
                
        feedbackText.text = hasWon ? "¡Ganaste!" : "¡Perdiste!";
        icon.gameObject.SetActive(hasWon);

        restartButton.gameObject.SetActive(true);
    }

    private void HandleOnGameRestart()
    {
        container.SetActive(false);

        restartButton.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        GameEvents.OnGameRestart?.Invoke();
    }
}
