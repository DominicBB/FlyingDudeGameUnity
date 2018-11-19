using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Weapon/MeleeWeapon")]
public class MeleeWeapon : WeaponData
{
    [Range(0,2)]
    public new float attackRange;
}
