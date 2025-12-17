using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class PopupView : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private float autoHideTime = 2f;    

    private CancellationTokenSource cts;

    private void Awake()
    {
        PopupEvents.OnShowPopup += HandleOnShowPopup;
        PopupEvents.OnClosePopup += HandleOnClosePopup;
    }

    private void OnDestroy()
    {
        PopupEvents.OnShowPopup -= HandleOnShowPopup;
        PopupEvents.OnClosePopup -= HandleOnClosePopup;

        CancelToken();
    }

    private void HandleOnShowPopup(string key)
    {
        container.gameObject.SetActive(true);

        LocalizationEvents.OnRequestLocalizedText?.Invoke(key, text =>
        {
            descriptionTxt.text = text;
        });

        CancelToken();

        cts = new CancellationTokenSource();
        AutoHideAsync(cts.Token).Forget();
    }

    private void HandleOnClosePopup()
    {
        container.gameObject.SetActive(false);
    }

    private async UniTaskVoid AutoHideAsync(CancellationToken token)
    {
        await UniTask.Delay(
                (int)(autoHideTime * 1000),
                cancellationToken: token)
                .SuppressCancellationThrow();

        if (token.IsCancellationRequested)
            return;

        HandleOnClosePopup();
    }

    private void CancelToken()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
}
