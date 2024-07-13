using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Jump
    }

    public virtual void AnimationUpdate() { }
}