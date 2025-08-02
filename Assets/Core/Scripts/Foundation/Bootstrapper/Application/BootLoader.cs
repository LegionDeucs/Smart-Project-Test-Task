using MyCore.StateMachine;
using SLS;
using UnityEngine;
using Zenject;

public class BootLoader : IInitializable
{
    private readonly ApplicationStateMachine _applicationStateMachine;
    private readonly SaveLoadSystem _sls;

    public BootLoader(ApplicationStateMachine applicationStateMachine, SaveLoadSystem sls)
    {
        _applicationStateMachine = applicationStateMachine;
        _sls = sls;
    }

    public void Initialize()
    {
        Application.targetFrameRate = 60;

        _sls.Init();
    }
}