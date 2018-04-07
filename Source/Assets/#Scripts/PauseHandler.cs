using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseHandler : MonoBehaviour {

	public Transform canvas;
	
	private bool isPaused = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
			canvas.gameObject.SetActive(isPaused);
			if(isPaused){
				Time.timeScale = 0;
			} else
			{
				Time.timeScale = 1;
			}
			
		}
	}
}
