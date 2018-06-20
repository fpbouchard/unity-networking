using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    public float BlastRadius = 0.5f;
    public float Damage = 10f;

    [SyncVar]
    public GameObject PlayerFrom;

	// Use this for initialization
	void Start () {
		if (isServer)
        {
            StartCoroutine("WaitAndDestroy");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // bail out if self

        if (isServer) {
            if (other.gameObject == PlayerFrom) {
                Debug.Log("Hit self!");
            }
            else {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, BlastRadius);
                foreach (Collider2D col in colliders) {
                    Debug.Log("Blasting " + col.gameObject.name);
                    Player player = col.gameObject.GetComponent<Player>();

                    if (player) {
                        player.CmdHit(gameObject.transform.position, Damage);
                    }
                }
                CmdExplode();
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }

    [Command]
    public void CmdExplode()
    {

    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(5f);
        NetworkServer.Destroy(this.gameObject);
    }
}
