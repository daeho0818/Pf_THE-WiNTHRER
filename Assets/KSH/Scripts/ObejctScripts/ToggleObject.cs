using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : ObjectScript
{
    public bool isToggle;
    public ObjectScript[] Door;

    public virtual void CancelToggle()
    {
        Debug.Log("취소");
    }

    
}
