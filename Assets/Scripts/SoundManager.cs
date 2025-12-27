using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGMとSEの音源")]
    public AudioClip bgmClip;
    public AudioClip jumpSE;
    public AudioClip switchSE;
    public AudioClip goalSE;
    public AudioClip deadSE;

    [Header("スピーカー")]
    public AudioSource bgmSource;
    public AudioSource seSource;

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ゲーム開始時にBGMを再生
        PlayBGM();
    }

    public void PlayBGM()
    {
        // 既に同じ曲が鳴っているなら何もしない
        if (bgmSource.clip == bgmClip && bgmSource.isPlaying)
        {
            return;
        }

        // 違う曲、または止まっているなら再生
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayJump() { if (jumpSE) seSource.PlayOneShot(jumpSE); }
    public void PlaySwitch() { if (switchSE) seSource.PlayOneShot(switchSE); }
    public void PlayGoal() { if (goalSE) seSource.PlayOneShot(goalSE); }
    public void PlayDead() { if (deadSE) seSource.PlayOneShot(deadSE); }
}