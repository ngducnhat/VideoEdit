using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Reflection;

public class Utils : MonoBehaviour {
    public static Utils Instance;

    [SerializeField] private TextMeshProUGUI alertText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ShowAlert(string message)
    {
        if (alertText != null)
        {
            alertText.text = message;
            alertText.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(() => {
                Invoke("HideAlert", 1f);
            });
        }
    }

    public void HideAlert()
    {
        if (alertText != null)
        {
            alertText.GetComponent<CanvasGroup>().DOFade(0f, 1f);
        }
    }
}
