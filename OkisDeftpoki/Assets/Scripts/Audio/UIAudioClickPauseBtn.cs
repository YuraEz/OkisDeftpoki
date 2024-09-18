using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioClickPauseBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private string onAudioText;
    [SerializeField] private string offAudioText;

    private Button button;
    private AudioM audioManager;

    private void Start()
    {
        audioManager = ServiceLocator.GetService<AudioM>();

        button = GetComponent<Button>();
        button.onClick.AddListener(egwegwyehweogwbguewbegwbuoegbw);


        if (audioManager.weggwegwe)
        {
            print("true music");
            if (text) text.SetText(offAudioText);
            if (text2) text2.SetText(offAudioText);
            return;
        }
        if (text) text.SetText(onAudioText);
        if (text2) text2.SetText(onAudioText);
    }

    private void egwegwyehweogwbguewbegwbuoegbw()
    {
        if (audioManager.weggwegwe)
        {
            audioManager.MusicSource.UnPause();
            audioManager.weggwegwe = false;
            if (text) text.SetText(onAudioText);
            if (text2) text2.SetText(onAudioText);
            return;
        }
        audioManager.MusicSource.Pause();
        audioManager.weggwegwe = true;
        if (text) text.SetText(offAudioText);
        if (text2) text2.SetText(offAudioText);
    }

}
