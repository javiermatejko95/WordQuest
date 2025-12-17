using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopContainerView : MonoBehaviour
{
    [SerializeField] private Button load5WordsButton;
    [SerializeField] private Button load6WordsButton;
    [SerializeField] private Button load7WordsButton;

    [SerializeField] private Button statsButton;

    private void Awake()
    {
        load5WordsButton.onClick.AddListener(() => HandleOnLoadGame("5"));
        load6WordsButton.onClick.AddListener(() => HandleOnLoadGame("6"));
        load7WordsButton.onClick.AddListener(() => HandleOnLoadGame("7"));

        statsButton.onClick.AddListener(() => StatsEvents.OnTogglePopup?.Invoke());
    }

    private void HandleOnLoadGame(string number)
    {
        EventSystem.current.SetSelectedGameObject(null);
        GameEvents.OnLoadGame?.Invoke(number);
    }


}
