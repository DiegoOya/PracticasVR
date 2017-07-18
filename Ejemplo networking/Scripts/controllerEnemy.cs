using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class controllerEnemy : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ServerCallback] //Se ejecuta  en el server y es el server el que gestiona las fisicas de la bola comunicandoselas directamente a los demas clientes
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Raton" || collision.gameObject.tag == "HTC")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up* 1000);
        }        
    }
}
