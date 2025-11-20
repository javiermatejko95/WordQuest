using System;

public static class StatsEvents
{
    public static Action<bool> OnEndGame;
    public static Func<StatsModel> OnGetSats;
}
