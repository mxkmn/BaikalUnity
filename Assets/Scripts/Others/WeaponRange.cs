using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _distance;
    [SerializeField] private float _ammo;
    [SerializeField] private float _reloading;
    private List<float> parametrs;

    public WeaponRange(float damage, float cooldown, float distance, float ammo, float reloading)
    {
        this._damage = damage;
        this._cooldown = cooldown;
        this._distance = distance;
        this._ammo = ammo;
        this._reloading = reloading;
        parametrs = new List<float> { damage, cooldown, distance, ammo , reloading };

        /*parametrs.Add(damage);
        parametrs.Add (cooldown);
        parametrs.Add(distance);*/
    }

    public List<float> GetParam() => parametrs;
}
