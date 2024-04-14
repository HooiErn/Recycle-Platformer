using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{

    public int pointCount;
 //   public int highScore;
    public Text scoreText;
    //  public Text highScoreText;

    private void Start()
    {
       // highScore = PlayerPrefs.GetInt("HighScore", 0);
       // UpdateHighScoreText();

    }
    void Update()
    {
        scoreText.text = "Score: "+ pointCount.ToString();
    }

    public void UpdateHighScoreText()
    {
       // highScoreText.text = "High Score: " + highScore.ToString();
    }
}
