using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StoreLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] bgList;
    [SerializeField] private Image[] bgImg;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    void Update()
    {
        int CurrentBG = PlayerPrefs.GetInt("bg", 0);
 
        foreach (Image img in bgImg)
        {
            img.sprite = bgList[CurrentBG];
        }
    }
}
