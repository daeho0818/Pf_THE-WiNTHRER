using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Bar : UiScripts
{
    public Image[] bars;
    public Image iconImage;
    public int barCount;

    void Start()
    {
        // barCount = (int)UiManager.instance.player.curHp;
    }

    public void SetActiveBar(bool _active)
    {
        gameObject.SetActive(_active);
    }

    void SetBarSpriteColor(float _a)
    {
        Color tmp;
        for(int i = 0 ; i < bars.Length; i++)
        {
            tmp = bars[i].color;
            tmp.a = _a;
            bars[i].color = tmp;
        }
        tmp = iconImage.color;
        tmp.a = _a;
        iconImage.color = tmp;
    }
    
    void OnEnable()
    {
        SetBarSpriteColor(1);
    }

    void FadeAway()
    {
        if(bars[0].color.a <= 0)
        {
            SetActiveBar(false);
        }
        else 
        {
            SetBarSpriteColor(bars[0].color.a - 0.04f);
        }
    }

    void OffBar(int _power, bool _isDamage)
    {
        if(_isDamage)
        {
            for(int i = 0 ; i < _power; i++)
            {
                barCount -= 1;
                if(barCount < 0)
                    barCount = 0;
                else 
                    bars[barCount].gameObject.SetActive(false);
                
            }
        }
        else if(!_isDamage)
        {
            for(int i = 0 ; i < _power; i++)
            {
                if(barCount < 5)
                {
                    barCount += 1;
                    bars[barCount - 1].gameObject.SetActive(true);
                }
            }
        }
    }

    public void BarDamaged(int _power, bool _isDamage)
    {
        CancelInvoke();
        SetBarSpriteColor(1);
        OffBar(_power, _isDamage);
        InvokeRepeating("FadeAway", 2f, 0.1f);
    }

}
