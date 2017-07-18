using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Esta clase se encarga de manejar el NetworkBehaviour del PlayerPrefab de las gafas HTC
/// </summary>
public class PlayerHTC : NetworkBehaviour {
	
    GameObject mandoIzq, mandoDer, figura, father;

    private void Start()
    {
        // Desactiva todas las camaras que no sean del jugador local
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            return;
        }
    }

    // Update is called once per frame
    /*Necesitamos detectar si el jugador HTC es jugador local. Si lo es, se ejecutan los controles de HTC*/
    void Update()
    {
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            return;
        }
        mandoIzq = transform.GetChild(0).gameObject;
        mandoDer = transform.GetChild(1).gameObject;
        if (mandoIzq.GetComponent<SteamVR_LaserPointer>().update)
        {
            mandoIzq.GetComponent<SteamVR_LaserPointer>().Actualizar();
        }
        if (mandoDer.GetComponent<SteamVR_LaserPointer>().update)
        {
            mandoDer.GetComponent<SteamVR_LaserPointer>().Actualizar();
        }
    }

    // Se encarga de spawnear a las figuras en el entorno para interactuar con ellas
    [Command]
    public void CmdSpawn(string fig, string padre)
    {
        figura = (GameObject)Instantiate(Resources.Load(fig));
        NetworkServer.Spawn(figura);
        figura.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        RpcParent(figura,padre);
        figura.GetComponent<NetworkIdentity>().RemoveClientAuthority(connectionToClient);
    }

    // Se encarga de hacer al objeto hijo del mando que corresponda
    [ClientRpc]
    public void RpcParent(GameObject figura, string padre)
    {
        figura.transform.SetParent(GameObject.FindGameObjectWithTag(padre).transform);
        figura.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(figura.transform.parent.gameObject);
    }
}
