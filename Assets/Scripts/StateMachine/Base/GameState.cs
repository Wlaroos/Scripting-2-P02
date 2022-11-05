using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameSM))]
public class GameState : State
{
    protected GameSM StateMachine { get; private set; }

    public static event Action StateEnter;
    public static event Action StateExit;

    public override void Enter()
    {
        StateEnter?.Invoke();
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        StateExit?.Invoke();
    }

    private void Awake()
    {
        StateMachine = GetComponent<GameSM>();
    }
}
