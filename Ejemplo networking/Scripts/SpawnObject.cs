using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObject : NetworkBehaviour {

    public GameObject bola;

    public override void OnStartServer() //instancia una bola neutral cuando se inicializa el server
    {
        GameObject enemy = (GameObject)Instantiate(bola);
        NetworkServer.Spawn(enemy);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
