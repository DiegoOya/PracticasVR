using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupController : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    FixedJoint fixedJoint;
    GameObject esfera, cubo, capsula, enMano;

    private void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Figura")
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.transform.SetParent(null);
                }
            }
        }
    }
}

    
