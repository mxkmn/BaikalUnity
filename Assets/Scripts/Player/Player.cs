using UnityEngine;
using System;

[RequireComponent(typeof(PlayerFighting))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public enum FightingBehaviour { Idle, Defend };

    public event Action<FightingBehaviour> OnFightingBehaviourChangedAction;

    [Header("Parameters")]
    [SerializeField] private bool isActivate;
    [SerializeField] private FightingBehaviour fightingBehaviour;

    [Header("Links")]
    public PlayerAnimation _playerAnimation;
    public PlayerFighting _playerFighting;
    public Game _game;

    private void Awake()
    {

    }

    private void Start()
    {
        _game.OnStartGameAction += OnStartGame;
        _game.OnStopGameAction += OnStopGame;
        _game.OnContinueGameAction += OnContinueGame;

        SetFightingBehaviourDefault();
    }

    private void Update()
    {
        if (isActivate)
        {
            if (fightingBehaviour == FightingBehaviour.Idle && Input.GetMouseButton(1))
            {
                SetFightingBehaviour(FightingBehaviour.Defend);
            }

            else if (fightingBehaviour == FightingBehaviour.Defend && !Input.GetMouseButton(1))
            {
                SetFightingBehaviour(FightingBehaviour.Idle);
            }
        }
    }

    public void GetDamage()
    {
        if (fightingBehaviour == FightingBehaviour.Idle)
        {
            Audio.instance.Death();
            _game.Lose();
        }
    }

    private void OnStopGame()
    {
        isActivate = false;
    }

    private void OnStartGame()
    {
        isActivate = true;
    }

    private void OnContinueGame()
    {
        isActivate = true;
    }

    private void SetFightingBehaviour(FightingBehaviour fightingBehaviourNew)
    {
        if (fightingBehaviour == fightingBehaviourNew)
            return;

        fightingBehaviour = fightingBehaviourNew;
        OnFightingBehaviourChangedAction?.Invoke(fightingBehaviour);
    }

    private void SetFightingBehaviourDefault() => SetFightingBehaviour(FightingBehaviour.Idle);
}
