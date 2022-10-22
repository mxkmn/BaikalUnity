using Unity.VisualScripting;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown;
    [SerializeField] private bool isActivate;
    
    private float cooldownNow;
    private Vector3 PosCenter;
    private Player.FightingBehaviour fightingBehaviour;

    [Header("Links")]
    [SerializeField] private Animator _animEnergy;
    private Player _player;
    private Camera _cam;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _cam = Camera.main.GetComponent<Camera>();
    }

    private void Start()
    {
        PosCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        cooldownNow = _cooldown;

        _player._game.OnStopGameAction += OnStopGame;
        _player._game.OnContinueGameAction += OnContinueGame;
        _player.OnFightingBehaviourChangedAction += OnFightingBehaviourChanged;
    }

    private void Update()
    {
     if (isActivate)
        {
            if (cooldownNow < _cooldown)
                cooldownNow += Time.deltaTime;

            if (fightingBehaviour == Player.FightingBehaviour.Idle)
            {
                if (Input.GetMouseButton(0) && cooldownNow >= _cooldown)
                {
                    Attack();
                    _animEnergy.SetTrigger("Shoot");
                    cooldownNow = 0f;
                }
            }
        }  
    }

    private void Attack()
    {
        
        Ray ray = _cam.ScreenPointToRay(PosCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 50))
        {
            Debug.Log("Ïèó â " + hit.collider.gameObject);
            if (hit.collider.gameObject.layer == 6)
                EnemyDamage(hit.collider.gameObject.GetComponent<Enemy>());
        }
    }

    private void EnemyDamage(Enemy enemy)
    {
        enemy.Disactivate();
        _player._game.MinusEnemy();
    }

    private void OnStopGame()
    {
        isActivate = false;
    }

    public void OnStartGame()
    {
        isActivate = true;
    }

    private void OnContinueGame()
    {
        isActivate = true;
    }

    private void OnFightingBehaviourChanged(Player.FightingBehaviour fightingBehaviourNew) => fightingBehaviour = fightingBehaviourNew;
}
