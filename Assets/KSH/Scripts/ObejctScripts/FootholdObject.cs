using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootholdObject : ToggleObject
{
    public GameObject spriteObj;
    public Transform[] positionArray;
    public float pushSpeed;
    bool isChanged = false;
    public override void ObjectAction()
    {
        isChanged = true;
        DoorToggle();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Box"))
        {
            isToggle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Box"))
        {
            isToggle = false;
        }
    }

    public void DoorToggle()
    {
        for (int i = 0; i < Door.Length; i++)
        {
            Door[i].ObjectAction();
        }
    }

    private void Update()
    {
        if(!isToggle)
        {
            if(spriteObj.transform.position.Equals(positionArray[1].position) && !isChanged)
            {
                ObjectAction();
            }
            else 
            {
                isChanged = false;
                spriteObj.transform.position = Vector2.MoveTowards(spriteObj.transform.position, positionArray[1].position, pushSpeed * Time.deltaTime);
            }
        }
        else 
        {
            if(spriteObj.transform.position.Equals(positionArray[0].position) && !isChanged)
            {
                ObjectAction();
            }
            else 
            {
                isChanged = false;
                spriteObj.transform.position = Vector2.MoveTowards(spriteObj.transform.position, positionArray[0].position, pushSpeed * Time.deltaTime);
            }
        }
    }
}
