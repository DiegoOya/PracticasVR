using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CompFigura : NetworkBehaviour
{

	bool toca = false;

    // Update is called once per frame
	[ServerCallback]
    void Update()
    {
        if (this.transform.parent == null && !toca) //Si la figura no tiene padre (mando o ratón) activamos el trigger
        {
			RpcFall(gameObject);
        }
	}

	[ClientRpc]
	void RpcFall(GameObject figura)
	{
		figura.GetComponent<Collider>().isTrigger = true;
		figura.GetComponent<Rigidbody>().isKinematic = false;
	}

	[ServerCallback]
    void OnTriggerEnter(Collider col) //Una vez activado el trigger hacemos que la figura se pare con el primer collider detectado
    {
        if (col.gameObject.name != "Laser") //Si choca con el laser que no se pare en el aire
		{
			RpcTriggerEnter(gameObject);
			toca = true;
		}
    }

	[ClientRpc]
	void RpcTriggerEnter(GameObject figura)
	{
		figura.GetComponent<Rigidbody>().isKinematic = true;
	}

	[ServerCallback]
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name != "Laser") 
		{
			toca = false;
		}
	}
}
