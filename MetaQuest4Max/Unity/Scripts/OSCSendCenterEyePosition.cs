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

public class OSCSendCenterEyePosition : MonoBehaviour
{

	#region Static Public Methods

	#endregion

	#region Public Vars

	[Header("OSC Settings")]
	public OSCReceiver Receiver;
	public OSCTransmitter Transmitter;
	[SerializeField] private Transform AnchorPoint;

	public string oscAddressPosition = "/MQ4M/head/position";
	public string oscAddressRotation = "/MQ4M/head/quat";

	private Vector3 lastHeadPosition = Vector3.zero;
    private Vector3 lastHeadRotation = Vector3.zero;

	#endregion

	#region Unity Methods

    void Update()
    {
		if (Transmitter == null) return;

        Vector3 headPosition = AnchorPoint.position;
        Vector3 headRotation = AnchorPoint.eulerAngles;

        if (headPosition != lastHeadPosition || headRotation != lastHeadRotation)
        {
            // Special tweak for position to directly use position in Max
			OSCMessage positionMessage = new OSCMessage(oscAddressPosition);
            positionMessage.AddValue(OSCValue.Float(headPosition.x));
            positionMessage.AddValue(OSCValue.Float(headPosition.y));
            positionMessage.AddValue(OSCValue.Float(-headPosition.z));
            Transmitter.Send(positionMessage);

            // Special tweak for rotation to directly use rotatexyz in Max
            OSCMessage rotationMessage = new OSCMessage(oscAddressRotation);
            rotationMessage.AddValue(OSCValue.Float(-headRotation.x));
            rotationMessage.AddValue(OSCValue.Float(-headRotation.y));
            rotationMessage.AddValue(OSCValue.Float(headRotation.z));
            Transmitter.Send(rotationMessage);

            lastHeadPosition = headPosition;
            lastHeadRotation = headRotation;
        }
	}	
	#endregion
}


