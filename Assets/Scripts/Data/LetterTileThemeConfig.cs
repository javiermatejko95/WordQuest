using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileThemeConfig", menuName = "ThemeConfig")]
public class LetterTileThemeConfig : ScriptableObject
{
    [Header("Background Config")]
    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private Color absentBackgroundColor;
    [SerializeField] private Color presentBackgroundColor;
    [SerializeField] private Color correctBackgroundColor;

    [Space, Header("Frame Config")]
    [SerializeField] private Color defaultKeyboardColor;

    [Space, Header("Letters Config")]
    [SerializeField] private Color defaultLetterColor;
    [SerializeField] private Color selectedLetterColor;

    public Color DefaultBackgroundColor { get => defaultBackgroundColor; }
    public Color AbsentBackgroundColor { get => absentBackgroundColor; }
    public Color PresentBackgroundColor { get => presentBackgroundColor; }
    public Color CorrectBackgroundColor { get => correctBackgroundColor; }

    public Color DefaultKeyboardColor { get => defaultKeyboardColor; }

    public Color DefaultLetterColor { get => defaultLetterColor; }
    public Color SelectedLetterColor {  get => selectedLetterColor; }
}