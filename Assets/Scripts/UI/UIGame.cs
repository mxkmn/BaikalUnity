using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [Header("Menu and Text")]
    [SerializeField] private Text _score;
    [SerializeField] private GameObject pausePanel, winPanel, losePanel, newScore;
    public Text scoreNow, fight;

    [Header("Button")]
    [SerializeField] private Button[] _exiteBtn;
    [SerializeField] private Button _stopBtn;
    [SerializeField] private Button _continueBtn;

    [Header("Links")]
    [SerializeField] private Game _game;

    private bool isLoading, isWaitTouch;
    private int _timeAnimation = 300;

    private void Start()
    {
        _game.OnEndGameAction += OnEndGame;

        InitGame();

        isWaitTouch = true;
        Audio.instance.RunMusicGame();
    }

    private void OnEnable()
    {
        foreach (Button btn in _exiteBtn)
            btn.onClick.AddListener(ExiteGame);

        _stopBtn.onClick.AddListener(StopGame);
        _continueBtn.onClick.AddListener(ContinueGame);
    }

    private void OnDisable()
    {
        foreach (Button btn in _exiteBtn)
            btn.onClick.RemoveListener(ExiteGame);
        _stopBtn.onClick.RemoveListener(StopGame);
        _continueBtn.onClick.RemoveListener(ContinueGame);
    }

    async public void StopGame()
    {
        if (isWaitTouch)
        {
            isWaitTouch = false;
            Audio.instance.ButtonUp();

            await Task.Delay(_timeAnimation);

            _game.StopGame();
            pausePanel.SetActive(true);
            isWaitTouch = true;
            _stopBtn.gameObject.SetActive(false);
        }
    }

    async private void ContinueGame()
    {
        if (isWaitTouch)
        {
            isWaitTouch = false;
            Audio.instance.ButtonUp();

            await Task.Delay(_timeAnimation);

            _game.ContinueGame();
            pausePanel.SetActive(false);
            isWaitTouch = true;
            _stopBtn.gameObject.SetActive(true);
        }
    }

    async private void ExiteGame()
    {
        if (isWaitTouch && !isLoading)
        {
            Audio.instance.ButtonUp();
            await Task.Delay(_timeAnimation);
            StartCoroutine(LoadSceneRoutine(0));
        }
    }

    private void OnEndGame(bool isWin)
    {
        if (isWin)
        {
            if (PlayerPrefs.GetFloat("TheBestTime") > _game._timeScore || PlayerPrefs.GetFloat("TheBestTime") == 0)
            {
                PlayerPrefs.SetFloat("TheBestTime", _game._timeScore);
                newScore.SetActive(true);
            }

            _score.text = "<color=#E03434>The Best Time</color>: " + System.Math.Round(PlayerPrefs.GetFloat("TheBestTime"), 2) + " s";
            winPanel.SetActive(true);
        }
            
        else 
            losePanel.SetActive(true);
        _stopBtn.gameObject.SetActive(false);
    }

    private void InitGame()
    {
        Fader.instance.GetComponent<Canvas>().worldCamera = Camera.main;
        Fader.instance.GetComponent<Canvas>().planeDistance = 0.5f;
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
