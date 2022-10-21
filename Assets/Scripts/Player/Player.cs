using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ RequireComponent (typeof(PlayerFighting))]
[ RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    
    public enum MovementBehaviour { Idle, };


    [Header("Parameters")]
    [SerializeField] private byte _health = 1;
    [SerializeField] private bool isActivate;

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
    }

    private void OnStopGame()
    {
        isActivate = false;
    }
    private void OnStartGame()
    {
        isActivate = true;
    }

    private void GetDamage() => _game.Lose();
}
