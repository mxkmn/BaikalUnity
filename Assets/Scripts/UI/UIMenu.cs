using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    [Header("Button")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private GameObject _noneTouchPanel;
    [SerializeField] private Text _bestTime;

    private bool isLoading, isWaitTouch;
    private int _timeAnimation = 300;

    private void Start()
    {
        InitMenu();

        Audio.instance.RunMusicMenu();
    }

    private void OnEnable()
    {
        _playBtn.onClick.AddListener(PlayGame);
    }

    private void OnDisable()
    {
        _playBtn.onClick.RemoveListener(PlayGame);
    }

    async private void PlayGame()
    {
        if (isWaitTouch && !isLoading)
        {
            Audio.instance.ButtonUp();
            await Task.Delay(_timeAnimation);
            StartCoroutine(LoadSceneRoutine(1));
        }
    }

    public void TurnTouch()
    {
        isWaitTouch = true;
    }

    private void InitMenu()
    {
        Fader.instance.GetComponent<Canvas>().worldCamera = Camera.main;
        Fader.instance.GetComponent<Canvas>().planeDistance = 0.5f;

        if (!PlayerPrefs.HasKey("TheBestTime"))
            PlayerPrefs.SetFloat("TheBestTime", 0f);

        _bestTime.text = "<color=#E03434>The Best  Time</color>: " + System.Math.Round(PlayerPrefs.GetFloat("TheBestTime"), 2) + " s";
    }

    private IEnumerator LoadSceneRoutine(int scene)
    {
        isLoading = true;

        bool isWaitFading = true;
        Fader.instance.FadeIn(() => isWaitFading = false);
        Audio.instance.StopMusic();

        while (isWaitFading)
            yield return null;

        var async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;

        isWaitFading = true;
        Fader.instance.FadeOut(() => isWaitFading = false);


        while (isWaitFading)
            yield return null;

        
        isLoading = false;
    }
}
