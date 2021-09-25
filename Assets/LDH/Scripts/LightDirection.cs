using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDirection : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPos = transform.position;

        mousePos.z = objPos.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        float dy = target.y - objPos.y;
        float dx = target.x - objPos.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }
}
