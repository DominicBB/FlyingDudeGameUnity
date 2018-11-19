using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonTargetedSpell : Spell {
    public Vector3 Dir { get; protected set; }
    public float Damage { get; protected set; }
    public Vector3 Origen { get; protected set; }
}
