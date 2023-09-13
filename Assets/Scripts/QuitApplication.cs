using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuitApplicationHandler();
    }

	private void QuitApplicationHandler()
	{
		if (Input.GetKey(KeyCode.Escape)){
            Debug.Log("Quitting the application");
            Application.Quit();
		}
	}
}
