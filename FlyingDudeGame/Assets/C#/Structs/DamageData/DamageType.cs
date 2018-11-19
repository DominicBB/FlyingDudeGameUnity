using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct DamageType
{
    public string name;
    public Color baseColor;
    List<DamageType> strongAgainst;
    List<DamageType> weakAgainst;
}
