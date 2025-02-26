using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        State01,
        State02,
        State03
    }

    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();

        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.State01, new StateBase());
    }
}
