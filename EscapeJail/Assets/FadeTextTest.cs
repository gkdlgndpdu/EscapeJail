using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTextTest : MonoBehaviour
{
    private Text text;
    private float fadeTime = 0.5f;
    private void Awake()
    {
        text = GetComponent<Text>();

    }

    private void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    private enum State
    {
        Down,Up
    }
    IEnumerator FadeRoutine()
    {
        if (text == null) yield break ;
        float alpha = 1f;
        State state = State.Down;
        while (true)
        {
            if(state == State.Down)
            {
                text.color = new Color(1f, 1f, 1f, alpha);
                alpha -= Time.deltaTime;
                if (alpha <= 0)
                {
                    state = State.Up;
                }
            }
            else
            {
                text.color = new Color(1f, 1f, 1f, alpha);
                alpha += Time.deltaTime;
                if (alpha>= 1)
                {
                    state = State.Down;
                }
            }
      
            yield return null;
        }
    }
 
   
}
