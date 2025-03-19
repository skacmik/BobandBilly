using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip menuMusic; // Hudba pro menu
    public AudioClip[] levelMusic; // Pole hudeb pro levely

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hudba z�stane mezi sc�nami
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic(); // Spust� hudbu p�i na�ten� hry
        SceneManager.sceneLoaded += OnSceneLoaded; // Poslouch� zm�nu sc�ny
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic(); // P�epne hudbu p�i na�ten� nov� sc�ny
    }

    private void PlayMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        AudioClip clipToPlay = null;

        if (sceneName == "Menu" + "Levels")
        {
            clipToPlay = menuMusic;
        }
        else
        {
            int levelIndex;
            if (int.TryParse(sceneName.Replace("Level", ""), out levelIndex) && levelIndex > 0 && levelIndex <= levelMusic.Length)
            {
                clipToPlay = levelMusic[levelIndex - 1]; // Hled� hudbu podle indexu levelu
            }
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
