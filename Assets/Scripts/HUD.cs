using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text timerText;

    int score;
    const string ScorePrefix = "Score: ";
    const string TimePrefix = "Time: ";
    float elapsedSeconds = 0;
    bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = ScorePrefix + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            timerText.text = TimePrefix + elapsedSeconds.ToString();
        }  
    }

    /// <summary>
    /// Stops the game timer
    /// </summary>
    public void StopGameTimer()
    {
        running = false;
    }

    /// <summary>
    /// Adds points for an asteroid
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = ScorePrefix + score.ToString();
    }
}
