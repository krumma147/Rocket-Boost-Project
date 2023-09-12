using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] AudioClip EngineAudio;
    //[SerializeField] AudioClip EngineAudio;
    //[SerializeField] AudioClip EngineAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

	void ProcessRotate()
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

	void ApplyRotation(float rotationPerFrame)
	{
        rb.freezeRotation = true; //Freeze rotation from contact with other stuff in world
		transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreeze rotation
    }

	void ProcessThrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
            //Debug.Log("Pressing space");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
			if (!audioSource.isPlaying && gameObject.GetComponent<Rocket>().isActiveAndEnabled)
			{
                audioSource.PlayOneShot(EngineAudio);
			}
		}else
        {
            audioSource.Stop();
        }
    }
}
