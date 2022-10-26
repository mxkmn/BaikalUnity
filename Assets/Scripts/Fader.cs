using UnityEngine;
using System;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "Fader";

    [SerializeField] private Animator _animator;

    private static Fader _instance;

    public static Fader instance
    {
        get 
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public bool isFading { get; private set; }

    private Action _fadeInCallBack;
    private Action _fadeOutCallBack;

    public void FadeIn(Action fadeInCallBack)
    {
        if (isFading)
            return;

        isFading = true;
        _fadeInCallBack = fadeInCallBack;
        _animator.SetBool("isFaded", true);
    }

    public void FadeOut(Action fadeOutCallBack)
    {
        if (isFading)
            return;

        isFading = true;
        _fadeOutCallBack = fadeOutCallBack;
        _animator.SetBool("isFaded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadeInCallBack?.Invoke();
        _fadeInCallBack = null;
        isFading = false;
    }
    
    private void Handle_FadeOutAnimationOver()
    {
        _fadeOutCallBack?.Invoke();
        _fadeOutCallBack = null;
        isFading = false;
        if (GameObject.FindObjectOfType<Game>() != null)
            GameObject.FindObjectOfType<Game>().StartGame();
        else if (GameObject.FindObjectOfType<UIMenu>() != null)
            GameObject.FindObjectOfType<UIMenu>().TurnTouch();
    }
}

