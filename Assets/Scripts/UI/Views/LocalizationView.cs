using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationView : MonoBehaviour
{
    [SerializeField] private LocalizationData localizationData;
    [SerializeField] private TMP_Dropdown languageDropdown;

    private void Awake()
    {
        LocalizationEvents.OnLanguageChanged += HandleOnLanguageChanged;

        PopulateDropdown();
    }

    private void HandleOnLanguageChanged()
    {
        Debug.Log("Se cambió el idioma");
    }

    private void PopulateDropdown()
    {
        languageDropdown.options.Clear();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (var entry in localizationData.LocalizationValues)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData
            {
                text = entry.CodeID,
                image = entry.flagIcon  // solo funciona si tu dropdown está configurado en Image/Text
            };

            options.Add(option);
        }

        languageDropdown.AddOptions(options);
        languageDropdown.RefreshShownValue();

        languageDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void OnDropdownChanged(int index)
    {
        string code = localizationData.LocalizationValues[index].CodeID;

        LocalizationEvents.OnLanguageLoad?.Invoke(code);

        Debug.Log($"Idioma seleccionado: {code}");
    }
}
