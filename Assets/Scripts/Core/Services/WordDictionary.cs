using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class WordDictionary
{
    public HashSet<string> Words { get; private set; }
    public HashSet<string> WordsClean { get; private set; }

    //public WordDictionary()
    //{
    //    Load();
    //}

    public void Load()
    {
        TextAsset file = Resources.Load<TextAsset>("es_clean");
        Words = new HashSet<string>(file.text
            .Split('\n')
            .Select(word => word.Trim()));

        WordsClean = new HashSet<string>(
        file.text
            .Split('\n')
            .Select(word => StringUtils.RemoveDiacritics(word.Trim().ToUpper()))
        );

        Debug.Log($"?? Palabras cargadas: {Words.Count}");
    }

    public void LoadWithNumber(string number)
    {
        TextAsset file = Resources.Load<TextAsset>($"{number}_es_clean");
        Words = new HashSet<string>(file.text
            .Split('\n')
            .Select(word => word.Trim()));

        WordsClean = new HashSet<string>(
        file.text
            .Split('\n')
            .Select(word => StringUtils.RemoveDiacritics(word.Trim().ToUpper()))
        );

        Debug.Log($"?? Palabras cargadas: {Words.Count}");
    }

    public bool IsValid(string word)
    {
        return WordsClean.Contains(word.ToUpper()) || Words.Contains(word.ToUpper());
    }
}
