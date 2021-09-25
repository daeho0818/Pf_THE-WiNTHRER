using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPuzzleSystem : ObjectScript
{
    public LeverObject answerLever;
    public List<ToggleObject>  answerList = new List<ToggleObject>();
    List<ToggleObject> playerToggleList = new List<ToggleObject>();

    public void AddPlayerToggleList(ToggleObject _lever)
    {
        playerToggleList.Add(_lever);
        ObjectAction();
    }

    public override void ObjectAction()
    {
        if(playerToggleList.Count >= answerList.Count)
        {
            for(int i = 0; i < answerList.Count; i++)
            {
                if(!playerToggleList[i].Equals(answerList[i]))
                {
                    WrongAnswer();
                    return;
                }
            }
            answerLever.ObjectAction();
            // Destroy(this);
        }
    }

    void WrongAnswer()
    {
        for(int i = 0 ; i < playerToggleList.Count; i++)
        {
            playerToggleList[i].ObjectAction();
        }
        playerToggleList.Clear();
    }
}
