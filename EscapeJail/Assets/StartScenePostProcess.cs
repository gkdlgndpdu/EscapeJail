using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class StartScenePostProcess : MonoBehaviour
{

    private PostProcessingProfile postProcessBehavior;

    private BloomModel.Settings bloom;

    private void Awake()
    {
        postProcessBehavior = GetComponent<PostProcessingBehaviour>().profile;

        bloom = postProcessBehavior.bloom.settings;

        bloom.bloom.intensity = 0f;
    }
    private void Start()
    {
        StartCoroutine(EffectRoutine());
    }
    private enum State
    {
        Down, Up
    }

    IEnumerator EffectRoutine()
    {
  
        float alpha = 1f;
        State state = State.Down;
        while (true)
        {
            if (state == State.Down)
            {
                SetBrightnsee(alpha);
                alpha -= Time.deltaTime;
                if (alpha <= 0)
                {
                    state = State.Up;
                }
            }
            else
            {
                SetBrightnsee(alpha);
                alpha += Time.deltaTime;
                if (alpha >= 1)
                {
                    state = State.Down;
                }
            }

            yield return null;
        }
    }

    private void SetBrightnsee(float value)
    {
        if (postProcessBehavior == null) return;
        bloom = postProcessBehavior.bloom.settings;
        bloom.bloom.intensity = value*4f;
        postProcessBehavior.bloom.settings = bloom;
    }


}
