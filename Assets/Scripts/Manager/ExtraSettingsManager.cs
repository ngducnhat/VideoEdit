using Nexweron.Core.MSK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraSettingsManager : MonoBehaviour {
    [SerializeField] private ColorPicker picker;
    [SerializeField] private Image keyColorImage;
    [SerializeField] private ChromaKey_Alpha_General chromaKeyAlphaGeneral;

    void Start () {
        picker.onValueChanged.AddListener(OnKeyColorChanged);
        picker.CurrentColor = Color.green;
    }

    public void OnKeyColorClick()
    {
        picker.gameObject.SetActive(!picker.gameObject.activeSelf);
    }

    public void HideColorPicker()
    {
        picker.gameObject.SetActive(false);
    }

    private void OnKeyColorChanged(Color color)
    {
        keyColorImage.color = color;
        chromaKeyAlphaGeneral.keyColor = color;
    }
}
