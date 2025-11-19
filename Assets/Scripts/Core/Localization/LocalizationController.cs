using UnityEngine.Localization.Settings;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using System;

public class LocalizationController : MonoBehaviour
{
    private void Awake()
    {
        LocalizationEvents.OnLanguageLoad += HandleOnLanguageLoad;
    }

    private void HandleOnLanguageLoad(string code)
    {
        //TODO: show percentage of loading and loading popup/screen
        ChangeLanguage(code).Forget();
    }

    private async UniTask ChangeLanguage(string code)
    {
        await LocalizationSettings.InitializationOperation.Task;

        var locale = LocalizationSettings.AvailableLocales.Locales
            .Find(l => l.Identifier.Code == code);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            await LocalizationSettings.SelectedLocaleAsync.Task;
        }

        LocalizationEvents.OnLanguageChanged?.Invoke(code);
    }
}
