using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyNetworkManager : NetworkManager {

    //public GameObject canvas;

    GameObject playerHtc, playerRaton;
    int playerPrefabIndex; //0 HTC y 1 Ratón

    public override void OnClientConnect(NetworkConnection conn)
    {
        SaveLoad.Load();
        playerPrefabIndex = ControlOption.current.option;
        IntegerMessage msg = new IntegerMessage(playerPrefabIndex);
        ClientScene.AddPlayer(conn, 0, msg);
    }

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
