using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip footstepSoundEffect;

    public void PlayFootstepSound() //Invoke in walk animation
    {
        //playerAudio.clip = footstepSoundEffect;
        //playerAudio.Play();
    }

    public void PlayJumpSound()
    {

    }
}
