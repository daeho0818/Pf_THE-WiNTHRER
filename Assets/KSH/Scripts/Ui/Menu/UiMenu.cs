using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenu : MonoBehaviour
{
    bool isSetting = false;

    public void ToggleMenu()
    {
        if(isSetting)
        {
            ToggleSetting();
        }
        else 
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        ToggleMenu();
    }

    public void ToggleSetting()
    {
        //toggle SettingMenu
    }

    public void GotoTitle()
    {
        //todo titleScene
    }
}
