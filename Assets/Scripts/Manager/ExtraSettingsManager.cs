using Nexweron.Core.MSK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraSettingsManager : MonoBehaviour {
    [SerializeField] private ControllerManager controllerManager;
    [SerializeField] private ColorPicker picker;
    [SerializeField] private Image keyColorImage;
    [SerializeField] private ChromaKey_Alpha_General chromaKeyAlphaGeneral;
    [SerializeField] private Blur_General blurGeneral;
    [SerializeField] private MaskAlpha_Expert maskAlphaExpert;
    [SerializeField] private Dropdown cameraDropdown;
    [SerializeField] private Dropdown audioDropdown;
    //SLider
    [SerializeField] private Slider dChromaSlider;
    [SerializeField] private Text dChromaValue;
    [SerializeField] private Slider dChromaTSlider;
    [SerializeField] private Text dChromaTValue;
    [SerializeField] private Slider dLumaSlider;
    [SerializeField] private Text dLumaValue;
    [SerializeField] private Slider dLumaTSlider;
    [SerializeField] private Text dLumaTValue;
    [SerializeField] private Slider blurXSlider;
    [SerializeField] private Text blurXValue;
    [SerializeField] private Slider blurYSlider;
    [SerializeField] private Text blurYValue;
    [SerializeField] private Slider alphaEdgeSlider;
    [SerializeField] private Text alphaEdgeValue;
    [SerializeField] private Slider alphaPowSlider;
    [SerializeField] private Text alphaPowValue;


    void Start () {
        picker.onValueChanged.AddListener(OnKeyColorChanged);
        picker.CurrentColor = Color.green;

        cameraDropdown.AddOptions(controllerManager.listCameraDevices);
        if (PlayerPrefs.HasKey("CurrentCameraIndex"))
        {
            cameraDropdown.value = PlayerPrefs.GetInt("CurrentCameraIndex");
        }

        //Debug.Log(controllerManager.listAudioDevices[0] + "exist on the list");
        audioDropdown.AddOptions(controllerManager.listAudioDevices);
        if (PlayerPrefs.HasKey("CurrentAudioIndex"))
        {
            audioDropdown.value = PlayerPrefs.GetInt("CurrentAudioIndex");
        }

        dChromaSlider.value = chromaKeyAlphaGeneral.dChroma;
        dChromaValue.text = dChromaSlider.value.ToString();
        dChromaSlider.onValueChanged.AddListener((float value) => {dChromaValue.text = value.ToString(); });

        dChromaTSlider.value = chromaKeyAlphaGeneral.dChromaT;
        dChromaTValue.text = dChromaTSlider.value.ToString();
        dChromaTSlider.onValueChanged.AddListener((float value) => { dChromaTValue.text = value.ToString(); });

        dLumaSlider.value = chromaKeyAlphaGeneral.dLuma;
        dLumaValue.text = dLumaSlider.value.ToString();
        dLumaSlider.onValueChanged.AddListener((float value) => { dLumaValue.text = value.ToString(); });

        dLumaTSlider.value = chromaKeyAlphaGeneral.dLumaT;
        dLumaTValue.text = dLumaTSlider.value.ToString();
        dLumaTSlider.onValueChanged.AddListener((float value) => { dLumaTValue.text = value.ToString(); });

        blurXSlider.value = blurGeneral.blurX;
        blurXValue.text = blurXSlider.value.ToString();
        blurXSlider.onValueChanged.AddListener((float value) => { blurXValue.text = value.ToString(); });

        blurYSlider.value = blurGeneral.blurY;
        blurYValue.text = blurYSlider.value.ToString();
        blurYSlider.onValueChanged.AddListener((float value) => { blurYValue.text = value.ToString(); });

        alphaEdgeSlider.value = maskAlphaExpert.alphaEdge;
        alphaEdgeValue.text = alphaEdgeSlider.value.ToString();
        alphaEdgeSlider.onValueChanged.AddListener((float value) => { alphaEdgeValue.text = value.ToString(); });

        alphaPowSlider.value = maskAlphaExpert.alphaPow;
        alphaPowValue.text = alphaPowSlider.value.ToString();
        alphaPowSlider.onValueChanged.AddListener((float value) => { alphaPowValue.text = value.ToString(); });

    }

    public void OnCameraChanged(int index)
    {
        PlayerPrefs.SetInt("CurrentCameraIndex", index);
        controllerManager.CurrentCameraIndex = index;
    }

    public void OnAudioChanged(int index)
    {
        PlayerPrefs.SetInt("CurrentAudioIndex", index);
        controllerManager.CurrentAudioIndex = index;
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
