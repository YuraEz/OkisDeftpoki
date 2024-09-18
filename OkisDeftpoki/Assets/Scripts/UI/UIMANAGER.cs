using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class UIMANAGER : MonoBehaviour
{
    [SerializeField] private string startScreen;
    [SerializeField] private UISCREEN[] screens;

    [Space]
    private UISCREEN curScreen;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Awake()
    {
        foreach (UISCREEN screen in screens)
        {
            screen.Init();
        }

        ChangeScreen(startScreen);
    }

    private void Update()
    {
        if (curScreen)
        {
            curScreen.UpdateScreen();

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = curScreen.CursorLockMode;
            }
        }
    }

    private UISCREEN GetScreen(string screenName)
    {
        foreach (UISCREEN screen in screens)
        {
            if (screen.ScreenName == screenName) return screen;
        }

       // return "menu";
        throw new System.Exception($"{screenName} экрана не существует!");
    }

    public void ChangeScreen(string screenName)
    {
        UISCREEN nextScreen = GetScreen(screenName);

        if (curScreen && nextScreen != curScreen)
        {
            curScreen.HideScreen();
        }

        curScreen = nextScreen;
        curScreen.ShowScreen();
        Cursor.lockState = curScreen.CursorLockMode;
    }
}
