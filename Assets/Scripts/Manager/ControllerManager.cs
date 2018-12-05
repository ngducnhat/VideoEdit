using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Video;
using SFB;
using RenderHeads.Media.AVProLiveCamera;

public class ControllerManager : MonoBehaviour {
    [SerializeField] private InputField videoInput;
    private string videoInputPath;
    [SerializeField] private InputField backgroundInput;
    private string backgroundInputPath;
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture videoPlayerTexture;
    [SerializeField] private RawImage videoPlayerViewport;
    [SerializeField] private GameObject controllerPopup;
    [SerializeField] private Text hideBtnText;
    [SerializeField] private GameObject extraSettingsPopup;
    private AVProLiveCameraDevice device;
    [SerializeField] private AVProLiveCamera liveCamera;
    [SerializeField] public AVProLiveCameraManager liveCameraManager;

    public string microphone;
    public AudioSource AudioSource;
    //[SerializeField] public AudioManager audioManager;

    public List<string> listCameraDevices;
    public List<string> listAudioDevices;

    private int currentCameraIndex;
    private int currentAudioIndex;
    public int CurrentCameraIndex
    {
        get
        {
            return currentCameraIndex;
        }
        set
        {
            currentCameraIndex = value;
        }
    }

    public int CurrentAudioIndex
    {
        get
        {
            return currentAudioIndex;
        }
        set
        {
            currentAudioIndex = value;
        }
    }
    private void Awake()
    {
        videoPlayerTexture.Release();
        videoPlayerTexture.width = Screen.width;
        videoPlayerTexture.height = Screen.height;
        extraSettingsPopup.SetActive(false);
        videoPlayerViewport.color = new Color(videoPlayerViewport.color.r, videoPlayerViewport.color.g, videoPlayerViewport.color.b, 0f);
    }

    private void Start()
    {
        listCameraDevices = new List<string>();
        Debug.Log("==========NumDevices=========" + liveCameraManager.NumDevices);
        for (int i = 0; i < liveCameraManager.NumDevices; i++)
        {
            listCameraDevices.Add(liveCameraManager.GetDevice(i).Name);
        }


        listAudioDevices = new List<string>();

        AudioSource = GetComponent<AudioSource>();

        //Get all available microphone
        foreach (string device in Microphone.devices)
        {
            //Debug.Log("Found microphone device: " + device);
            listAudioDevices.Add(device);
        }
    }
    public void OnVideoInputClick()
    {
        string[] tempArray = StandaloneFileBrowser.OpenFilePanel("Choose your video", PlayerPrefs.GetString("videoInputOldPath"), "", false);
        if (tempArray.Length > 0)
        {
            videoInputPath = tempArray[0];
            videoInput.text = videoInputPath.Split('\\').Last();
            PlayerPrefs.SetString("videoInputOldPath", videoInputPath.Remove(videoInputPath.Length - videoInput.text.Length));
        }
    }

    public void OnBackgroundInputClick()
    {
        string[] tempArray = StandaloneFileBrowser.OpenFilePanel("Choose your background", PlayerPrefs.GetString("backgroundInputOldPath"), "", false);
        if (tempArray.Length > 0)
        {
            backgroundInputPath = tempArray[0];
            backgroundInput.text = backgroundInputPath.Split('\\').Last();
            PlayerPrefs.SetString("backgroundInputOldPath", backgroundInputPath.Remove(backgroundInputPath.Length - backgroundInput.text.Length));
            Texture2D tempTexture = (new WWW(backgroundInputPath)).texture;
            backgroundImage.texture = tempTexture;
        }
    }

    public void OnPlayBtnClick()
    {
        videoPlayerViewport.color = new Color(videoPlayerViewport.color.r, videoPlayerViewport.color.g, videoPlayerViewport.color.b, 1f);
        liveCamera._deviceSelection = AVProLiveCamera.SelectDeviceBy.Index;
        liveCamera._desiredDeviceIndex = currentCameraIndex;
        liveCamera.Begin();
        //if (videoInputPath == null || videoInputPath == "" || videoInputPath == string.Empty)
        //{
        //    Utils.Instance.ShowAlert("Please choose video!");
        //    return;
        //}
        //videoPlayer.url = videoInputPath;
        //videoPlayer.prepareCompleted += VideoPlay;
        //videoPlayer.Prepare();

        microphone = listAudioDevices[currentAudioIndex];
        Debug.Log("Current microphone " + microphone);
        UpdateMicrophone();


    }

    public void OnStopBtnClick()
    {
        videoPlayerViewport.color = new Color(videoPlayerViewport.color.r, videoPlayerViewport.color.g, videoPlayerViewport.color.b, 0f);
        liveCamera.StopAllCoroutines();
        //videoPlayer.Stop();
        //videoPlayerTexture.Release();
        //backgroundImage.color = Color.black;

        AudioSource.Stop();

    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }

    public void OnHideBtnClick()
    {
        if (controllerPopup.activeSelf == true)
        {
            hideBtnText.text = "Show";
            controllerPopup.SetActive(false);
            extraSettingsPopup.SetActive(false);
        } else
        {
            hideBtnText.text = "Hide";
            controllerPopup.SetActive(true);
        }
    }

    public void OnExtraSettingsBtnClick()
    {
        extraSettingsPopup.SetActive(!extraSettingsPopup.activeSelf);
    }

    public void VideoPlay(VideoPlayer player)
    {
        //float screenRatio = (float)Screen.width / (float)Screen.height;
        //float videoRatio = (float)videoPlayer.texture.width / (float)videoPlayer.texture.height;
        //videoPlayerViewport.rectTransform.sizeDelta = (screenRatio > videoRatio) ? new Vector2(Screen.height * videoRatio, Screen.height) : new Vector2(Screen.width, Screen.width / videoRatio);
        backgroundImage.color = Color.white;
        player.prepareCompleted -= VideoPlay;
        player.Play();
    }

    public void UpdateMicrophone()
    {
        AudioSource.Stop();
        AudioSource.clip = Microphone.Start(microphone, true, 10, 44100);
        AudioSource.loop = true;

        if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0)) { }
            AudioSource.Play();
        }
        else
        {
            Debug.Log(microphone + "does not work!");
        }
    }
}

