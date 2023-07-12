using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource soundlplayer;

    private void Start()
    {
        soundlplayer = gameObject.AddComponent<AudioSource>();
        soundlplayer.volume = PlayerPrefs.GetInt("Sound", 1);
    }

    public void PlaySound(AudioClip sound)
    {
        soundlplayer.PlayOneShot(sound);
    }

    public void Vibrate()
    {
        if (PlayerPrefs.GetInt("Vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }
    }
}
