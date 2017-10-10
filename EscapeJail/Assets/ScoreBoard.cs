using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    private Text text;
    private int nowScore;
    private int tempScore;
    private float lerpSpeed = 2f;
    private float scoreCount = 0;

    private void Awake()
    {
        Instance = this;
        text = GetComponent<Text>();
    }

    public void GetScore()
    {
        nowScore += 100;
        scoreCount = 0f;
    }

    private void ScoreUpRoutine()
    {
        if (scoreCount <= 1f)
            scoreCount += Time.deltaTime * lerpSpeed;

        tempScore = (int)Mathf.Round(Mathf.Lerp(tempScore, nowScore, scoreCount));
        text.text = tempScore.ToString();
    }

	
	
	void Update ()
    {
        ScoreUpRoutine();    
    }
}
