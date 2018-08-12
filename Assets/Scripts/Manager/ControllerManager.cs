using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Video;
using SFB;

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

    private void Awake()
    {
        videoPlayerTexture.Release();
        videoPlayerTexture.width = Screen.width;
        videoPlayerTexture.height = Screen.height;
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
        if (videoInputPath == null || videoInputPath == "" || videoInputPath == string.Empty)
        {
            Utils.Instance.ShowAlert("Please choose video!");
            return;
        }
        videoPlayer.url = videoInputPath;
        videoPlayer.prepareCompleted += VideoPlay;
        videoPlayer.Prepare();
    }

    public void OnStopBtnClick()
    {
        videoPlayer.Stop();
        videoPlayerTexture.Release();
        backgroundImage.color = Color.black;
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
        } else
        {
            hideBtnText.text = "Hide";
            controllerPopup.SetActive(true);
        }
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
}

