using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    // Use this for initialization

    public ClockScript clock;
    private Light sunLight;

	void Start () {
        sunLight = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        sunLight.intensity = clock.getPercentageThroughDay();
	}
}
