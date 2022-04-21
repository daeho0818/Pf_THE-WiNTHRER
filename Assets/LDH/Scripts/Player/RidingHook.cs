using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingHook : MonoBehaviour
{
    public PlayerCharacterAnimationControl animController;

    GameObject lightObj;

    public Transform hook;

    LineRenderer lineRenderer;

    Vector2 mouseDir;

    [HideInInspector]
    public bool isAttack = false;
    public bool isHookEnd = false;
    bool isRiding = false;
    bool lMouse = false;

    void Start()
    {
        lineRenderer = hook.GetComponentInChildren<LineRenderer>();

        lineRenderer.positionCount = 2;

        lineRenderer.endWidth = 0.05f;
        lineRenderer.startWidth = 0.05f;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hook.position);

        lineRenderer.useWorldSpace = true;

        lightObj = transform.GetChild(0).gameObject;

        animController = GetComponent<PlayerCharacterAnimationControl>();
    }

    void Update()
    {
        LightItem();
        HookItem();
    }

    void LightItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lightObj.SetActive(!lightObj.activeSelf);

            if (lightObj.activeSelf)
            {
                StartCoroutine(DownBattery());
            }
            else
            {
                StartCoroutine(UpBattery());
            }
        }
    }

    bool downing = false;
    IEnumerator DownBattery()
    {
        if (downing) yield break;

        downing = true;
        upping = false;

        StopCoroutine(UpBattery());

        while (PlayerInfo.Instance.Battery > 0)
        {
            if (!lightObj.activeSelf)
            {
                downing = false;
                yield break;
            }
            yield return new WaitForSeconds(1);
            PlayerInfo.Instance.Battery--;
        }

        lightObj.SetActive(false);
        downing = false;

        StartCoroutine(UpBattery());
    }

    bool upping = false;
    IEnumerator UpBattery()
    {
        if (upping) yield break;

        StopCoroutine(DownBattery());

        upping = true;
        downing = false;

        while (PlayerInfo.Instance.Battery < PlayerInfo.Instance.MaxBattery)
        {
            if (lightObj.activeSelf)
            {
                upping = false;
                yield break;
            }
            yield return new WaitForSeconds(0.75f);
            PlayerInfo.Instance.Battery++;
        }

        upping = false;
    }

    void HookItem()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(1))
        {
            lMouse = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            lMouse = false;
        }

        if (!isRiding && lMouse)
        {
            hook.position = transform.position;
            hook.gameObject.SetActive(true);
            mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            isRiding = true;
        }

        if (isRiding && !isHookEnd && !isAttack)
        {
            hook.Translate(mouseDir * Time.deltaTime * 60);
            if (Vector2.Distance(transform.position, hook.position) >= 5)
            {
                isHookEnd = true;
            }
        }

        else if (isRiding && isHookEnd && !isAttack)
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 60);

            if (Vector2.Distance(hook.position, transform.position) <= 0.1f)
            {
                isRiding = false;
                isHookEnd = false;
                hook.gameObject.SetActive(false);
            }
        }
        else if (isAttack)
        {
            if (!lMouse)
            {
                isAttack = false;
                isRiding = false;
                isHookEnd = false;

                mouseDir = Vector2.zero;

                Hook h = hook.GetComponent<Hook>();
                h.targetObject = null;
                h.joint2D.enabled = false;

                hook.gameObject.SetActive(false);
                animController.SetHookAnim(false);
            }
        }
    }
}
