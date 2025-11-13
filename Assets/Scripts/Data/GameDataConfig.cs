using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data")]
public class GameDataConfig : ScriptableObject
{
    [SerializeField] private int maxRows;
    [SerializeField] private int maxColumns;

    [SerializeField] private int maxAttempts;

    public int MaxRows { get => maxRows; }
    public int MaxColumns { get => maxColumns; }

    public int MaxAttempts { get => maxAttempts; }
}
