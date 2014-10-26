using UnityEngine;
using System.Collections;

public enum MusicType
{
    Theme,
    Battle
}

public class MusicManagerScript : Singleton<MusicManagerScript> {

    public float crossFadeDuration;

    public AudioSource themeMusic;
    public AudioSource battleMusic;

    private AudioSource currentSource;

    void Start()
    {
        themeMusic.Play();
        currentSource = themeMusic;
    }

    public void ChangeMusicType(MusicType newType)
    {
        AudioSource newSource = getAudioSource(newType);
        StartCoroutine(crossfade(currentSource, newSource));
        currentSource = newSource;
    }

    private IEnumerator crossfade(AudioSource from, AudioSource to)
    {
        float t = 0.0f;
        to.Play();
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / crossFadeDuration);
            to.volume = Mathf.Lerp(0, 1, t);
            from.volume = Mathf.Lerp(1, 0, t);
            yield return 0;
        }
        from.Stop();
    }

    private AudioSource getAudioSource(MusicType type)
    {
        switch (type)
        {
            case MusicType.Theme:
                return themeMusic;
            case MusicType.Battle:
                return battleMusic;
        }
        return themeMusic;
    }
}
