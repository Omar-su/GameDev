using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayMachine : EvilMachine
{
    public int maxDelay = 50;
    CharacterMovement movement;

    private void Start() {
        base.Start();
        movement = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    public override void CauseHavoc() {
        movement.delay = (int)((float)powerlevel / maxDelay * maxDelay);
        powerlevel = 0;
    }

    public override void StopHavoc() {
        movement.delay = 1;
    }

    protected override string PowerInfo() {
        if (powerlevel == maxPower) return "Maximal Nedsegning\nUppnådd!!!";
        else return "Nedsegning laddad:\n" + (Mathf.Round((float)powerlevel / maxPower * 100)) + "%";
    }
}
