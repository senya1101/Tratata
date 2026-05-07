using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // При старте сцены берем громкость из глобальных настроек
        audioSource.volume = GameSettings.MusicVolume; 
        
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }

    public void UpdateVolume()
    {
        if (audioSource != null)
        {
            audioSource.volume = GameSettings.MusicVolume;
        }
    }
}