using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezingUiSystem : MonoBehaviour
{
    public Image[] freezingSprites;

    void FreezingUpdate()
    {
        
        int freezingCount = (int)PlayerInfo.Instance.ColdGuage / (15 / 4);
        if(freezingCount >= 1)
        {
            float freezingLevel = 1 - ((float)PlayerInfo.Instance.ColdGuage % (15f / 4f)) / 4;

            Color tmp = new Color(0.7f, 0.8f, 1);
            tmp.a = freezingLevel;

            for(int i = 0; i < freezingSprites.Length; i++)
            {
                tmp.a = 0;
                freezingSprites[i].color = tmp;
            }

            for(int i = 0 ; i < freezingCount - 1; i++)
            {
                tmp.a = 1;
                freezingSprites[i].color = tmp;
            }

            tmp.a = freezingLevel;
            if(freezingCount < 5)
                freezingSprites[freezingCount - 1].color = tmp;
        }
    }

    void Update()
    {
        FreezingUpdate();
    }
}
