using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHTC : NetworkBehaviour {
    GameObject bola;

	// Update is called once per frame
	void Update () { //se controla con WASD
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * 10f);
        bola = GameObject.FindGameObjectWithTag("bola");
        if (isServer) RpcMove(bola);
    }

    //[Command]
    //void CmdMove()
    //{
        
    //}

    [ClientRpc] //Normalmente éste suele ser el servidor, por tanto si hacemos la bola hija de este objeto se lo comunica a todos los clientes
    void RpcMove(GameObject bola)
    {
        if (bola != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                bola.transform.SetParent(this.transform);
                bola.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
