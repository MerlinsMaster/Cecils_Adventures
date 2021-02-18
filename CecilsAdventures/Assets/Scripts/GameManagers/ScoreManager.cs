using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;           // the score
    public int pointsToAdd;     // points earned
    public int pointsToLose;    // points lost

    private void Awake()
    {
        SM.scoreManager = this;
    }

    private void Start()
    {
        score = SM.dataManager.score;                            // score is set to zero by default
    }

    private void Update()
    {
        if (score < 0)  // This prevents the score from going below zero
            score = 0;

        //score = pointsToAdd - pointsToLose;     // score is updated, adding points earned and subtracting points lost
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        SM.dataManager.score = score;
    }

    public void LosePoints(int pointsToLose)
    {
        score -= pointsToLose;
        SM.dataManager.score = score;
    }
}
