using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotEnergyController : MonoBehaviour
{
	public float energy;

	public Text energyText;

    // Start is called before the first frame update
    void Start()
    {	
     	energy = 100;  
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = "Energy: " + (int)energy; 
    }

    public void DoubleJump() {
    	energy -= 2;
    }

    public void Dash() {
    	energy -= 1;
    }

    public void Jetpack() {
    	energy -= 0.1f;
    }

}
