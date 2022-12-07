using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    [SerializeField] AudioClip _music;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_music);
    }
}
