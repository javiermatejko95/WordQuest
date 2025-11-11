using UnityEngine;

public class WordsGameController : MonoBehaviour
{
    [SerializeField] private GameDataConfig gameDataConfig;

    private WordModel model;
    private WordValidatorService validator;
    private WordEvaluationService evaluator;

    private void Awake()
    {
        model = new();
        validator = new ();
        evaluator = new ();

        GameEvents.OnLetterEntered += HandleOnLetterEnter;
        GameEvents.OnDeleteLetter += HandleOnDeleteLetter;
        GameEvents.OnSubmitWord += HandleOnSubmitWord;
    }

    private void HandleOnLetterEnter(string letter)
    {
        if (model.CurrentInput.Length >= 5 || model.GameFinished) return;
        model.CurrentInput += letter.ToUpper();
    }

    private void HandleOnDeleteLetter()
    {
        if (model.CurrentInput.Length > 0)
            model.CurrentInput = model.CurrentInput[..^1];
    }

    private void HandleOnSubmitWord()
    {
        if (model.GameFinished || model.CurrentInput.Length < 5) return;
        if (!validator.IsValidWord(model.CurrentInput)) return;

        LetterResult[] result = evaluator.Evaluate(model.CurrentInput, model.WordToGuess);
        GameEvents.OnWordEvaluated?.Invoke(model.CurrentAttempt, result);

        bool won = model.CurrentInput == model.WordToGuess;
        model.CurrentAttempt++;
        model.CurrentInput = "";

        if (won || model.CurrentAttempt >= model.MaxAttempts)
        {
            model.GameFinished = true;
            GameEvents.OnGameFinished?.Invoke(won);
        }
    }
}
