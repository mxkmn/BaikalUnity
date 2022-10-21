using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private bool isActivate;
    [SerializeField] Vector3 PosCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    private float cooldownNow;

    [Header("Links")]
    private Player _player;
    private Camera _cam;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _cam = Camera.main.GetComponent<Camera>();
    }

    private void Start()
    {
        _player._game.OnStartGameAction += OnStartGame;
        _player._game.OnStopGameAction += OnStopGame;
    }

    private void Update()
    {
     if (isActivate)
        {
            if (cooldownNow < _cooldown)
                cooldownNow += Time.deltaTime;

            if (Input.GetMouseButton(0) && cooldownNow >= _cooldown)
            {
                Attack();
                cooldownNow = 0f;
            }
        }  
    }

    private void Attack()
    {
        Ray ray = _cam.ScreenPointToRay(PosCenter);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == 5)
                EnemyDamage(hit.collider.gameObject);
        }
    }

    private void EnemyDamage(GameObject gameObject )
    {
        //gameObject.GetComponent<>().;
    }

    private void OnStopGame()
    {
        isActivate = false;
    }

    private void OnStartGame()
    {
        isActivate = true;
    }
}
