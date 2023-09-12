using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float loadMapTime = 1f;
	[SerializeField] AudioClip succeess;
	[SerializeField] AudioClip crash;

	[SerializeField] ParticleSystem succeessParticle;
	[SerializeField] ParticleSystem crashParticle;


	AudioSource audioSource;

	bool isTransitioning = false;
	bool allowCollision = true;
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		CheatCode();
	}
	private void CheatCode()
	{
		if (Input.GetKey(KeyCode.L))
		{
			LoadNextLevel();
		}
		if (Input.GetKey(KeyCode.C))
		{
			allowCollision = !allowCollision;
		}
	}

	private void CancelCollision()
	{
		GetComponent<CollisionHandler>().enabled = false;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning || allowCollision) { return; }

		switch (collision.gameObject.tag)
		{
			case "Frienly":
				Debug.Log("Collide with friendly tag obj");
				break;
			case "Finish":
				MoveLevelSequence();
				break;
			case "Fuel":
				Debug.Log("You accquire some fuel");
				break;
			case "Untagged":
				Debug.Log("You blew up!");
				CrashSequence();
				break;
		}
	}

	private void MoveLevelSequence()
	{
		//todo Add sound effect 
		//todo Add particle effect
		isTransitioning = true;
		GetComponent<Rocket>().enabled = false;
		audioSource.Stop();
		succeessParticle.Play();
		audioSource.PlayOneShot(succeess);
		Invoke(nameof(LoadNextLevel), loadMapTime);
	}

	private void CrashSequence()
	{
		//todo Add sound effect when crash
		//todo Add particle effect when crash
		isTransitioning = true;
		GetComponent<Rocket>().enabled = false;
		audioSource.Stop();
		crashParticle.Play();
		audioSource.PlayOneShot(crash);
		Invoke("ReloadLevel", loadMapTime);
	}

	private void ReloadLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
	}

	private void LoadNextLevel()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0;
		}
		SceneManager.LoadScene(nextSceneIndex);
	}
}
