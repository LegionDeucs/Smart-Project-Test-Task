using MyCore.StateMachine;
using SLS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MetaSceneContext : MonoBehaviour
{
    [Inject] private ApplicationContext applicationContext;
    [Inject] private MainMenuUI mainMenuUI;
    [Inject] private SaveLoadSystem saveLoadSystemCache;

    void Start()
    {
        mainMenuUI.OnStartLevelButtonClicked += MainMenuUI_OnStartLevelButtonClicked;

        if (saveLoadSystemCache.saveLoadSystemCache.scoreSaveData.MaxScore != -1)
            mainMenuUI.SetUpScore(saveLoadSystemCache.saveLoadSystemCache.scoreSaveData.MaxScore.ToString());

    }

    private void MainMenuUI_OnStartLevelButtonClicked()
    {
        applicationContext.StateMachine.EnterState<LoadingSceneApplicationState>().NextState<GameApplicationState>();
    }
}
