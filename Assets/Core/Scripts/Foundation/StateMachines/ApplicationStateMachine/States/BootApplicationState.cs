using MyCore.StateMachine;
using SLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BootApplicationState : ApplicationStateMachineBaseState
{
    private readonly SaveLoadSystem saveLoadSystem;

    public BootApplicationState(ApplicationContext context, SaveLoadSystem saveLoadSystem) : base(context)
    {
        this.saveLoadSystem = saveLoadSystem;
    }

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        saveLoadSystem.Init();


        Context.StateMachine.EnterState<LoadingSceneApplicationState>().NextState<MetaApplicationState>();
    }

    public override void OnStateExit()
    {
        
    }
}