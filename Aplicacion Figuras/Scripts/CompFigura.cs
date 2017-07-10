using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompFigura : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == null) //Si la figura no tiene padre (mando o ratón) activamos el trigger
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }  
    }

    void OnTriggerEnter(Collider col) //Una vez activado el trigger hacemos que la figura se pare con el primer collider detectado
    {
        if (col.gameObject.name != "Laser") //Si choca con el laser que no se pare en el aire
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

}
