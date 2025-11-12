using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardInputController : MonoBehaviour
{
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
}
