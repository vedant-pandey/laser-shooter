using UnityEngine;
using System.Collections;

public class LvlManager : MonoBehaviour {

	public void LoadLevel(string name){
		print("Moving to "+name);
		//Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	public void QuitRequest(){
		print("Quit");
		Application.Quit();
	}
	
	public void LoadNextLevel(){
		//Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void BrickDestroyed(){
		//if(Brick.breakableCount <= 0){
		//	LoadNextLevel();
		//}
	}
}
