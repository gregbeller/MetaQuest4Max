/*
 * Copyright (c) Synekine Project - Greg Beller.
 * All rights reserved.
*/

using UnityEngine;
using extOSC;

public class OSCSendControllerButton : MonoBehaviour
{

	#region Static Public Methods

	#endregion

	#region Public Vars

	[Header("OSC Settings")]
	public OSCReceiver Receiver;
	public OSCTransmitter Transmitter;
	public string baseOscAddress = "/MQ4M/";

	    [Header("Controller Settings")]
    public OVRInput.Controller controllerType = OVRInput.Controller.RTouch;

    void Start()
    {
    }

    void Update()
    {
        SendControllerData();
    }

    void SendControllerData()
    {
        if (Transmitter == null)
            return;

        // Boutons

		SendOnChange("/button/one", OVRInput.Button.One, controllerType);
        SendOnChange("/button/two", OVRInput.Button.Two, controllerType);
        SendOnChange("/button/index_trigger", OVRInput.Button.PrimaryIndexTrigger, controllerType);
        SendOnChange("/button/hand_trigger", OVRInput.Button.PrimaryHandTrigger, controllerType);

        // Thumbstick
		SendOnChange("/thumbstick/click", OVRInput.Button.PrimaryThumbstick, controllerType);
        Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerType);
        SendOSCMessage("/thumbstick/x", thumbstick.x);
        SendOSCMessage("/thumbstick/y", thumbstick.y);

        // Near Touch
        SendOnChange("/neartouch/index_trigger", OVRInput.NearTouch.PrimaryIndexTrigger, controllerType);
        SendOnChange("/neartouch/thumb_buttons", OVRInput.NearTouch.PrimaryThumbButtons, controllerType);
    }

    void SendOnChange(string address, OVRInput.Button button, OVRInput.Controller controllerType){
		if 	  (OVRInput.GetDown(button, controllerType)) SendOSCMessage(address, true);
		else if (OVRInput.GetUp(button, controllerType)) SendOSCMessage(address, false);
	}

	void SendOnChange(string address, OVRInput.NearTouch button, OVRInput.Controller controllerType){
		if 	  (OVRInput.GetDown(button, controllerType)) SendOSCMessage(address, true);
		else if (OVRInput.GetUp(button, controllerType)) SendOSCMessage(address, false);
	}
    
    void SendOSCMessage(string subAddress, bool value)
    {
        OSCMessage message = new OSCMessage(baseOscAddress + subAddress);
        message.AddValue(OSCValue.Int(value ? 1 : 0));
        Transmitter.Send(message);
    }

    void SendOSCMessage(string subAddress, float value)
    {
        OSCMessage message = new OSCMessage(baseOscAddress + subAddress);
        message.AddValue(OSCValue.Float(value));
        Transmitter.Send(message);
    }
	#endregion
}
