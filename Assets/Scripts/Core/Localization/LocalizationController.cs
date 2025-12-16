using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LocalizationController : MonoBehaviour
{
    private const string LOCALIZATION_TABLE = "LocalizationTable";

    private async void Awake()
    {
        LocalizationEvents.OnLanguageLoad += HandleOnLanguageLoad;
        LocalizationEvents.OnGetLanguageKey += HandleOnGetLanguageKey;
    }

    private void OnDestroy()
    {
        LocalizationEvents.OnLanguageLoad -= HandleOnLanguageLoad;
        LocalizationEvents.OnGetLanguageKey -= HandleOnGetLanguageKey;
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

    private string HandleOnGetLanguageKey(string key)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString(LOCALIZATION_TABLE, key);
    }
}
