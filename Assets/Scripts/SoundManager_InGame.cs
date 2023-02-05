using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_InGame : MonoBehaviour
{
    [SerializeField] private AudioClip countdown;
    [SerializeField] private AudioSource audioSource;

    public void CountdownSound()
    {
        audioSource.PlayOneShot(countdown);
    }
}
