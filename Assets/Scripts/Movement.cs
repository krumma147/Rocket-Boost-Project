using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb; 
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotateSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

	private void ProcessRotate()
	{
        if (Input.GetKey(KeyCode.A))
		{
			ApplyRotation(rotateSpeed);
		}
		else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }
    }

	private void ApplyRotation(float rotationPerFrame)
	{
        rb.freezeRotation = true; //Freeze rotation from contact with other stuff in world
		transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreeze rotation
    }

	void ProcessThrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
            Debug.Log("Pressing space");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		}
    }
}
