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
        LocalizationEvents.OnLanguageSaved += HandleOnLanguageSaved;

        PopulateDropdown();
    }

    private void HandleOnLanguageSaved(string code)
    {
        Debug.Log("Se cambió el idioma a " + code);

        int indexToSelect = 0;

        for (int i = 0; i < languageDropdown.options.Count; i++)
        {
            string optionCode = languageDropdown.options[i].text;

            if (optionCode == code)
            {
                indexToSelect = i;
                break;
            }
        }

        languageDropdown.value = indexToSelect;
        languageDropdown.RefreshShownValue();

        GameEvents.OnHideBoard?.Invoke();
    }

    private void PopulateDropdown()
    {
        languageDropdown.options.Clear();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (var entry in localizationData.LocalizationValues)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(entry.CodeID, entry.flagIcon);

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
