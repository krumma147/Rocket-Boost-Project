using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Vector3 startingPoint;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; } // Mathf.Epsilon get the lowest float which prevent the code to face error when period is 0
        //Take a look at video 57th in the course for more detail
        float cycles = Time.time / period; //Continue growing the cycle over time - how many cycle were made after some time with the given period
        const float tau = Mathf.PI * 2; //Const value of 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // from -1 to 1 
        movementFactor = (rawSinWave + 1) / 2; // refactor it to be 0 to 1
        Vector3 offset = movementVector * movementFactor; //The distance that the object will travel
        transform.position = startingPoint + offset; //Make the object travel to the point with given offset
    }
}

//Do level design in video 60
