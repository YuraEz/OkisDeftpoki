using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISCREENCHANGER : MonoBehaviour
{
    [SerializeField] private string screenName;

    [SerializeField] private Animator effect;
    [SerializeField] private float delay;

    private Button button;
    private UIMANAGER uiManager;
    private bool isClicked;

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIMANAGER>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (isClicked) return;

            isClicked = true;

            if (effect) effect.SetTrigger("exit");

            Invoke(nameof(ChangeScreen), delay);
        });
    }

    private void ChangeScreen()
    {
        uiManager.ChangeScreen(screenName);
        isClicked = false;
    }

}
