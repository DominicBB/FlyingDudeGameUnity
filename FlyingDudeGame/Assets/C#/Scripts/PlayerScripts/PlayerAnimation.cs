using UnityEngine;
using System.Collections;

public static class PlayerAnimation
{
    private static int attackHash;
    private static int IsFlyingHash;
    private static int InputXYZHash;
    public static Player Player { get; internal set; }

    public static bool IsFlyingVar { get
        {
            return animator.GetBool(IsFlyingHash);
        }}
    public static bool AttackVar
    {
        get
        {
            return animator.GetBool(attackHash);
        }
    }
    public static float InputXYZVar
    {
        get
        {
            return animator.GetFloat(InputXYZHash);
        }
    }

    private static Animator animator;
    public static Animator Animator
    {
        get { return animator; }
        set
        {
            animator = value;
            attackHash = Animator.StringToHash("Attack");
            IsFlyingHash = Animator.StringToHash("IsFlying");
            InputXYZHash = Animator.StringToHash("InputXYZ");
            
        }
    }



    public static void Attack(bool attack)
    {
        Player.updateForwardDirection = true;
        Animator.SetBool(attackHash, attack);
    }


    public static void InputYXZ(float inputXYZ)
    {
        Animator.SetFloat(InputXYZHash, inputXYZ);
    }


    public static void IsFlying(bool isFlying)
    {
        Animator.SetBool(IsFlyingHash, isFlying);
    }


    public static void AttackEnded()
    {
        Animator.SetBool(attackHash, false);
    }

}
