using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] clips;
    public AudioClip[] music;
    public AudioClip levelMusic;

    public AudioSource MusicPlayer;

    private List<AudioSource> pausedClips;

    [SerializeField]
    private BoolEvent OnPausedEvent;

    private void OnEnable()
    {
        OnPausedEvent.Event += OnPause;
    }

    private void OnDisable()
    {
        OnPausedEvent.Event -= OnPause;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        initializeDict();
    }

    private void Start()
    {
        pausedClips = new List<AudioSource>();

        //TODO: Fix this so it isn't stupid
        if(clips.Length > 0)
        {
            gameObject.GetComponents<AudioSource>()[0].time = GameManager.Instance.startAtTime;
        }
    }

    public void PlayTrackWithIndex(int index)
    {
        StopTracks();
        gameObject.GetComponents<AudioSource>()[index].Play();
        gameObject.GetComponents<AudioSource>()[index].loop = true;
    }

    public void OverlayTrackWithIndex(int index)
    {
        gameObject.GetComponents<AudioSource>()[index].Play();
        gameObject.GetComponents<AudioSource>()[index].loop = true;
    }

    public void PlayTrackWithIndexOnce(int index)
    {
        StopTracks();
        gameObject.GetComponents<AudioSource>()[index].Play();
        gameObject.GetComponents<AudioSource>()[index].loop = false;
    }


    public void OverlayTrackWithIndexOnce(int index)
    {
        gameObject.GetComponents<AudioSource>()[index].Play();
        gameObject.GetComponents<AudioSource>()[index].loop = false;
    }

    void initializeDict()
    {
        foreach (AudioClip clip in clips)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 0.5f;
        }
        MusicPlayer = gameObject.AddComponent<AudioSource>();
        MusicPlayer.volume = 0.5f;
    }

    void StopTracks()
    {
        foreach (AudioSource source in gameObject.GetComponents<AudioSource>())
        {
            source.Stop();
        }
    }


    private void PauseAllAudio()
    {
        foreach(AudioSource source in gameObject.GetComponents<AudioSource>())
        {
            if(source.isPlaying)
            {
                source.Pause();
                pausedClips.Add(source);
            }
        }
    }

    private void ResumeAllAudio()
    {
        foreach(AudioSource source in pausedClips)
        {
            source.Play();
        }
        pausedClips.Clear();
    }

    private void OnPause(bool newVal)
    {
        if (newVal)
        {
            PauseAllAudio();
        }
        else
        {
            ResumeAllAudio();
        }
    }

    public void ComboBreak()
    {
        if(Random.value > 0.5f) gameObject.GetComponents<AudioSource>()[0].Play();
        else gameObject.GetComponents<AudioSource>()[1].Play();
    }

    public void Bottle()
    {
        if (Random.value > 0.5f) gameObject.GetComponents<AudioSource>()[2].Play();
        else gameObject.GetComponents<AudioSource>()[3].Play();
    }

    public void Drink()
    {
        if (Random.value > 0.5f) gameObject.GetComponents<AudioSource>()[4].Play();
        else gameObject.GetComponents<AudioSource>()[5].Play();
    }

    public void Laughter()
    {
        gameObject.GetComponents<AudioSource>()[6].Play();
    }

    public void Rank(char rank)
    {
        switch (rank)
            {
            case 's':
                gameObject.GetComponents<AudioSource>()[7].Play();
                break;
            case 'a':
                gameObject.GetComponents<AudioSource>()[8].Play();
                break;
            case 'b':
                gameObject.GetComponents<AudioSource>()[9].Play();
                break;
            case 'c':
                gameObject.GetComponents<AudioSource>()[10].Play();
                break;
            case 'd':
                gameObject.GetComponents<AudioSource>()[11].Play();
                break;
            case 'f':
                gameObject.GetComponents<AudioSource>()[12].Play();
                break;
        }
    }

    public void RecordScratch()
    {
        gameObject.GetComponents<AudioSource>()[13].Play();
    }

    public void SloMo()
    {
        if (Random.value > 0.33f) gameObject.GetComponents<AudioSource>()[14].Play();
        else if (Random.value > 0.67f) gameObject.GetComponents<AudioSource>()[15].Play();
        else gameObject.GetComponents<AudioSource>()[16].Play();
    }

    public void SplashScreen()
    {
        gameObject.GetComponents<AudioSource>()[17].Play();
    }

    public void SetLevelMusic(int musicIndex)
    {
        levelMusic = music[musicIndex];
        MusicPlayer.clip = levelMusic;
    }


    public void SetLevelMusic(AudioClip levelMusicClip)
    {
        levelMusic = levelMusicClip;
        MusicPlayer.clip = levelMusic;
    }

    public void StartLevelMusic()
    {
        MusicPlayer.Play();
    }

    public void StartLevelMusicWithDelay(float delay)
    {
        MusicPlayer.PlayDelayed(delay);
    }
}
