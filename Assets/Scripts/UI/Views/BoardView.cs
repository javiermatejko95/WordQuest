using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private GameDataConfig gameDataConfig;
    [SerializeField] private LetterTileThemeConfig letterTileThemeConfig;

    [SerializeField] private GameObject rowViewPrefab;
    [SerializeField] private LetterTileView letterTileViewPrefab;
    [SerializeField] private LetterTileView[,] board;

    private int currentRow = 0;
    private int currentCol = 0;

    private void Awake()
    {
        GameEvents.OnLetterAdded += AddLetter;
        GameEvents.OnDeleteLetter += RemoveLetter;
        GameEvents.OnWordEvaluated += LockRow;
        GameEvents.OnGameRestart += HandleOnGameRestart;

        board = new LetterTileView[gameDataConfig.Rows, gameDataConfig.Columns];

        SpawnLetterTiles();
    }

    private void OnDestroy()
    {
        GameEvents.OnLetterAdded -= AddLetter;
        GameEvents.OnDeleteLetter -= RemoveLetter;
        GameEvents.OnWordEvaluated -= LockRow;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
    }

    private void SpawnLetterTiles()
    {
        for(int i = 0; i < gameDataConfig.Rows; i++) 
        {
            GameObject go = Instantiate(rowViewPrefab, transform);

            for(int j = 0; j < gameDataConfig.Columns; j++)
            {
                LetterTileView letterTileView = Instantiate(letterTileViewPrefab, go.transform);
                board[i, j] = letterTileView;
                letterTileView.Init(letterTileThemeConfig);
            }
        }
    }

    private void AddLetter(string letter)
    {
        if (currentCol >= gameDataConfig.Columns) return;
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
        for (int i = 0; i < letters.Length; i++)
        {
            board[row, i].SetLetter(letters[i].Letter.ToString());
            board[row, i].SetState(letters[i].State, letterTileThemeConfig);
        }

        currentRow++;
        currentCol = 0;
    }

    private void HandleOnGameRestart()
    {
        currentRow = 0;
        currentCol = 0;

        for(int i = 0; i < gameDataConfig.Rows; i++)
        {
            for(int j = 0; j < gameDataConfig.Columns; j++)
            {
                board[i,j].Init(letterTileThemeConfig);
            }
        }
    }
}
