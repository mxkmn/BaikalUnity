using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _animationTime;
    [SerializeField] private Player.FightingBehaviour fightingBehaviour;

    [Header("Links")]
    private Player _player;

    [Header("Animators")]
    [SerializeField] private Animator _anim;


    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.OnFightingBehaviourChangedAction += OnFightingBehaviourChanged;
    }

    private void OnFightingBehaviourChanged(Player.FightingBehaviour fightingBehaviourNew) 
    {
        fightingBehaviour = fightingBehaviourNew;

        if (fightingBehaviour == Player.FightingBehaviour.Idle)
        {

        }

        if (fightingBehaviour == Player.FightingBehaviour.Defend)
        {

        }
    }
}
