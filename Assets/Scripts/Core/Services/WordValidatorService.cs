using System.Collections.Generic;

public class WordValidatorService
{
    private HashSet<string> validWords = new() { "UNITY", "APPLE", "WORLD", "PLANE", "HOUSE" };

    public bool IsValidWord(string word)
    {
        return validWords.Contains(word.ToUpper());
    }
}
