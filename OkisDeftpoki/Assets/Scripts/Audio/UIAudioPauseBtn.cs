using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioPauseBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text2;



    [SerializeField] private string onAudioText;
    [SerializeField] private string offAudioText;


    [Space]
    [SerializeField] private Image img;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

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
            if (img) img.sprite = offSprite;
            return;
        }
        if (text) text.SetText(onAudioText);
        if (text2) text2.SetText(onAudioText);
        if (img) img.sprite = onSprite;
    }

    private void egwegwyehweogwbguewbegwbuoegbw()
    {
        if (audioManager.weggwegwe)
        {
            audioManager.MusicSource.UnPause();
            audioManager.weggwegwe = false;
            if (text) text.SetText(onAudioText);
            if (text2) text2.SetText(onAudioText);
            if (img) img.sprite = onSprite;
            return;
        }
        audioManager.MusicSource.Pause();
        audioManager.weggwegwe = true;
        if (text) text.SetText(offAudioText);
        if (text2) text2.SetText(offAudioText);
        if (img) img.sprite = offSprite;
    }

}
