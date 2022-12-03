using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMachine : MonoBehaviour
{
    int powerlevel = 0;
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
        powerlevel++;
        Debug.Log("EvilMachine: Pumped to " + powerlevel);
    }
    
}
