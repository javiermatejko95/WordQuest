using System;
using UnityEngine;
using static UnityEditor.Localization.LocalizationTableCollection;

public class BoardView : MonoBehaviour
{
    [SerializeField] private GameDataConfig gameDataConfig;
    [SerializeField] private LetterTileThemeConfig letterTileThemeConfig;

    [SerializeField] private GameObject rowViewPrefab;
    [SerializeField] private LetterTileView letterTileViewPrefab;
    [SerializeField] private LetterTileView[,] board;

    private int currentRow = 0;
    private int currentCol = 0;

    private int currentMaxColumns = 0;

    private void Awake()
    {
        GameEvents.OnLetterAdded += AddLetter;
        GameEvents.OnDeleteLetter += RemoveLetter;
        GameEvents.OnWordEvaluated += LockRow;
        GameEvents.OnGameRestart += HandleOnGameRestart;
        GameEvents.OnWordReveal += HandleOnWordReveal;
        GameEvents.OnLoadGame += ShowLetterTiles;
        GameEvents.OnHideBoard += HandleOnHideBoard;
        GameEvents.OnWordDoesNotExist += HandleOnWordDoesNotExist;

        board = new LetterTileView[gameDataConfig.MaxRows, gameDataConfig.MaxColumns];

        SpawnLetterTiles();
    }

    private void OnDestroy()
    {
        GameEvents.OnLetterAdded -= AddLetter;
        GameEvents.OnDeleteLetter -= RemoveLetter;
        GameEvents.OnWordEvaluated -= LockRow;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
        GameEvents.OnWordReveal -= HandleOnWordReveal;
        GameEvents.OnLoadGame -= ShowLetterTiles;
        GameEvents.OnWordDoesNotExist -= HandleOnWordDoesNotExist;
    }

    private void SpawnLetterTiles()
    {
        for (int i = 0; i < gameDataConfig.MaxRows; i++)
        {
            GameObject go = Instantiate(rowViewPrefab, transform);

            for (int j = 0; j < gameDataConfig.MaxColumns; j++)
            {
                LetterTileView letterTileView = Instantiate(letterTileViewPrefab, go.transform);
                board[i, j] = letterTileView;
                letterTileView.Init(letterTileThemeConfig);
                letterTileView.gameObject.SetActive(false);
            }
        }
    }

    private void ShowLetterTiles(string columns)
    {
        HandleOnGameRestart();

        currentMaxColumns = int.Parse(columns);

        for(int i = 0; i < gameDataConfig.MaxRows; i++) 
        {
            for(int j = 0; j < gameDataConfig.MaxColumns; j++)
            {
                board[i, j ].Init(letterTileThemeConfig);
                board[i, j].gameObject.SetActive(j < currentMaxColumns ? true : false);
            }
        }
    }

    private void AddLetter(string letter)
    {
        if (currentCol >= currentMaxColumns) return;
        board[currentRow, currentCol].SetLetter(letter);
        currentCol++;
    }

    private void RemoveLetter()
    {
        if (currentCol == 0) return;
        currentCol--;
        board[currentRow, currentCol].SetLetter("");
    }

    private void LockRow(int row, LetterResult[] letters)
    {
        float delay = 0f;

        for (int i = 0; i < letters.Length; i++)
        {
            board[row, i].SetLetter(letters[i].Letter.ToString());
            board[row, i].PlayRevealAnimation(delay, letters[i].State, letterTileThemeConfig);

            delay += 0.1f;
        }

        currentRow++;
        currentCol = 0;
    }

    private void HandleOnWordDoesNotExist()
    {
        for (int i = 0; i < gameDataConfig.MaxRows; i++)
        {
            board[currentRow, i].PlayInvalidWordAnimation();
        }
    }

    private void HandleOnGameRestart()
    {
        currentRow = 0;
        currentCol = 0;

        for(int i = 0; i < gameDataConfig.MaxRows; i++)
        {
            for(int j = 0; j < gameDataConfig.MaxColumns; j++)
            {
                board[i,j].Init(letterTileThemeConfig);
            }
        }
    }

    private void HandleOnWordReveal(string word)
    {
        float delay = 0f;

        for (int i = 0; i < currentMaxColumns; i++)
        {
            board[currentRow - 1, i].SetLetter(word[i].ToString());
            board[currentRow - 1, i].PlayRevealAnimation(delay, LetterState.Correct, letterTileThemeConfig);
            delay += 0.1f;
        }
    }

    private void HandleOnHideBoard()
    {
        for (int i = 0; i < gameDataConfig.MaxRows; i++)
        {
            for (int j = 0; j < gameDataConfig.MaxColumns; j++)
            {
                board[i, j].gameObject.SetActive(false);
            }
        }
    }
}
