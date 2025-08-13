using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameManager gameManager;
    public Database database;
    [Header("오디오 소스")]
    public AudioSource bgmSource;
    public AudioSource[] sfxSource;
    [Header("오디오 클립들")]
    public AudioClip[] bgmClip;
    public int sfxIndex = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (GameManager.Instance.currentState == GameState.Night)
        {
            if (bgmSource.clip != bgmClip[1])
            {
                bgmSource.clip = bgmClip[1];
                bgmSource.Play();
            }
        }
        else
        {
            if (bgmSource.clip != bgmClip[0])
            {
                bgmSource.clip = bgmClip[0];
                bgmSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource[sfxIndex].clip = clip;
        sfxSource[sfxIndex].Play();
        sfxIndex++;
        if (sfxIndex >= sfxSource.Length) 
            sfxIndex = 0;
    }

    public void PlayAttackSFX()
    {
        PlaySFX(database.currentCharInfo.attackSound);
    }
    public void PlaySkillSFX()
    {
        PlaySFX(database.currentSkillInfo.sound);
    }
}
