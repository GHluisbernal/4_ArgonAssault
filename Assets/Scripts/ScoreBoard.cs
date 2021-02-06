using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int totalScore;
    Text scoreText;
    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = totalScore.ToString();
    }

    public void ScoreHit(int scorePerHit)
    {
        totalScore += scorePerHit;
        scoreText.text = totalScore.ToString();
    }
}
