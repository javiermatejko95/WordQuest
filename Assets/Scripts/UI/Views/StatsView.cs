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

    private const string LOC_GAMES_PLAYED = "LOC_GAMES_PLAYED";
    private const string LOC_GAMES_WON = "LOC_GAMES_WON";
    private const string LOC_VICTORY_PERCENTAGE = "LOC_VICTORY_PERCENTAGE";
    private const string LOC_BEST_TRY = "LOC_BEST_TRY";
    private const string LOC_CURRENT_STREAK = "LOC_CURRENT_STREAK";
    private const string LOC_BEST_STREAK = "LOC_BEST_STREAK";

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
        statWidgets[0].SetData(LOC_GAMES_PLAYED, data.TotalGamesPlayed.ToString());
        statWidgets[1].SetData(LOC_GAMES_WON, data.TotalGamesWon.ToString());
        statWidgets[2].SetData(LOC_VICTORY_PERCENTAGE, data.VictoryPercentage.ToString("0"));
        statWidgets[3].SetData(LOC_BEST_TRY, data.BestAttempt.ToString());
        statWidgets[4].SetData(LOC_CURRENT_STREAK, data.CurrentStreak.ToString());
        statWidgets[5].SetData(LOC_BEST_STREAK, data.MaxStreak.ToString());
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
