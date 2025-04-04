	/* Copyright (c) 2020 ExT (V.Sigalkin) */

/*
 * Copyright (c) Synekine Project - Greg Beller.
 * All rights reserved.
*/

using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using extOSC;

public class OSCSendPosition : MonoBehaviour
{

	#region Static Public Methods

	#endregion

	#region Public Vars

	[Header("OSC Settings")]
	public OSCReceiver Receiver;
	public OSCTransmitter Transmitter;

	public string oscAddressPosition = "/MetaQuest4Max/Position";
	public string oscAddressRotation = "/MetaQuest4Max/Rotation";

	[Header("Controller Settings")]
    public OVRInput.Controller controllerType = OVRInput.Controller.RTouch;
	#endregion

	#region Unity Methods

	void Update()
	{
		// Special tweak for position to directly use position in Max
		if (Transmitter == null) return;
		OSCMessage message = new OSCMessage(oscAddressPosition);
		Vector3 _controllerPosition = OVRInput.GetLocalControllerPosition(controllerType);
		message.AddValue(OSCValue.Float(_controllerPosition.x));
		message.AddValue(OSCValue.Float(_controllerPosition.y));
		message.AddValue(OSCValue.Float(- _controllerPosition.z));
		Transmitter.Send(message);

        // Special tweak for rotation to directly use rotatexyz in Max
		message = new OSCMessage(oscAddressRotation);
		Vector3 _controllerRotation = OVRInput.GetLocalControllerRotation(controllerType).eulerAngles;
		message.AddValue(OSCValue.Float(- _controllerRotation.x));
		message.AddValue(OSCValue.Float(- _controllerRotation.y));	
		message.AddValue(OSCValue.Float(_controllerRotation.z));
		Transmitter.Send(message);
	}
	#endregion
}



