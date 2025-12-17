using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fill;
    [SerializeField] private TMP_Text percentageText;

    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float value = 1f;

    [SerializeField] private bool showPercentage = true;

    private void OnValidate()
    {
        Apply();
    }

    public void SetValue(float newValue)
    {
        value = Mathf.Clamp01(newValue);
        Apply();
    }

    public void SetValuePercent(float percent)
    {
        value = Mathf.Clamp01(percent / 100f);
        Apply();
    }

    private void Apply()
    {
        if (fill != null)
        {
            fill.type = Image.Type.Filled;
            fill.fillMethod = Image.FillMethod.Horizontal;
            fill.fillAmount = value;
        }

        if (percentageText != null)
        {
            percentageText.gameObject.SetActive(showPercentage);
            if (showPercentage)
            {
                percentageText.text = Mathf.RoundToInt(value * 100f) + "%";
            }                
        }
    }
}
