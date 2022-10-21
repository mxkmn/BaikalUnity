using System.Collections;
using System.Collections.Generic;
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
    public PlayerMovement _playerMovement;
    public PlayerFighting _playerFighting;
    public Game _game;

    private void Awake()
    {

    }

    private void Start()
    {
        _game.OnStartGameAction += OnStartGame;
        _game.OnStopGameAction += OnStopGame;

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
            _game.Lose();
    }

    private void SetFightingBehaviour(FightingBehaviour fightingBehaviourNew)
    {
        if (fightingBehaviour == fightingBehaviourNew)
            return;

        fightingBehaviour = fightingBehaviourNew;
        OnFightingBehaviourChangedAction?.Invoke(fightingBehaviour);
    }

    private void SetFightingBehaviourDefault() => SetFightingBehaviour(FightingBehaviour.Idle);

    private void OnStopGame()
    {
        isActivate = false;
    }
    private void OnStartGame()
    {
        isActivate = true;
    }
}
