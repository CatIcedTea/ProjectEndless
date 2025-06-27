using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource ambienceSource;
    public AudioSource sfx;

    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip ambience;
    public AudioClip menuAccept;
    public AudioClip menuCancel;
    public AudioClip menuHover;
    public AudioClip attackSwing;
    public AudioClip footstep;
    public AudioClip playerHit;
    public AudioClip playerImpact;
    public AudioClip dollDamage;
    public AudioClip dollDeath;
    public AudioClip dollIdle;
    public AudioClip keyPickup;
    public AudioClip healthPickup;
    public AudioClip doorOpen;
    public AudioClip voiceId;

    void Start()
    {
        musicSource.clip = music;
        musicSource.Play();

        ambienceSource.clip = ambience;
        ambienceSource.Play();
    }

    public void PlayAudio(AudioClip audio)
    {
        sfx.pitch = 1;
        sfx.PlayOneShot(audio);
    }

    public void PlayAudioRandomPitch(AudioClip audio, float range)
    {
        sfx.pitch = 1 + Random.Range(-range, range);
        sfx.PlayOneShot(audio);
    }

    public void PlayAudioAtPitch(AudioClip audio, float pitch)
    {
        sfx.pitch = pitch;
        sfx.PlayOneShot(audio);
    }

    public void PlayAudioAtLocation(AudioClip audio, Vector3 location)
    {
        sfx.pitch = 1;
        AudioSource.PlayClipAtPoint(audio, location);
    }
}
