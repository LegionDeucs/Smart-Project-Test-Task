using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Transform ScoreParent;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Button startLevelButton;

    public event System.Action OnStartLevelButtonClicked;

    private void Start()
    {
        startLevelButton.onClick.AddListener(() => OnStartLevelButtonClicked?.Invoke());
    }

    public void SetUpScore(string score)
    {
        ScoreParent.SetActive();

        scoreText.text = score;
    }
}
