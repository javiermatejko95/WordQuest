using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private StatsModel statsModel;
    private const string STATS_KEY = "stats";

    private Dictionary<int, int> attempPercentage = new();

    private void Awake()
    {
        StatsEvents.OnEndGame += HandleOnEndGame;
        StatsEvents.OnGetStats += HandleOnGetStats;
        StatsEvents.OnGetAttemptsPercentages += HandleOnGetAttemptPercentages;
        StatsEvents.OnResetStats += HandleOnResetStats;

        HandleOnLoadStats();
    }

    private void OnDestroy()
    {
        StatsEvents.OnEndGame -= HandleOnEndGame;
        StatsEvents.OnGetStats -= HandleOnGetStats;
        StatsEvents.OnGetAttemptsPercentages -= HandleOnGetAttemptPercentages;
        StatsEvents.OnResetStats -= HandleOnResetStats;
    }

    private void HandleOnEndGame(bool hasWon, WordModel wordModel)
    {
        statsModel.TotalGamesPlayed += 1;
        statsModel.TotalGamesWon += hasWon ? 1 : 0;
        statsModel.CurrentStreak += hasWon ? 1 : -statsModel.CurrentStreak;
        statsModel.MaxStreak = statsModel.CurrentStreak > statsModel.MaxStreak ? statsModel.CurrentStreak : statsModel.MaxStreak;
        statsModel.WinsByAttempt[wordModel.CurrentAttempt] += 1;
        statsModel.BestAttempt = statsModel.WinsByAttempt
            .Where(key => key.Value > 0)
            .DefaultIfEmpty(new KeyValuePair<int, int>(0, 0))
            .OrderByDescending(kv => kv.Value)
            .First()
            .Key;

        float victoryPercentage = ((float)statsModel.TotalGamesWon / statsModel.TotalGamesPlayed) * 100f;
        statsModel.VictoryPercentage = Mathf.FloorToInt(victoryPercentage);

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

    private void HandleOnResetStats()
    {
        SaveService.Delete(STATS_KEY);

        statsModel = new StatsModel();
        SaveService.Save<StatsModel>(STATS_KEY, statsModel);

        StatsEvents.OnResettedStats?.Invoke();
    }

    private Dictionary<int, float> HandleOnGetAttemptPercentages()
    {
        Dictionary<int, float> result = new Dictionary<int, float>();

        int totalGames = statsModel.TotalGamesPlayed;

        if (totalGames == 0)
        {
            for (int i = 1; i <= 6; i++)
                result[i] = 0;

            return result;
        }

        for (int i = 1; i <= 6; i++)
        {
            int wins = statsModel.WinsByAttempt.ContainsKey(i) ? statsModel.WinsByAttempt[i] : 0;
            result[i] = ((float)wins / totalGames);
        }

        return result;
    }
}
