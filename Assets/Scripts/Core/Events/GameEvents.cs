using System;

public static class GameEvents
{
    public static Action<string> OnLetterEntered;
    public static Action<string> OnLetterAdded;
    public static Action OnDeleteLetter;
    public static Action OnSubmitWord;
    public static Action<int, LetterResult[]> OnWordEvaluated;
    public static Action<bool> OnGameFinished;

    public static Action OnGameRestart;
}
