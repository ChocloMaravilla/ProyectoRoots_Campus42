using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip hoverClip, clickClip, arrastrarClip;
    [SerializeField] private AudioSource audioSource;

    public void FirstSound()
    {
        audioSource.PlayOneShot(hoverClip);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }

    public void DragSound()
    {
        audioSource.PlayOneShot(arrastrarClip);
    }
}
