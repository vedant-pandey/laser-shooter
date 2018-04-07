using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public float width = 10f ;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xmax,xmin;
	
	// Use this for initialization
	void Start () {
		float dist = transform.position.z - Camera.main.transform.position.z;
		Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist));
		Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1,1,dist));
		xmin= left.x;
		xmax= right.x;
		SpawnUntilFull();
	}
	
	void SpawnEnemies(){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width,height,0f));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right*speed*Time.deltaTime;
		} else{
			transform.position += Vector3.left*speed*Time.deltaTime;
		}
		
		float rightEdge = transform.position.x + (0.5f*width);
		float leftEdge = transform.position.x - (0.5f*width);
		
		
		
		if(leftEdge < xmin){
			movingRight = true;
		}
		else if(rightEdge > xmax){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			Debug.Log("All enemies killed");
			SpawnUntilFull();
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
