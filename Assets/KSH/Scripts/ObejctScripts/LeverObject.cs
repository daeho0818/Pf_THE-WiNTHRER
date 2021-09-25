using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : ToggleObject
{
    public Animator animator;
    public bool isAction = false;
    public bool puzzleLever = false;

    public SequentialPuzzleSystem puzzleSystem;

    private void Start()
    {
        animator.SetBool("LeverBool", isToggle);
    }

    public override void ObjectAction()
    {
        isToggle = !isToggle;
        animator.SetBool("LeverBool", isToggle);
        isAction = true;
    }

    public void DoorToggle()
    {
        if(!puzzleLever && Door.Length > 0)
        {
            for (int i = 0; i < Door.Length; i++)
            {
                Door[i].ObjectAction();
            }
        }
        else if(puzzleLever && Door.Length > 0 && isToggle)
        {
            puzzleSystem.AddPlayerToggleList(this);
        }
        isAction = false;
    }

}
