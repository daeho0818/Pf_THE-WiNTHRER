using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObejct : ObjectScript
{
    public ToggleObject toggleObject;
    public bool isToggleOpen;
    public GameObject door;
    public Transform[] positoinArray;//0번 닫혔을 때, 1번 열렸을 때
    public AudioSource audioPlayer;

    public float doorOpenSpeed;

    private void Start()
    {
        isToggleOpen = toggleObject.isToggle;
    }

    public override void ObjectAction()
    {
        // Debug.Log(gameObject.name);
        isToggleOpen = toggleObject.isToggle;

        if(isToggleOpen)
        {
            audioPlayer.Play();
        }
    }

    private void Update()
    {
        if(isToggleOpen && !door.transform.position.Equals(positoinArray[1].transform.position))
        {
            door.transform.position = Vector2.MoveTowards(door.transform.position, positoinArray[1].transform.position, doorOpenSpeed * Time.deltaTime);
        }
        else if(!isToggleOpen && !door.transform.position.Equals(positoinArray[0].transform.position))
        {
            door.transform.position = Vector2.MoveTowards(door.transform.position, positoinArray[0].transform.position, doorOpenSpeed * Time.deltaTime);
        }
    }
}
