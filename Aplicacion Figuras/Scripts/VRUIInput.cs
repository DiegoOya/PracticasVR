using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*Gestiona los eventos del laser del mando de las HTC para que
 * detecte elementos con collider y en caso de que sean elementos de 
 * la UI como botones, gestionar los eventos para ejecutar las funciones
 * designadas por cada uno*/
[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private SteamVR_TrackedController trackedController;

    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
        laserPointer.PointerOut += HandlePointerOut;

        trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = GetComponentInParent<SteamVR_TrackedController>();
        }
        trackedController.TriggerClicked -= HandleTriggerClicked;
        trackedController.TriggerClicked += HandleTriggerClicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {            
            if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>()!=null)
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            else if (EventSystem.current.currentSelectedGameObject.GetComponent<Slider>() != null)
            {
                Debug.Log("Entra");
                //EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value = SteamVR_Controller.Input((int)this.gameObject.GetComponent<SteamVR_TrackedObject>().index).;
                //EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value = Mathf.Abs(SteamVR_Controller.Input((int)this.gameObject.GetComponent<SteamVR_TrackedObject>().index).velocity.x);
            }                
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            //Debug.Log("HandlePointerIn", e.target.gameObject);
        }

        var slider = e.target.GetComponent<Slider>();
        if (slider != null)
        {
            slider.Select();
            Debug.Log(slider.value);
            //slider.onValueChanged.AddListener(delegate { ValueChangeCheck(slider); });
            //Debug.Log("HandlePointerIn", e.target.gameObject);
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            //Debug.Log("HandlePointerOut", e.target.gameObject);
        }

        var slider = e.target.GetComponent<Slider>();
        if (slider != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            //Debug.Log("HandlePointerIn", e.target.gameObject);
        }
    }
}