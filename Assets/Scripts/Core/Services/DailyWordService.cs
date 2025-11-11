using System;
using System.Collections.Generic;
using System.Linq;

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
}
