using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Gauge : UiScripts
{
    public Image gaugeSprite;
    public Image backSprite;

    private void Update()
    {
        gaugeSprite.fillAmount = PlayerInfo.Instance.Battery / PlayerInfo.Instance.MaxBattery;
    }

}
