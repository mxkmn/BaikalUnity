using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [Header("Button")]
    [SerializeField] private Button _playBtn;

    [Header("Links")]
    [SerializeField] private Game _game;

    private void Start()
    {
        _game.OnStopGameAction += OnStopGame;
    }

    private void OnEnable()
    {
        _playBtn.onClick.AddListener(PlayGame);
    }

    private void OnDisable()
    {
        _playBtn.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
        _game.StartGame();
    }

    private void OnStopGame()
    {

    }
}
