using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
        StatsEvents.OnResettedStats += UpdateData;

        closeBtn.onClick.AddListener(() => StatsEvents.OnTogglePopup?.Invoke());
        resetStatsBtn.onClick.AddListener(() => HandleOnResetStats());

        for (int i = 1; i <= attemptWidgets.Length; i++)
        {
            attemptWidgets[i - 1].Setup(i);
        }
    }

    private void OnDestroy()
    {
        StatsEvents.OnTogglePopup -= HandleOnToggleStatsPopup;
        StatsEvents.OnResettedStats -= UpdateData;
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
        statWidgets[3].SetData("Mejor intento", data.BestAttempt.ToString());
        statWidgets[4].SetData("Racha actual", data.CurrentStreak.ToString());
        statWidgets[5].SetData("Mejor racha", data.MaxStreak.ToString());
    }

    private void UpdateAttemptWidgetsData(StatsModel data)
    {
        Dictionary<int, float> attemptsPercentagesDictionary = StatsEvents.OnGetAttemptsPercentages?.Invoke();

        for(int i = 1; i <= attemptWidgets.Length; i++)
        {
            attemptWidgets[i-1].SetData(attemptsPercentagesDictionary[i], data.WinsByAttempt[i].ToString());
        }
    }
}
