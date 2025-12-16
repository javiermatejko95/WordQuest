using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterTileView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image background;
    [SerializeField] private Transform root;

    private Tween currentTween;
    private Vector3 baseLocalPos;

    public void Init(LetterTileThemeConfig letterTileThemeConfig)
    {
        KillTween();

        background.color = letterTileThemeConfig.DefaultLetterTileViewBackgroundColor;
        label.text = "";
        label.color = letterTileThemeConfig.DefaultLetterColor;

        baseLocalPos = root.localPosition;
    }

    public void SetLetter(string letter)
    {
        label.text = letter;

        PlayAddLetterAnimation();
    }

    public void SetState(LetterState state, LetterTileThemeConfig letterTileThemeConfig)
    {
        background.color = state switch
        {
            LetterState.Correct => letterTileThemeConfig.CorrectBackgroundColor,
            LetterState.Present => letterTileThemeConfig.PresentBackgroundColor,
            _ => letterTileThemeConfig.AbsentBackgroundColor
        };

        label.color = letterTileThemeConfig.SelectedLetterColor;
    }

    public void PlayAddLetterAnimation()
    {
        KillTween();

        root.localScale = Vector3.one;

        currentTween = root.DOPunchScale(
        new Vector3(-0.15f, -0.15f, 0f),
        0.2f,
        8,
        0.6f
    );
    }

    public void PlayRevealAnimation(float delay, LetterState state, LetterTileThemeConfig theme)
    {
        KillTween();

        root.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();

        seq.SetDelay(delay);

        seq.Append(root.DOScaleY(0f, 0.15f).SetEase(Ease.InQuad));

        seq.AppendCallback(() =>
        {
            SetState(state, theme);
        });

        seq.Append(root.DOScaleY(1f, 0.15f).SetEase(Ease.OutQuad));

        currentTween = seq;
    }

    public void PlayInvalidWordAnimation()
    {
        KillTween();

        root.localPosition = baseLocalPos;

        currentTween = root.DOShakePosition(
            0.3f,
            strength: 10f,
            vibrato: 20,
            randomness: 90,
            fadeOut: true
        );
    }

    private void KillTween()
    {
        if (currentTween != null && currentTween.IsActive())
            currentTween.Kill();
    }
}
