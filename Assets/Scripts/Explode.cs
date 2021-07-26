using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	public GameObject explosion;
	public ParticleSystem[] effects;
	private GameObject ballSprite;

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag=="Hat"){
			ballSprite = (GameObject) Instantiate(explosion, transform.position, transform.rotation);
			// // foreach(var effect in effects){
			// // 	effect.transform.parent = null;
			// // 	effect.Stop ();
			// // 	Destroy(effect.gameObject, 1.0f);
			// // }
			Destroy(gameObject);
			Destroy(this.ballSprite, 2);
		}
	}
}
