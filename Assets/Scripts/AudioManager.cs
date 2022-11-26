using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    public AudioMixerGroup soundEffectMixer;

    private int musicIndex = 0;

    // Créér un unique AudioManager accéssible de partout
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) // si la musique est terminé
        {
            // On change de musique
            PlayNextSong();
        }
    }

    private void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // On créer un game object empty
        tempGO.transform.position = pos; // On positionne le Game Object pour qu'on entendent le son si c'est dans la zonne de la caméra seulement
        AudioSource audioSource = tempGO.AddComponent<AudioSource>(); // On ajoute la component AudioSource au Game Object
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
