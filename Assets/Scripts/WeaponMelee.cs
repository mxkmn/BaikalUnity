using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee
{
    [Header("Parameters")]
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _distance;
    private List<float> parametrs;

    public WeaponMelee(float damage, float cooldown, float distance)
    {
        this._damage = damage;
        this._cooldown = cooldown;
        this._distance = distance;
        parametrs = new List<float> { damage , cooldown , distance };

        /*parametrs.Add(damage);
        parametrs.Add (cooldown);
        parametrs.Add(distance);*/
    }

    public List <float> GetParam() => parametrs;
}
