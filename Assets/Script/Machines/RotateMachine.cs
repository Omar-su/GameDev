using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMachine : EvilMachine
{
    public int maxSpin;
    Camera camera;
    bool causingHavoc = false;

    private void Start() {
        base.Start();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update() {
        if (causingHavoc) {
            camera.transform.rotation *= Quaternion.Euler(new Vector3(0, 0, powerlevel / maxPower * 0.05f));
        }
    }

    public override void CauseHavoc() {
        causingHavoc = true;
    }

    public override void StopHavoc() {
        causingHavoc = false;
        camera.transform.rotation = new Quaternion();
        powerlevel = 0;
    }

    protected override string PowerInfo() {
        if (powerlevel == maxPower) return "Maximal SNURR\nUppnådd!!!";
        else return "Rotation laddad:\n" + (Mathf.Round((float)powerlevel / maxPower * 100)) + "%";
    }
}