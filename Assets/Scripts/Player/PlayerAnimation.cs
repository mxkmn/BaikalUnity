using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _animationTime;
    [SerializeField] private Player.FightingBehaviour fightingBehaviour;

    [Header("Animators")]
    [SerializeField] private Animator _animEnergy;

    [Header("Links")]
    private Player _player;

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
