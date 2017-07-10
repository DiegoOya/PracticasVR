using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHTC : NetworkBehaviour {
	
    GameObject mandoIzq, mandoDer;

    // Update is called once per frame
    /*Necesitamos detectar si el jugador HTC es jugador local. Si lo es, se ejecutan los controles de HTC*/
    void Update()
    {
        mandoIzq = transform.GetChild(0).gameObject;
        mandoDer = transform.GetChild(1).gameObject;
        if (!isLocalPlayer) return;
        if (mandoIzq.GetComponent<SteamVR_LaserPointer>().update)
        {
            mandoIzq.GetComponent<SteamVR_LaserPointer>().Actualizar();
        }
        if (mandoDer.GetComponent<SteamVR_LaserPointer>().update)
        {
            mandoDer.GetComponent<SteamVR_LaserPointer>().Actualizar();
        }
    }
}
