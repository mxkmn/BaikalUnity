using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class Game : MonoBehaviour
{
    public event Action OnStartGameAction;
    public event Action OnStopGameAction;
    public event Action OnContinueGameAction;
    public event Action<bool> OnEndGameAction;

    [Header("Settings")]
    [SerializeField, Range(1, 20)] private int _delayBeforeAttack  = 3;
    [SerializeField, Range(1, 20)] private int _delayDream  = 1;
    [SerializeField] private UIGame _UIGame;
    [SerializeField] private Player _player;
    private byte _amountEnemy;
    private Text _scoreNow; 
    private Animator _fight; 

    [Header("Enemy")]
    public GameObject _enemies;

    [Header("Guns")]
    [SerializeField] private MeshRenderer _sword;
    [SerializeField] private MeshRenderer _defend;

    private Enemy[] _enemy;
    private SkinnedMeshRenderer[] _enemyMR;
    private bool isPlay;
    private CameraController _camera;

    [HideInInspector] public float _timeScore = 0f;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
        _scoreNow = _UIGame.scoreNow;
        _fight = _UIGame.fight.GetComponent<Animator>();
        _enemy = _enemies.GetComponentsInChildren<Enemy>();
        _enemyMR = _enemies.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        Cursor.visible = false;
        _amountEnemy = (byte)_enemy.Length;
        Story();
    }

    private void Update()
    {
        if (isPlay)
        {
            _timeScore += Time.deltaTime;
            _scoreNow.text = "<color=#E03434>Time</color>: " + MathF.Round(_timeScore, 2) + " s";

            if (Input.GetKey(KeyCode.Escape))
            {
                _UIGame.StopGame();
            }
        }
    }

    public void MinusEnemy()
    {
        _amountEnemy--;
        Audio.instance.Damage();
        if (_amountEnemy == 0)
            Win();
    }

    public void StopGame()
    {
        isPlay = false;
        OnStopGameAction?.Invoke();
        Cursor.visible = true;
    }

    public void StartGame()
    {
        OnStartGameAction?.Invoke();
    }

    public void ContinueGame()
    {
        isPlay = true;
        OnContinueGameAction?.Invoke();
        Cursor.visible = false;
    }

    public void Lose()
    {
        StopGame();
        OnEndGameAction?.Invoke(false);
        Audio.instance.Lose();
    }

    public void Win()
    {
        StopGame();
        OnEndGameAction?.Invoke(true);
        Audio.instance.Win();
    }

    async private void Story()
    {
        await Task.Delay(_delayBeforeAttack * 1000);

        _camera.TurnShaking();
        bool isWaitFading = true;

        Fader.instance.FadeIn(() => isWaitFading = false);
        while (isWaitFading)
            await Task.Yield();

        ShowEnemy();
        await Task.Delay(_delayDream * 1000);

        isWaitFading = true;
        Fader.instance.FadeOut(() => isWaitFading = false);

        while (isWaitFading)
            await Task.Yield();

        _fight.SetTrigger("Fight");
        Audio.instance.Fight();
        ActivateEnemy();
        isPlay = true;
        _player._playerFighting.OnStartGame();
    }

    private void ShowEnemy()
    {
        _sword.enabled = true;
        _defend.enabled = true;
        foreach (SkinnedMeshRenderer enemyMR in _enemyMR)
            enemyMR.enabled = true;
    }

    private void ActivateEnemy()
    {
        foreach (Enemy enemy in _enemy)
            enemy.Activate();
    }
}
