using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    RidingHook ridingHook;
    public GameObject targetObject;
    public DistanceJoint2D joint2D;
    private void Start()
    {
        ridingHook = GameObject.Find("Player").GetComponent<RidingHook>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            targetObject = collision.gameObject;
            joint2D.enabled = true;
            ridingHook.isAttack = true;
            ridingHook.animController.SetHookAnim(true);
        }
        else if(!collision.CompareTag("Player") && !collision.CompareTag("ColdZone"))
        {
            Debug.Log(collision.tag);
            ridingHook.isHookEnd = true;
        }
    }
}
