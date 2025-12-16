using System;
using System.Collections.Generic;

[Serializable]
public class StatsModel
{
    public int TotalGamesPlayed;
    public int TotalGamesWon;
    public int CurrentStreak;
    public int MaxStreak;
    public int VictoryPercentage;
    public int CurrentAttempt;
    public int BestAttempt;

    public Dictionary<int, int> WinsByAttempt = new Dictionary<int, int>();

    public StatsModel()
    {
        WinsByAttempt = new Dictionary<int, int>();

        for (int i = 1; i <= 6; i++)
            WinsByAttempt[i] = 0;
    }
}
