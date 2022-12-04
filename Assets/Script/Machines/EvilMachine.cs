using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilMachine : MonoBehaviour
{
    public GameObject powertext;
    private GameObject oldIncrease = null;
    public string animationName;
    public int powerlevel = 0;
    public int maxPower;
    // Start is called before the first frame update
    protected void Start()
    {
        maxPower *= 60;
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
        if (oldIncrease != null) Destroy(oldIncrease);
        oldIncrease = Instantiate(powertext, transform);
        oldIncrease.GetComponent<TextMesh>().text = PowerInfo();
    }

    protected abstract string PowerInfo();
    public abstract void CauseHavoc();
    public abstract void StopHavoc();

}
