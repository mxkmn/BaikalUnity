using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _animationTime;
    [SerializeField] private Player.FightingBehaviour fightingBehaviour;

    [Header("Animators")]
    [SerializeField] private Animator _hitSword;
    [SerializeField] private Animator _defend;

    [Header("Links")]
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.OnFightingBehaviourChangedAction += OnFightingBehaviourChanged;
        _player._playerFighting.OnHitSwordAction += OnHitSword;
    }

    private void OnFightingBehaviourChanged(Player.FightingBehaviour fightingBehaviourNew) 
    {
        fightingBehaviour = fightingBehaviourNew;

        if (fightingBehaviour == Player.FightingBehaviour.Idle)
        {
            _defend.SetBool("isDefend", false);
        }

        if (fightingBehaviour == Player.FightingBehaviour.Defend)
        {
            _defend.SetBool("isDefend", true);
            Debug.Log("fightingBehaviour: " + fightingBehaviour);
        }
    }

    private void OnHitSword()
    {
        int choice = Random.Range(0, 3);
        switch (choice) 
        {
            case 0:
                _hitSword.SetTrigger("Attack1");
                break;
            case 1:
            case 2:
                _hitSword.SetTrigger("Attack2");
                break;
        };
    }
}
