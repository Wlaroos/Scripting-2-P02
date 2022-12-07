using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SweetSpot : MonoBehaviour
{
    [SerializeField] GameSM _SMRef;

    [SerializeField] Slider _sliderRef;
    [SerializeField] float sliderSpeed = 100f;

    [SerializeField] AudioClip _minigameErrorSFX;
    [SerializeField] AudioClip _minigameWinSFX;
    [SerializeField] AudioClip _minigameTickSFX;

    bool isSliding = false;
    bool isDirectionUp = true;
    float amtSlider = 0.0f;

    private void OnEnable()
    {
        StartSlider();
    }

    private void OnDisable()
    {
        isSliding = false;
        isDirectionUp = true;
        amtSlider = 0.0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndSlider();
        }

        if (isSliding)
        {
            SliderActive();
        }
    }

    void SliderActive()
    {
        if(isDirectionUp)
        {
            amtSlider += Time.deltaTime * sliderSpeed;
            if(amtSlider > 100)
            {
                isDirectionUp = false;
                amtSlider = 100f;
            }
        }
        else
        {
            amtSlider -= Time.deltaTime * sliderSpeed;
            if (amtSlider < 0)
            {
                isDirectionUp = true;
                amtSlider = 0f;
            }
        }

        if(_sliderRef.value % 2 == 0) AudioManager.Instance.PlaySound2D(_minigameTickSFX, .5f, UnityEngine.Random.Range(0.95f,1.05f));

        _sliderRef.value = amtSlider;

    }

    public void StartSlider()
    {
        isSliding = true;
        amtSlider = 0.0f;
        isDirectionUp = true;
    }

    public void EndSlider()
    {
        isSliding = false;
        Debug.Log(amtSlider);
        if (amtSlider >= 48 && amtSlider <= 52)
        {
            Debug.Log("Nice! Extra Dice!");
            AudioManager.Instance.PlaySound2D(_minigameWinSFX,1);
            _SMRef.SetExtraDice(1);
        }
        else
        {
            Debug.Log("Booo... No Dice For Losers!");
            AudioManager.Instance.PlaySound2D(_minigameErrorSFX, 1);
            _SMRef.SetExtraDice(0);
        }

        _SMRef.OnStateExit();
        _SMRef.ChangeState<PlayerTurnGameState>();
    }
}
