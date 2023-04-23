using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.loop = sound.loop;
            sound.audioSource = audioSource;
        }
    }

    public IEnumerator PlaySound(int sound, float fadeTime)
    {
        yield return new WaitForEndOfFrame();
        AudioSource audioSource = sounds[sound].audioSource;
        if (sounds[sound].loop) audioSource.time = Random.Range(0f, audioSource.clip.length);
        float targetVolume = sounds[sound].volume;
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    public IEnumerator StopSound(int sound, float fadeTime)
    {
        yield return new WaitForEndOfFrame();
        AudioSource audioSource = sounds[sound].audioSource;
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static AudioManager GetAudioManager()
    {
        return FindObjectOfType<AudioManager>();
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public AudioSource audioSource;
}