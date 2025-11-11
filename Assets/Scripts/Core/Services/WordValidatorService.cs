using System.Globalization;
using System.Text;

public class WordValidatorService
{
    private WordDictionary dictionary;

    public WordValidatorService(WordDictionary wordDictionary)
    {
        dictionary = wordDictionary;
    }

    public bool IsValidWord(string word)
    {
        return dictionary.IsValid(word.ToUpper());
    }    
}
