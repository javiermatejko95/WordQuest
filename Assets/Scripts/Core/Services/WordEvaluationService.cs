public class WordEvaluationService
{
    public LetterResult[] Evaluate(string guess, string target)
    {
        int length = target.Length;
        LetterResult[] result = new LetterResult[target.Length];
        char[] targetArray = target.ToCharArray();
        bool[] used = new bool[length];

        for (int i = 0; i < length; i++)
        {
            if (guess[i] == target[i])
            {
                result[i] = new LetterResult { Letter = guess[i], State = LetterState.Correct };
                used[i] = true;
            }
        }

        for (int i = 0; i < length; i++)
        {
            if (result[i].State == LetterState.Correct) continue;

            bool found = false;
            for (int j = 0; j < length; j++)
            {
                if (!used[j] && guess[i] == target[j])
                {
                    found = true;
                    used[j] = true;
                    break;
                }
            }

            result[i] = new LetterResult
            {
                Letter = guess[i],
                State = found ? LetterState.Present : LetterState.Absent
            };
        }

        return result;
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
