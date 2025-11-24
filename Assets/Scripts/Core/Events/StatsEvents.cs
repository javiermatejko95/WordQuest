using System;
using System.Collections.Generic;

public static class StatsEvents
{
    public static Action<bool, WordModel> OnEndGame;
    public static Func<StatsModel> OnGetStats;
    public static Action OnTogglePopup;
    public static Action OnResetStats;
    public static Func<Dictionary<int, float>> OnGetAttemptsPercentages;
}
