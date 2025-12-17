using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LocalizationController : MonoBehaviour
{
    private const string LOCALIZATION_TABLE = "LocalizationTable";

    private void Awake()
    {
        LocalizationEvents.OnLanguageLoad += HandleOnLanguageLoad;
        LocalizationEvents.OnRequestLocalizedText += HandleOnRequestLocalizedText;
    }

    private void OnDestroy()
    {
        LocalizationEvents.OnLanguageLoad -= HandleOnLanguageLoad;
        LocalizationEvents.OnRequestLocalizedText -= HandleOnRequestLocalizedText;
    }

    private void HandleOnLanguageLoad(string code)
    {
        //TODO: show percentage of loading and loading popup/screen
        StartCoroutine(ChangeLanguageCoroutine(code));
    }

    private IEnumerator ChangeLanguageCoroutine(string code)
    {
        yield return LocalizationSettings.InitializationOperation;

        var locale = LocalizationSettings.AvailableLocales.Locales
            .Find(l => l.Identifier.Code == code);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            yield return LocalizationSettings.SelectedLocaleAsync;
        }

        LocalizationEvents.OnLanguageChanged?.Invoke(code);
    }

    private void HandleOnRequestLocalizedText(string key, System.Action<string> callback)
    {
        StartCoroutine(GetLocalizedTextCoroutine(key, callback));
    }

    private IEnumerator GetLocalizedTextCoroutine(string key, System.Action<string> callback)
    {
        yield return LocalizationSettings.InitializationOperation;

        var tableOp = LocalizationSettings.StringDatabase.GetTableAsync(LOCALIZATION_TABLE);
        yield return tableOp;

        StringTable table = tableOp.Result;
        if (table != null)
        {
            callback?.Invoke(
                table.GetEntry(key)?.GetLocalizedString() ?? key
            );
        }
        else
        {
            callback?.Invoke(key);
        }
    }
}
