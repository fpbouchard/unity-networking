using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
        if (isServer) {
            GameObject playerGameObject = Instantiate(PlayerPrefab, gameObject.transform.position, Quaternion.identity);
            NetworkServer.SpawnWithClientAuthority(playerGameObject, connectionToClient);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
