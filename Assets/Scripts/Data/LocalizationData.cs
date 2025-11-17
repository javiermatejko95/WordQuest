using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [SerializeField] private LocalizationStruct[] localizationValues;

    public LocalizationStruct[] LocalizationValues { get => localizationValues; }
}

[Serializable]
public struct LocalizationStruct
{
    public string CodeID;
    public Sprite flagIcon;
}