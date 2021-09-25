using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL ddol { get; private set; } = null;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (ddol)
        {
            Destroy(gameObject);
            return;
        }
        PlayerPrefs.SetFloat("PosX", 20);
        PlayerPrefs.SetFloat("PosY", -3);

        ddol = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
