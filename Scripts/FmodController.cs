using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodController : MonoBehaviour
{
    public void playAmbience()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/FogWind");
    }
    public void playSniff()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/Sniff");
    }
    public void playWalk()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/DogMovement/DogWalk");
    }
    public void playBreathing()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/Breathing");
    }
    public void playJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/DogMovement/DogJump");
    }
    public void playLand()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/DogMovement/DogLand");
    }
    public void playDig()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DogSounds/Digging");
    }
    public void playPickUp()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/ItemSounds/PickupItem");
    }
    public void playRefuelFire()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/ItemSounds/RefuelFire");
    }
    public void playGhostScream()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ghost/SpiritScream");
    }
    public void playGhostAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ghost/SpiritAttack");
    }
    public void playGhostMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/ScaryMusic");
    }
    public void playCreditMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/CreditMusic");
    }
    public void playManScream()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/ManScream");
    }
}