using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControladorJugadores : NetworkBehaviour
{

	// Update is called once per frame
	void Update () {
        switch (this.gameObject.tag)
        {
            case "HTC":
                if (isLocalPlayer)
                {
                    if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * 10f);                  
                }
                break;
            case "Raton":
                if (isLocalPlayer)
                {
                    if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.back * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * Time.deltaTime * 10f);
                    if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left * Time.deltaTime * 10f);
                }
                break;
        }
	}
}
