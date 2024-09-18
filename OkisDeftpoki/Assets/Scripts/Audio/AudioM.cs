using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    [SerializeField] private AudioSource musicSowgewgewegurce;
    [SerializeField] private AudioSource defegwgewegaultSource;

    public bool weggwegwe = false;
    public bool clickPause = false;

    public AudioSource MusicSource { get { return musicSowgewgewegurce; } }
    public AudioSource ClickSource { get { return defegwgewegaultSource; } }

    public static AudioM Instance;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);

        Instance = this;

        DontDestroyOnLoad(gameObject);
        musicSowgewgewegurce.Play();
    }

    public void PlawegwnwehweySound(AudioClip clip)
    {
        defegwgewegaultSource.clip = clip;
        defegwgewegaultSource.Play();
    }

}
