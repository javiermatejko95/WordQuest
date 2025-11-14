using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ConfettiSpawner : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform confettiPrefab;

    [Header("Config")]
    [SerializeField] private ConfettiData confettiData;

    private void Awake()
    {
        GameEvents.OnGameFinished += HandleOnGameFinished;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameFinished -= HandleOnGameFinished;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Play();
        }
    }

    private void HandleOnGameFinished(bool hasWon)
    {
        if(!hasWon)
        {
            return;
        }

        Play();
    }

    private void Play()
    {
        for (int i = 0; i < confettiData.Amount; i++)
        {
            SpawnPiece(true);
            SpawnPiece(false);
        }
    }

    private void SpawnPiece(bool fromLeft)
    {
        RectTransform piece = Instantiate(confettiPrefab, canvasRect);

        piece.GetComponent<Image>().color = confettiData.Colors[Random.Range(0, confettiData.Colors.Length)];

        float width = canvasRect.rect.width;
        float height = canvasRect.rect.height;

        Vector2 startPos = fromLeft
            ? new Vector2(-50f, 0f)
            : new Vector2(width + 50f, 0f);

        piece.anchoredPosition = startPos;

        Vector2 diagonalTarget = 
            new Vector2(
                fromLeft ? Random.Range(0, width / 2) : Random.Range(width / 2, width),
                Random.Range(height / 4, height)
            );

        Vector2 fallTarget = new Vector2(
            diagonalTarget.x + Random.Range(-40f, 40f),
            -150f
        );

        float diagTime = Random.Range(0.35f, 0.55f);
        float fallTime = Random.Range(1.1f, 1.5f);

        Sequence seq = DOTween.Sequence();

        seq.Append(piece.DOAnchorPos(diagonalTarget, diagTime).SetEase(Ease.OutQuad));
        seq.Append(piece.DOAnchorPos(fallTarget, fallTime).SetEase(Ease.InQuad));

        float totalTime = diagTime + fallTime;

        float rotX = Random.Range(-180f, 180f);
        float rotY = Random.Range(-180f, 180f);

        float rotZ = Random.Range(720f, 1400f);

        float directionY = Random.value > 0.5f ? 1f : -1f;

        piece
            .DORotate(
                new Vector3(rotX, rotY * directionY, rotZ ),
                totalTime,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear);

        piece.localScale = Vector3.zero;
        piece.DOScale(Random.Range(0.7f, 1.1f), 0.3f).SetEase(Ease.OutBack);

        seq.OnComplete(() => Destroy(piece.gameObject));
    }
}
