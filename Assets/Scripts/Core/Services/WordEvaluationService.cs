using System.Collections.Generic;

public class WordEvaluationService
{
    public LetterResult[] Evaluate(string guess, string target, string target2)
    {
        int length = target.Length;
        LetterResult[] result = new LetterResult[length];

        Dictionary<char, int> letterCount = new Dictionary<char, int>();        

        AddLetters(target);

        for (int i = 0; i < length; i++)
        {
            if (guess[i] == target[i] || guess[i] == target2[i])
            {
                result[i] = new LetterResult
                {
                    Letter = guess[i],
                    State = LetterState.Correct
                };

                letterCount[guess[i]]--;
            }
        }

        for (int i = 0; i < length; i++)
        {
            if (result[i].State == LetterState.Correct)
                continue;

            char letter = guess[i];

            if (letterCount.TryGetValue(letter, out int count) && count > 0)
            {
                result[i] = new LetterResult
                {
                    Letter = letter,
                    State = LetterState.Present
                };

                letterCount[letter]--;
            }
            else
            {
                result[i] = new LetterResult
                {
                    Letter = letter,
                    State = LetterState.Absent
                };
            }
        }

        return result;

        void AddLetters(string word)
        {
            foreach (char c in word)
            {
                if (!letterCount.ContainsKey(c))
                    letterCount[c] = 0;
                letterCount[c]++;
            }
        }
    }
}

public enum LetterState
{
    Absent,
    Present,
    Correct,
}

public struct LetterResult
{
    public char Letter;
    public LetterState State;
}
