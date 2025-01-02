using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ICommand
{
    private Animator anim;
    private float cooldown;
    private System.Action<float> updateCooldown;

    public AttackCommand(Animator anim, float cooldown, System.Action<float> updateCooldown)
    {
        this.anim = anim;
        this.cooldown = cooldown;
        this.updateCooldown = updateCooldown;
    }

    public void Execute()
    {
        anim.SetTrigger("attack");
        updateCooldown(cooldown);
    }
}
