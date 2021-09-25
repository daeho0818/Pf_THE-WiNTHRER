using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    public static TorchManager instance;
    TorchObject currenTorch;

    private void Start()
    {
        instance = this;
    }

    public void TorchToggle(TorchObject _torch)
    {
        if(currenTorch != null)
            currenTorch.TurnOff();
        currenTorch = _torch;
        currenTorch.TurnOn();
    }
}
