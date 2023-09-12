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

    [SerializeField] ParticleSystem boosterParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

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
			RotatingLeft();
		}
		else if (Input.GetKey(KeyCode.D))
		{
			RotatingRight();
		}
		else
		{
			StopRotating();
		}
	}

	private void StopRotating()
	{
		rightThrustParticle.Stop();
		leftThrustParticle.Stop();
	}

	private void RotatingRight()
	{
		ApplyRotation(-rotateSpeed);
		if (!rightThrustParticle.isPlaying)
		{
			rightThrustParticle.Play();
		}
	}

	private void RotatingLeft()
	{
		ApplyRotation(rotateSpeed);
		if (!leftThrustParticle.isPlaying)
		{
			leftThrustParticle.Play();
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
			Thrusting();
		}
		else
		{
			StopThrusting();
		}
	}

	private void StopThrusting()
	{
		audioSource.Stop();
		boosterParticle.Stop();
	}

	private void Thrusting()
	{
		rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!boosterParticle.isPlaying)
		{
			boosterParticle.Play();
		}
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(EngineAudio);
		}
	}
}
