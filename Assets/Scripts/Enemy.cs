using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isActivate;

    private void Update()
    {
        if (isActivate)
        {

        }
    }

    public void Activate()
    {
        isActivate = true;
    }

    public void Disactivate()
    {
        isActivate = false;
        gameObject.SetActive(false);
    }
}
