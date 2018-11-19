using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="new Armour", menuName = "ArmourData/Armour")]
public class ArmourData : ItemData
{
    List<DamageResistance> damageResistances;
    List<DamageBoostPercentage> damageBoosts;
}
