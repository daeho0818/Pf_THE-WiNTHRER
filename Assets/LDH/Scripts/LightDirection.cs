using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDirection : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 objPos;
    Vector3 target;

    float dx, dy, rotateDegree;
    void Update()
    {
        mousePos = Input.mousePosition;
        objPos = transform.position;

        mousePos.z = objPos.z - Camera.main.transform.position.z;

        target = Camera.main.ScreenToWorldPoint(mousePos);

        dy = target.y - objPos.y;
        dx = target.x - objPos.x;

        rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }
}
