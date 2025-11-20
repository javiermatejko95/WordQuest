using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private StatsModel statsModel;
    private const string STATS_KEY = "stats";

    private void Awake()
    {
        StatsEvents.OnEndGame += HandleOnEndGame;
        StatsEvents.OnGetSats += HandleOnGetStats;

        HandleOnLoadStats();
    }

    private void OnDestroy()
    {
        StatsEvents.OnEndGame -= HandleOnEndGame;
        StatsEvents.OnGetSats -= HandleOnGetStats;
    }

    private void HandleOnEndGame(bool hasWon)
    {
        statsModel.TotalGamesPlayed += 1;
        statsModel.TotalGamesWon += hasWon ? 1 : 0;
        statsModel.CurrentStreak += hasWon ? 1 : -statsModel.CurrentStreak;
        statsModel.MaxStreak = statsModel.CurrentStreak > statsModel.MaxStreak ? statsModel.CurrentStreak : statsModel.MaxStreak;

        float victoryPercentage = ((float)statsModel.TotalGamesWon / statsModel.TotalGamesPlayed) * 100f;
        statsModel.VictoryPercentage = Mathf.FloorToInt(victoryPercentage) ;

        SaveService.Save<StatsModel>(STATS_KEY, statsModel);
    }

    private void HandleOnLoadStats()
    {
        if (SaveService.Exists(STATS_KEY))
        {
            statsModel = SaveService.Load<StatsModel>(STATS_KEY);
        }
        else
        {
            statsModel = new StatsModel();
            SaveService.Save<StatsModel>(STATS_KEY, statsModel);
        }
    }

    private StatsModel HandleOnGetStats()
    {
        return statsModel; 
    }
}
