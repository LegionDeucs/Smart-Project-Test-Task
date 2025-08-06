using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text stepsCounterText;
    public void UpdateStepsCounter(int steps)
    {
        stepsCounterText.text = steps.ToString();
    }
}
