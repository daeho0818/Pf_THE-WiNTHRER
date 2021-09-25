using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchObject : MonoBehaviour
{
    public GameObject[] particles;
    public AudioSource auioPlayer;

    public void TurnOff()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].gameObject.SetActive(false);
        }
    }
    public void TurnOn()
    {
        auioPlayer.Play();
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].gameObject.SetActive(true);
        }
    }
}
