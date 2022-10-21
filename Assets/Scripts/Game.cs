using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public event Action OnStartGameAction;
    public event Action OnStopGameAction;

    private List<WeaponMelee> weaponMelees;
    private List<WeaponRange> weaponRanges;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void StopGame()
    {

    }

    public void Lose()
    {
        StopGame();
    }

    public void Win()
    {
        StopGame();
    }

    private void InitializeWeapon()
    {
        weaponMelees = new List<WeaponMelee>();
        weaponMelees.Add(new WeaponMelee(0, 0, 0));
        weaponMelees.Add(new WeaponMelee(0, 0, 0));
        weaponMelees.Add(new WeaponMelee(0, 0, 0));
        weaponMelees.Add(new WeaponMelee(0, 0, 0));
        weaponMelees.Add(new WeaponMelee(0, 0, 0));

        weaponRanges = new List<WeaponRange>();
        weaponRanges.Add(new WeaponRange(0, 0, 0, 0, 0));
        weaponRanges.Add(new WeaponRange(0, 0, 0, 0, 0));
        weaponRanges.Add(new WeaponRange(0, 0, 0, 0, 0));
        weaponRanges.Add(new WeaponRange(0, 0, 0, 0, 0));
        weaponRanges.Add(new WeaponRange(0, 0, 0, 0, 0));
    }

    public List<float> GetWeaponMelee(byte index) =>  weaponMelees[index].GetParam();

    public List<float> GetWeaponRanges(byte index) => weaponRanges[index].GetParam();
}
