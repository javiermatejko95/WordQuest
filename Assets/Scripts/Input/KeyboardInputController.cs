using System.Linq;
using UnityEngine;

public class KeyboardInputController : MonoBehaviour
{
    [SerializeField] private LocalizationData localizationData;

    private void Awake()
    {
        LocalizationEvents.OnLanguageLoad += HandleOnLanguageLoad;
    }

    private void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
                GameEvents.OnLetterEntered?.Invoke(char.ToUpper(c).ToString());

            else if (c == '\b')
                GameEvents.OnDeleteLetter?.Invoke();

            else if (c == '\n' || c == '\r')
                GameEvents.OnSubmitWord?.Invoke();
        }
    }

    private void OnDestroy()
    {
        LocalizationEvents.OnLanguageLoad -= HandleOnLanguageLoad;
    }

    private void HandleOnLanguageLoad(string code)
    {
        bool active = localizationData.LanguageRules.Contains(code);
        KeyboardEvents.OnEnyeKeyEnabled?.Invoke(active);
    }
}
