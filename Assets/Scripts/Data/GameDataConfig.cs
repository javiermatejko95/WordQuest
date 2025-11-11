using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data")]
public class GameDataConfig : ScriptableObject
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private int maxAttempts;

    public int Rows { get => rows; }
    public int Columns { get => columns; }

    public int MaxAttempts { get => maxAttempts; }
}
