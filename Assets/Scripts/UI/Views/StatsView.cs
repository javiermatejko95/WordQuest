using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : MonoBehaviour
{
    [SerializeField] private GameObject statsPopup;
    [SerializeField] private StatWidget[] statWidgets;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button resetStatsBtn;
    [SerializeField] private AttemptWidget[] attemptWidgets;

    private bool statsPopupToggle;

    private void Awake()
    {
        StatsEvents.OnTogglePopup += HandleOnToggleStatsPopup;

        closeBtn.onClick.AddListener(() => StatsEvents.OnTogglePopup?.Invoke());
        resetStatsBtn.onClick.AddListener(() => HandleOnResetStats());
    }

    private void HandleOnToggleStatsPopup()
    {
        statsPopupToggle = !statsPopupToggle;

        statsPopup.SetActive(statsPopupToggle);
        
        if(statsPopupToggle)
        {
            UpdateData();
        }        
    }

    private void HandleOnResetStats()
    {
        StatsEvents.OnResetStats?.Invoke();
    }

    private void UpdateData()
    {
        StatsModel data = StatsEvents.OnGetStats?.Invoke();

        UpdateStatWidgetsData(data);
        UpdateAttemptWidgetsData(data);
    }

    private void UpdateStatWidgetsData(StatsModel data)
    {
        statWidgets[0].SetData("Juegos Jugados", data.TotalGamesPlayed.ToString());
        statWidgets[1].SetData("Juegos ganados", data.TotalGamesWon.ToString());
        statWidgets[2].SetData("Victorias (%)", data.VictoryPercentage.ToString("0"));
        statWidgets[3].SetData("Mejor intento", "#0");
        statWidgets[4].SetData("Racha actual", data.CurrentStreak.ToString());
        statWidgets[5].SetData("Mejor racha", data.MaxStreak.ToString());
    }

    private void UpdateAttemptWidgetsData(StatsModel data)
    {
        Dictionary<int, float> attemptsPercentagesDictionary = StatsEvents.OnGetAttemptsPercentages?.Invoke();

        for(int i = 0; i < attemptWidgets.Length; i++)
        {
            attemptWidgets[i].SetData(attemptsPercentagesDictionary[i+1], data.WinsByAttempt[i+1].ToString());
        }
    }
}
