using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyManager : NetworkManager {

    public GameObject canvas, bola;

    GameObject playerHtc, playerRaton;
    int playerPrefabIndex;

    public void HTCClicked()//La opción HTC se identifica con el número 1
    {
        playerPrefabIndex = 1;
        canvas.SetActive(false);
    }

    public void RatonClickado()//La opción Raton se identifica con el número 2
    {
        playerPrefabIndex = 2;
        canvas.SetActive(false);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        IntegerMessage msg = new IntegerMessage(playerPrefabIndex);
        ClientScene.AddPlayer(conn, 0, msg); //Comunicamos al server con un mensaje de red el identificador de la opción elegida
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {      
        switch (extraMessageReader.ReadMessage<IntegerMessage>().value) //Según cual sea el valor contenido en el mensaje de red se instancia una opción u otra
        {
            case 1:
                playerHtc = (GameObject)Instantiate(Resources.Load("Prefabs/PlayerHTC"));
                NetworkServer.AddPlayerForConnection(conn, playerHtc, playerControllerId);
                break;
            case 2:
                playerRaton = (GameObject)Instantiate(Resources.Load("Prefabs/PlayerRaton"));
                NetworkServer.AddPlayerForConnection(conn, playerRaton, playerControllerId);
                break;
        }
    }

    public override void OnStopServer()
    {
        canvas.SetActive(true);
    }

}
