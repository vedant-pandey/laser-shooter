using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed=10.0f;
	public float paddingX = 0.55f;
	public float paddingY = 0.5f;
	public GameObject laser;
	public float laserSpeed=0f;
	public bool moveWithMouse = false;
	public bool moveWithKeyboard = true;
	public float fireRate = 0.2f;
	public float health = 250;
	public AudioClip fireSound;
	public AudioClip deathSound;
	public float deathVolume = 0.5f;
	public float fireVolume = 0.5f;
	
	float xmin , xmax , ymin , ymax;
	
	// Use this for initialization
	void Start () {
		float dist = transform.position.z - Camera.main.transform.position.z;
		Vector3 bottomleftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist));
		Vector3 toprightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,1,dist));
		xmin = bottomleftmost.x + paddingX;
		xmax = toprightmost.x - paddingX;
		ymin = bottomleftmost.y + paddingY;
		ymax = toprightmost.y - paddingY;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(moveWithMouse){
			mouseMovement();
		}
		
		if(moveWithKeyboard){
			keyboardMovement();
		}
		
		
		//Restrict player to game space
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
		transform.position = new Vector3(newX, newY, transform.position.z);
		
		
		
	}
	
	void Fire(){
		Vector3 offset = new Vector3(0,1,0);
		GameObject laserHit = Instantiate(laser, transform.position + offset, Quaternion.identity) as GameObject;
		laserHit.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,laserSpeed,0f);
		AudioSource.PlayClipAtPoint(fireSound,transform.position,fireVolume);
	}
	
	void mouseMovement(){
		//Player movement
		if((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0)){
			Vector3 temp = new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0.0f);
			this.transform.position = temp;
		}
		
		//Shooting laser
		if(Input.GetMouseButtonDown(0)){
			InvokeRepeating("Fire",0.0000000001f, fireRate);
		}
		if(Input.GetMouseButtonUp(0)){
			CancelInvoke("Fire");
		}
	}
	void keyboardMovement(){
		//Shooting laser
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire",0.0000000001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		
		//Player movement
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right*speed*Time.deltaTime;
		} else if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left*speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += Vector3.up*speed*Time.deltaTime;
		} else if(Input.GetKey(KeyCode.DownArrow)){
			transform.position += Vector3.down*speed*Time.deltaTime;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Laser shot = col.gameObject.GetComponent<Laser>();
		if(shot){
			health -= shot.GetDamage();
			shot.Hit();
			if(health <= 0){
				Die();
			}
		}
		
		if(col.gameObject.name == "Enemy1(Clone)"){
			Die();
		}
	}
	
	void Die(){
		Destroy(gameObject);
	 	LvlManager mng = GameObject.Find("LvlManager").GetComponent<LvlManager>();
		mng.LoadLevel("Win");
		AudioSource.PlayClipAtPoint(deathSound,transform.position,deathVolume);
	}
}
