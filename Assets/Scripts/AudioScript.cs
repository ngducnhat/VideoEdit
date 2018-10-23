using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public string microphone;
    public AudioSource AudioSource;

    // Use this for initialization
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        //Get all available microphone
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Found microphone device: " + device);
            if (microphone == null)
            {
                //set default mic to first mic found
                microphone = device;
            }
        }
        if (microphone == null) microphone = "Built-in Microphone";
        UpdateMicrophone();
    }

    // Update is called once per frame
    void UpdateMicrophone()
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
