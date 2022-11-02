using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public event Action<int> DiceValueReturned = delegate { };

    Rigidbody _rb;

    bool _hasLanded;
    bool _thrown;

    Vector3 initPosition;

    protected int _diceValue;
    public int DiceValue => _diceValue;

    private void OnEnable()
    {
        EnemyTurnGameState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnGameState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void OnEnemyTurnBegan()
    {
        //Destroy(gameObject);
    }

    private void OnEnemyTurnEnded()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        _rb.useGravity = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RollDice();
        }

        if(_rb.IsSleeping() && !_hasLanded && _thrown)
        {
            _hasLanded = true;
            _rb.isKinematic = true;
            _rb.useGravity = false;
            GetDiceValue();
        }
        else if(_rb.IsSleeping() && _hasLanded && _diceValue == -1)
        {
            RollAgain();
        }
    }

    public void RollDice()
    {
        if(!_thrown && !_hasLanded)
        {
            _thrown = true;
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _rb.AddTorque(UnityEngine.Random.Range(0, 750), UnityEngine.Random.Range(0, 750), UnityEngine.Random.Range(0, 750));
            _rb.AddForce(UnityEngine.Random.Range(-200, 200), 0, UnityEngine.Random.Range(75, 200));
        }
        else if(_thrown && _hasLanded)
        {
            DiceReset();
        }
    }

    void DiceReset()
    {
        transform.position = initPosition;
        transform.rotation = Quaternion.identity;
        _rb.isKinematic = true;
        _thrown = false;
        _hasLanded = false;
        _rb.useGravity = false;
    }

    void RollAgain()
    {
        DiceReset();
        _thrown = true;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.AddTorque(UnityEngine.Random.Range(0, 750), UnityEngine.Random.Range(0, 750), UnityEngine.Random.Range(0, 750));
        _rb.AddForce(UnityEngine.Random.Range(-200, 200), 0, UnityEngine.Random.Range(75, 200));
    }

    public int GetDiceValue()
    {
        int iValue = -1;
        Vector3 dieRotation = gameObject.transform.eulerAngles;

        dieRotation = new Vector3(Mathf.RoundToInt(dieRotation.x), Mathf.RoundToInt(dieRotation.y), Mathf.RoundToInt(dieRotation.z));

        if (dieRotation.x == 180 && dieRotation.y == 270 ||
            dieRotation.x == 0 && dieRotation.z == 90)
        {
            iValue = 1;
        }
        else if (dieRotation.x == 270)
        {
            iValue = 2;
        }
        else if (dieRotation.x == 180 && dieRotation.z == 0 ||
          dieRotation.x == 0 && dieRotation.z == 180)
        {
            iValue = 3;
        }
        else if (dieRotation.x == 180 && dieRotation.z == 180 ||
          dieRotation.x == 0 && dieRotation.z == 0)
        {
            iValue = 4;
        }
        else if (dieRotation.x == 90)
        {
            iValue = 5;
        }
        else if (dieRotation.x == 0 && dieRotation.z == 270 ||
          dieRotation.x == 180 && dieRotation.z == 90)
        {
            iValue = 6;
        }

        //Debug.Log("eulerAngles: " + dieRotation.x + ", " + dieRotation.y + ", " + dieRotation.z + " Value: " + iValue);

        _diceValue = iValue;

        DiceValueReturned?.Invoke(_diceValue);

        return iValue;
    }

}
