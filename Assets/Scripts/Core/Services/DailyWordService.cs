using System;
using System.Linq;
using UnityEngine;

public class DailyWordService
{
    private WordDictionary dictionary;

    public DailyWordService(WordDictionary wordDictionary)
    {
        this.dictionary = wordDictionary;
    }

    public string GetWordOfTheDay()
    {
        int index = DateTime.Now.DayOfYear % dictionary.Words.Count;
        return dictionary.Words.ElementAt(index);
    }

    public string GetNewRandomWord()
    {
        int index = UnityEngine.Random.Range(0, dictionary.Words.Count);
        return dictionary.Words.ElementAt(index);
    }
}
