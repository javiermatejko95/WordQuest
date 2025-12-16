using System;

public static class GameEvents
{
    public static Action<string> OnLoadGame; 

    public static Action<string> OnLetterEntered;
    public static Action<string> OnLetterAdded;
    public static Action OnDeleteLetter;
    public static Action OnSubmitWord;
    public static Action<int, LetterResult[]> OnWordEvaluated;
    public static Action<bool> OnGameFinished;
    public static Action OnWordDoesNotExist;

    public static Action<string> OnWordReveal;

    public static Action OnGameRestart;
    public static Action OnHideBoard;
}
