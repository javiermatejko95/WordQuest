using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardView : MonoBehaviour
{
    [SerializeField] private KeyboardKey enyeKey;
    [SerializeField] private LetterTileThemeConfig letterTileThemeConfig;

    private Dictionary<string, KeyboardKey> keys;

    private void Awake()
    {
        KeyboardEvents.OnEnyeKeyEnabled += HandleOnEnyeKeyEnabled;

        GameEvents.OnWordEvaluated += HandleOnKeyLetterChangeStatus;
        GameEvents.OnGameRestart += HandleOnGameRestart;

        keys = GetComponentsInChildren<KeyboardKey>(true)
            .ToDictionary(k => k.Letter.ToUpper(), k => k);
    }

    private void OnDestroy()
    {
        KeyboardEvents.OnEnyeKeyEnabled -= HandleOnEnyeKeyEnabled;

        GameEvents.OnWordEvaluated -= HandleOnKeyLetterChangeStatus;
        GameEvents.OnGameRestart -= HandleOnGameRestart;
    }

    private void HandleOnEnyeKeyEnabled(bool status)
    {
        enyeKey.gameObject.SetActive(status);
    }

    private void HandleOnKeyLetterChangeStatus(int _, LetterResult[] result)
    {
        for(int i = 0; i < result.Length; i++) 
        {
            KeyboardKey key = keys[result[i].Letter.ToString()];

            if (key != null) 
            {
                key.SetState(result[i].State, letterTileThemeConfig);
            }
        }
    }

    private void HandleOnGameRestart()
    {
        foreach (KeyboardKey key in keys.Values) 
        {
            key.ResetState(letterTileThemeConfig);
        }
    }
}
