using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public GameObject laser;
	public float health = 150f;
	public float shotSpeed = -10f;
	public float fireRate = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	public float fireVolume = 0.5f;
	public float deathVolume = 0.5f;
	
	private ScoreKeeper scoreKeeper;
	
	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		float prob = fireRate*Time.deltaTime;
		if(Random.value < prob){
			Fire();
		}
	}
	
	void Fire(){
		GameObject shot = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
		shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, shotSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position,fireVolume);
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
	}
	
	public void Die(){
		AudioSource.PlayClipAtPoint(deathSound,transform.position,deathVolume);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
