using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayMachine : EvilMachine
{
    public int maxDelay = 50;
    protected override void CauseHavoc() {
        throw new System.NotImplementedException();
    }

    protected override string PowerInfo() {
        if (powerlevel == maxPower) return "Maximal Nedsegning\nUppnådd!!!";
        else return "Nedsegning laddad:\n" + (int)((float)powerlevel / maxPower * maxDelay);
    }
}
