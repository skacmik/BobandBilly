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
            DontDestroyOnLoad(gameObject); // Hudba zùstane mezi scénami
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
        PlayMusic(); // Spustí hudbu pøi naètení hry
        SceneManager.sceneLoaded += OnSceneLoaded; // Poslouchá zmìnu scény
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic(); // Pøepne hudbu pøi naètení nové scény
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
                clipToPlay = levelMusic[levelIndex - 1]; // Hledá hudbu podle indexu levelu
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
