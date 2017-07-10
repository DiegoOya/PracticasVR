using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

/*Esta clase gestiona las comunicaciones servidor-cliente*/
public class MyNetworkManager : NetworkManager {

    //public GameObject canvas;

    GameObject playerHtc, playerRaton;
    int playerPrefabIndex; //0 HTC y 1 Ratón

    public override void OnClientConnect(NetworkConnection conn) //Cada vez que un cliente se conecte, se lee del fichero local con qué configuración quiere aparecer en escena (HTC ó Ratón)
    {
        SaveLoad.Load();
        playerPrefabIndex = ControlOption.current.option;
        IntegerMessage msg = new IntegerMessage(playerPrefabIndex); //El cliente debe informar al server la configuración elegida
        ClientScene.AddPlayer(conn, 0, msg);
    }

    /* esta función se ejecuta en el servidor y añade una instancia u otra dependiendo
     * del mensaje enviado por cada uno de los clientes*/
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        switch (extraMessageReader.ReadMessage<IntegerMessage>().value)
        {
            case 0:
                playerHtc = (GameObject)Instantiate(Resources.Load("PlayerHTC"));
                NetworkServer.AddPlayerForConnection(conn, playerHtc, playerControllerId);
                break;
            case 1:
                playerRaton = (GameObject)Instantiate(Resources.Load("PlayerRaton"));
                NetworkServer.AddPlayerForConnection(conn, playerRaton, playerControllerId);
                break;
        }
    }

    //public override void OnStopServer()
    //{
    //    canvas.SetActive(true);
    //}
}
