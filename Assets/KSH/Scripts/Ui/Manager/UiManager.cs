using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : UiScripts
{
    public static UiManager instance;

    public bool onMenu
    {
        get {return menu.gameObject.activeSelf;}
    }

    public UiMenu menu;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.ToggleMenu();
        }
    }
}
