using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Video;

public class ControllerManager : MonoBehaviour {
    [SerializeField]private InputField videoInput;
    private string videoInputPath;
    [SerializeField] private InputField backgroundInput;
    private string backgroundInputPath;
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture videoPlayerTexture;
    [SerializeField] private RawImage videoPlayerViewport;

    private void Awake()
    {
        videoPlayerTexture.Release();
        videoPlayerTexture.width = Screen.width;
        videoPlayerTexture.height = Screen.height;
    }
    public void OnVideoInputClick()
    {
        videoInputPath = EditorUtility.OpenFilePanel("Choose your video", "", "");
        videoInput.text = videoInputPath.Split('/').Last();
    }

    public void OnBackgroundInputClick()
    {
        backgroundInputPath = EditorUtility.OpenFilePanel("Choose your background", "", "");
        backgroundInput.text = backgroundInputPath.Split('/').Last();
        Texture2D tempTexture = (new WWW(backgroundInputPath)).texture;
        if (tempTexture == null)
        {
            EditorUtility.DisplayDialog("Warning", "Please choose correct background image format!", "OK");
        } else
        {
            backgroundImage.texture = tempTexture;
        }
    }

    public void OnPlayBtnClick()
    {
        if (videoInput == null || videoInputPath == "" || videoInputPath == string.Empty)
        {
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

