using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfettiData", menuName = "ConfettiData")]
public class ConfettiData : ScriptableObject
{
    [SerializeField] private int amount = 30;
    [SerializeField] private Color[] colors;

    public int Amount { get => amount; }
    public Color[] Colors { get => colors; }
}
