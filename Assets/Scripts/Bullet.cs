using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    public float BlastRadius = 2f;

    [SyncVar]
    public GameObject PlayerFrom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isServer) {
            if (other.gameObject == PlayerFrom) {
                Debug.Log("Hit self!");
            }
            else {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, BlastRadius);
                foreach (Collider2D col in colliders) {
                    Debug.Log("Blasting " + col.gameObject.name);
                }
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
}
