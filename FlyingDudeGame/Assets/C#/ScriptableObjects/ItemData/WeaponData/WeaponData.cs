using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponData/Weapon")]
public class WeaponData : ItemData {
    public float attackRange;
    public DamageBoostFlatStruct flatDamageBoost;
}
