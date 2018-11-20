using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct DamageTypeStruct
{
    public string name;
    public Color baseColor;
    public List<DamageType> strongAgainst;
    public List<DamageType> weakAgainst;
}
