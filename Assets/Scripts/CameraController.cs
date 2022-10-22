using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField, Range(0.1f, 10)] private float sensetiveX = 1;
    [SerializeField, Range(0.1f, 10)] private float sensetiveY = 1;
    private bool isActivate;
    private Vector2 _rotate;


    [Header("Links")]
    private Player _player;
    private Transform _playerTransform;
    private Game _game;
    private CameraShake _cameraShake;

    private void Awake()
    {
        _cameraShake = GetComponent<CameraShake>();
        _player = GetComponentInParent<Player>();
        _playerTransform = _player.transform;
        _game = _player._game;
    }

    private void Start()
    {
        _game.OnStartGameAction += OnStartGame;
        _game.OnStopGameAction += OnStopGame;
        _game.OnContinueGameAction += OnContinueGame;
    }

    void FixedUpdate()
    {
        if (isActivate)
        {
            View();
        }
    }

    private void View()
    {
        _rotate.x = Input.GetAxis("Mouse X") * sensetiveX;
        _rotate.y = -Input.GetAxis("Mouse Y") * sensetiveY;

        if (_rotate.y != 0)
            transform.Rotate(_rotate.y, 0, 0);

        if (_rotate.x != 0)
            _playerTransform.Rotate(0, _rotate.x, 0);
    }

    private void OnStartGame()
    {
        isActivate = true;
    }

    private void OnStopGame()
    {
        isActivate = false;
    }

    private void OnContinueGame()
    {
        isActivate = true;
    }

    public void TurnShaking()
    {
        _cameraShake.ShakeCamera(0.8f, 0.5f, 10);
        //_cameraShake.ShakeRotateCamera(3,3); 
    }
}


