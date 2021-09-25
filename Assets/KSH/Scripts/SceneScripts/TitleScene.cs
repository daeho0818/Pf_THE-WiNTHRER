using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    bool clickToScene = false;
    bool isClick = false;
    public CanvasGroup group;

    IEnumerator FadeOut()
    {
        while(true)
        {
            if(group.alpha <= 0)
            {
                NextScene();
                break;
            }
            else
            {
                group.alpha -= 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(!isClick && !clickToScene && Input.GetMouseButtonDown(0))
        {
            clickToScene = true;
            isClick = true;
        }
        if(isClick && clickToScene)
        {
            StartCoroutine(FadeOut());
            isClick = false;
        }
    }
}
