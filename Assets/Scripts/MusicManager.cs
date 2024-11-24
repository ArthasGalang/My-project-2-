using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip wisdomTheme;
    public AudioClip warTheme;
    public AudioClip wealthTheme;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(string godName)
    {
        AudioClip selectedClip = null;

        switch (godName)
        {
            case "Wisdom":
                selectedClip = wisdomTheme;
                break;
            case "War":
                selectedClip = warTheme;
                break;
            case "Wealth":
                selectedClip = wealthTheme;
                break;
        }

        if (selectedClip != null && audioSource.clip != selectedClip)
        {
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
    }
}
