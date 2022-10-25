using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof (Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(0, 7)] private float _cooldown;
    [SerializeField, Range(0, 1)] private float _speed;
    [SerializeField] private Player _player;
    private Vector3 _posPlayer;
    private float _cooldownNow;
    private Vector3 _posEnemy;
    private Vector3 _direction;
    private Rigidbody _rb;

    private bool isActivate, isAttack;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _posPlayer = _player.transform.position;
        _posEnemy = transform.position;
        _direction = _posPlayer - _posEnemy;
    }

    private void FixedUpdate()
    {
        if (isActivate)
        {
            if (!isAttack)
                Move();

            else
                Attack();
        }
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.TransformDirection(_direction * Time.fixedDeltaTime * _speed));
    }

    private void Attack()
    {
        if (_cooldownNow < _cooldown)
            _cooldownNow += Time.fixedDeltaTime;
        
        if (_cooldownNow >= _cooldown)
        {
            _player.GetDamage();
            _cooldownNow = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            WaitRandomStop();
        }
    }

    async private void WaitRandomStop()
    {
        int time = (int)(Random.Range(0.1f, 0.5f) * 1000);

        await Task.Delay(time);
        
        isAttack = true;
    }

    public void Activate()
    {
        WaitActivate();
    }

    async private void WaitActivate()
    {
        int time = (int)(Random.Range(0.5f, 5f) * 1000);

        await Task.Delay(time);

        isActivate = true;
    }

    public void Disactivate()
    {
        isActivate = false;
        gameObject.SetActive(false);
    }
}
