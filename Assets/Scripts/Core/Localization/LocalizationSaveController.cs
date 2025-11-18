using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSaveController : MonoBehaviour
{
    [SerializeField] private LocalizationData localizationData;

    private const string SAVE_KEY = "localization";

    public LocalizationDataModel Data { get; private set; }

    private void Awake()
    {
        LocalizationEvents.OnLanguageChanged += Save;

        Load();
    }

    private void Save(string languageCode)
    {
        Data.CurrentLanguage = languageCode;
        SaveService.Save(SAVE_KEY, Data);
    }

    public void Load()
    {
        SaveService.Initialize(new JsonSaveProvider());

        if (SaveService.Exists(SAVE_KEY))
        {
            Data = SaveService.Load<LocalizationDataModel>(SAVE_KEY);
        }
        else
        {
            Data = new LocalizationDataModel();
            Data.CurrentLanguage = localizationData.LocalizationValues[0].CodeID;
        }

        LocalizationEvents.OnLanguageLoad?.Invoke(Data.CurrentLanguage);
    }
}
