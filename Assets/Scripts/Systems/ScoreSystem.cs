using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private float baseScoreMultiplier = 1000;
    [SerializeField] private float stepsMultiplier = 1;
    [SerializeField] private float timeMultiplier = 0.5f;

    public int CalculateScore(Vector2Int mazeSize, TimeSpan mazeCompleteDuration, int stepsToComplete)
    {
        return (int)(mazeSize.x * mazeSize.y * baseScoreMultiplier / (mazeCompleteDuration.TotalSeconds * timeMultiplier + stepsToComplete * stepsMultiplier));
    }
}
