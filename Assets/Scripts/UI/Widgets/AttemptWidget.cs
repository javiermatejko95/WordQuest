using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI positionTxt;
    [SerializeField] private TextMeshProUGUI numberAttemptTxt;
    [SerializeField] private ProgressBar progressBar;

    public void SetData(float percentage, string numberAttempt)
    {
        progressBar.SetValue(percentage);
        numberAttemptTxt.text = numberAttempt;
    }
}
