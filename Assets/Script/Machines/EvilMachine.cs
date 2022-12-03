using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilMachine : MonoBehaviour
{
    public GameObject powertext;
    public int powerlevel = 0;
    public int maxPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pump() {
        //TODO Display pump level / Info
        if (powerlevel < maxPower) powerlevel++;
        ShowPower();
    }

    void ShowPower() {
        Instantiate(powertext, transform).GetComponent<TextMesh>().text = PowerInfo();
    }

    protected abstract string PowerInfo();
    public abstract void CauseHavoc();
    public abstract void StopHavoc();

}
