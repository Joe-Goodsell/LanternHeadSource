using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class victoryOrDefectSound : GameManagerClient
{
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip defeat;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameManager.IsVictory)
        {
            audioSource.clip = victory;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = defeat;
            audioSource.Play();
        }
    }
}
