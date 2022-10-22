using UnityEngine;

public class Audio : MonoBehaviour
{
    private const string FADER_PATH = "Sounds";

    [Header("Sound Source")]
    [SerializeField] private AudioSource buttonUp;
    [SerializeField] private AudioSource win, lose, damage, death, fight;

    [Header("Music Source")]
    [SerializeField] private AudioSource musicMenu;
    [SerializeField] private AudioSource musicGame;

    private static Audio _instance;
    public static Audio instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Audio>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public void RunMusicMenu()
    {
        if (!musicMenu.isPlaying)
            musicMenu.Play();
    }

    public void RunMusicGame()
    {
        if (!musicGame.isPlaying)
            musicGame.Play();
    }

    public void StopMusic()
    {
        if (musicMenu.isPlaying)
            musicMenu.Stop();

        if (musicGame.isPlaying)
            musicGame.Stop();
    }

    public void ButtonUp()
    {
        if (buttonUp.isPlaying)
            return;
        buttonUp.Play();
    }

    public void Win()
    {
        if (win.isPlaying)
            return;
        win.Play();
    }

    public void Lose()
    {
        if (lose.isPlaying)
            return;
        lose.Play();
    }

    public void Damage()
    {
        damage.Play();
    }

    public void Fight()
    {
        if (fight.isPlaying)
            return;
        fight.Play();
    }

    public void Death()
    {
        if (death.isPlaying)
            return;
        death.Play();
    }
}
