using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSM : StateMachine
{
    [SerializeField] InputController _input;
    [SerializeField] Dice _dice;
    public InputController Input => _input;
    public Dice Dice => _dice;

    void Start()
    {
        ChangeState<SetupGameState>();
    }
}
