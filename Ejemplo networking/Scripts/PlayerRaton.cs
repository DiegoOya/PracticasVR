using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRaton : NetworkBehaviour {

    GameObject bola, instancia;
    Color color;
    Vector3 mover;
    private NetworkIdentity objNetId;

    void Update() { //se contrla con las flechas de direccion. Normalmente este objeto suele ser el cliente
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * Time.deltaTime * 10f);
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left * Time.deltaTime * 10f);
        bola = GameObject.FindGameObjectWithTag("bola");
        if (bola != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(bola.transform.parent == null) CmdMove(this.gameObject,bola);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                if (bola.transform.parent != null)
                {
                    CmdNotParent(this.gameObject, bola);
                }
            }
        }
        if(bola != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                color = new Color(Random.value, Random.value, Random.value);
                CmdChangeColor(bola, color);
            }
        }
        //instancia = (GameObject)Instantiate(Resources.Load("Bola"));
        if (Input.GetKeyDown(KeyCode.I))
        {
            CmdInstanciar();
        }
    }

    [Command]
    void CmdInstanciar() //se invoca en el server
    {
        instancia = (GameObject)Instantiate(Resources.Load("Prefabs/Bola"));
        NetworkServer.Spawn(instancia);
    }

    [Command] //se invoca en el server
    void CmdNotParent(GameObject padre, GameObject bola)
    {
        RpcNotParent(padre, bola);
    }

    [ClientRpc] //se invoca en el server y comunica a los clientes que la bola ya no tiene este objeto como padre
    void RpcNotParent(GameObject padre, GameObject bola)
    {
        bola.transform.SetParent(null);
        bola.GetComponent<Rigidbody>().isKinematic = false;
    }

    [Command]
    void CmdMove(GameObject padre, GameObject bola) //se invoca en el server y comunica al propio que este objeto es padre de la bola
    {
        objNetId = bola.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        RpcMove(padre,bola); //se invoca en el server y comunica a los clientes que este objeto es padre de la bola
        objNetId.RemoveClientAuthority(connectionToClient);
    }

    [ClientRpc]
    void RpcMove(GameObject padre, GameObject bola)
    {
        bola.transform.SetParent(padre.transform);
        bola.GetComponent<Rigidbody>().isKinematic = true;
    }

    [Command] //se comunica al server que la bola cambió de color
    void CmdChangeColor(GameObject bola, Color color)
    {
        RpcChangeColor(bola, color); //el server replica a todos los clientes el cambio en el color
    }

    [ClientRpc]
    void RpcChangeColor(GameObject bola, Color color)
    {
        bola.GetComponent<Renderer>().material.color = color;
    }
}