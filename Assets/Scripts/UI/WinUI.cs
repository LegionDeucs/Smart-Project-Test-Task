using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private Transform content;
    [SerializeField] private float fadeDuration;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text stepsText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Transform newRecordText;

    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button NewGameButton;

    [SerializeField] private float textRevealDuration = .5f;

    private Tween fadeTween;

    private const string TIME_TEMPLATE = "Your time: ";
    private const string STEPS_TEMPLATE = "Your steps: ";
    private const string SCORE_TEMPLATE = "Your score: ";

    public event System.Action OnMainMenuButtonClicked;
    public event System.Action OnNewGameButtonClicked;

    private void Start()
    {
        MainMenuButton.onClick.AddListener(()=>OnMainMenuButtonClicked?.Invoke());
        NewGameButton.onClick.AddListener(() => OnNewGameButtonClicked?.Invoke());
    }

    public void Show(int time, int steps, int score, bool isMaxScore)
    {
        fadeTween?.Kill();

        fader.alpha = 0;
        content.SetActive();

        fadeTween = fader.DOFade(1, fadeDuration);
        AnimateText(time, steps, score, isMaxScore);
    }

    private void AnimateText(int time, int steps, int score, bool isMaxScore)
    {
        timeText.text = TIME_TEMPLATE + time;
        stepsText.text = STEPS_TEMPLATE + steps;
        scoreText.text = SCORE_TEMPLATE + score;

        newRecordText.transform.localScale = Vector3.zero;

        timeText.maxVisibleCharacters = 0;
        stepsText.maxVisibleCharacters = 0;
        scoreText.maxVisibleCharacters = 0;
        Sequence textAppearSequence = DOTween.Sequence();

        textAppearSequence.Append(
        DOTween.To(() => timeText.maxVisibleCharacters,
           x => timeText.maxVisibleCharacters = x,
           timeText.text.Length,
           textRevealDuration));

        textAppearSequence.Append(
        DOTween.To(() => stepsText.maxVisibleCharacters,
           x => stepsText.maxVisibleCharacters = x,
           stepsText.text.Length,
           textRevealDuration));
        textAppearSequence.Append(
        DOTween.To(() => scoreText.maxVisibleCharacters,
           x => scoreText.maxVisibleCharacters = x,
           scoreText.text.Length,
           textRevealDuration));

        if (isMaxScore)
            textAppearSequence.Append(
                newRecordText.DOScale(1, textRevealDuration)
                );
    }

    public void ForceHide()
    {
        content.SetInactive();
        fader.alpha = 0;
    }
} 
