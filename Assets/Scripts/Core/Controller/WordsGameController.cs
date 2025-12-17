using UnityEngine;

public class WordsGameController : MonoBehaviour
{
    [SerializeField] private GameDataConfig gameDataConfig;

    private WordModel model;
    private WordValidatorService validator;
    private WordEvaluationService evaluator;
    private DailyWordService dailyWord;
    private WordDictionary wordDictionary;

    private int currentMaxColumns = 0;

    private const string WORD_DOES_NOT_EXIST = "LOC_WORD_DOES_NOT_EXIST";
    private const string GUESS_WORD = "LOC_GUESS_WORD";

    private void Awake()
    {
        model = new();
        evaluator = new ();
        wordDictionary = new();
        validator = new (wordDictionary);
        dailyWord = new(wordDictionary);

        GameEvents.OnLetterEntered += HandleOnLetterEnter;
        GameEvents.OnDeleteLetter += HandleOnDeleteLetter;
        GameEvents.OnSubmitWord += HandleOnSubmitWord;
        GameEvents.OnGameRestart += HandleOnGameRestart;
        GameEvents.OnGameFinished += HandleOnGameFinished;
        GameEvents.OnLoadGame += HandleOnLoadWithNumber;
    }

    private void OnDestroy()
    {
        GameEvents.OnLetterEntered -= HandleOnLetterEnter;
        GameEvents.OnDeleteLetter -= HandleOnDeleteLetter;
        GameEvents.OnSubmitWord -= HandleOnSubmitWord;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
        GameEvents.OnGameFinished -= HandleOnGameFinished;
        GameEvents.OnLoadGame -= HandleOnLoadWithNumber;
    }

    private void HandleOnLetterEnter(string letter)
    {
        if (model.CurrentInput.Length >= currentMaxColumns || model.GameFinished) return;
        model.CurrentInput += letter.ToUpper();

        GameEvents.OnLetterAdded?.Invoke(letter.ToUpper());
    }

    private void HandleOnDeleteLetter()
    {
        if (model.CurrentInput.Length > 0)
            model.CurrentInput = model.CurrentInput[..^1];
    }

    private void HandleOnSubmitWord()
    {
        if (model.GameFinished || model.CurrentInput.Length < 5)
        {
            return;
        }

        if (!validator.IsValidWord(model.CurrentInput))
        {
            PopupEvents.OnShowPopup?.Invoke(WORD_DOES_NOT_EXIST);
            GameEvents.OnWordDoesNotExist?.Invoke();
            return;
        }

        LetterResult[] result = evaluator.Evaluate(model.CurrentInput, model.WordToGuess, model.OriginalWord);
        GameEvents.OnWordEvaluated?.Invoke(model.CurrentAttempt, result);

        bool won = model.CurrentInput == model.WordToGuess;
        model.CurrentAttempt++;
        model.CurrentInput = "";

        if (won || model.CurrentAttempt >= model.MaxAttempts)
        {
            model.GameFinished = true;
            GameEvents.OnGameFinished?.Invoke(won);
            StatsEvents.OnEndGame?.Invoke(won, model);
        }
    }

    private void HandleOnGameRestart()
    {
        if(!dailyWord.HasLoadedWords())
        {
            return;
        }
        model.GameFinished = false;
        model.CurrentAttempt = 0;

        model.OriginalWord = dailyWord.GetNewRandomWord();
        model.WordToGuess = StringUtils.RemoveDiacritics(model.OriginalWord);

        PopupEvents.OnShowPopup?.Invoke(GUESS_WORD);

        Debug.Log("La palabra del dia es: " + model.OriginalWord);
    }

    private void HandleOnGameFinished(bool hasWon)
    {
        if(hasWon)
        {
            GameEvents.OnWordReveal?.Invoke(model.OriginalWord);
        }        
    }

    private void HandleOnLoadWithNumber(string number)
    {
        currentMaxColumns = int.Parse(number);

        wordDictionary.LoadWithNumber(number, LocalizationEvents.OnGetLanguageCodeID?.Invoke());

        GameEvents.OnGameRestart?.Invoke();
    }
}
