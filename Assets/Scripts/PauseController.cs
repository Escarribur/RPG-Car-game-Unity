using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public Transform pauseCanvas;
    public Transform menu;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
		
	}

    public void Pause()
    {
        if (pauseCanvas.gameObject.activeInHierarchy == false)
        {
            pauseCanvas.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseCanvas.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }
}
