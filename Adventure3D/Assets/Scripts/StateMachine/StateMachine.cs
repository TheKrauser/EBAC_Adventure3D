using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictState;

    private StateBase currentState;
    public float timeToStartGame = 1f;

    public StateBase CurrentState
    {
        get { return currentState; }
    }

    public void Init()
    {
        dictState = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T typeEnum, StateBase state)
    {
        dictState.Add(typeEnum, state);
    }

    public void SwitchState(T state)
    {
        if (currentState != null) currentState.OnStateExit();

        currentState = dictState[state];
        currentState.OnStateEnter();
    }

    public void Update()
    {
        if (currentState != null) currentState.OnStateStay();
    }
}
