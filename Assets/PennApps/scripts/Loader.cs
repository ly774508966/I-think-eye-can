﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class Loader : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour;
	private string current_model = "skull";
	private Transform myModelTrf;
	private bool update = false;

	public Transform chess;
	private Vector3 chess_size = new Vector3 (4f, 4f, 4f);
	private Quaternion chess_rotate = Quaternion.identity;
	private Vector3 chess_position = new Vector3(0f, 0f, 0f);

	public Transform skull;
	private Vector3 skull_size = new Vector3 (0.005f, 0.005f, 0.005f);
	private Quaternion skull_rotate = Quaternion.Euler(new Vector3(-90.0f, 0f, 0f));
	private Vector3 skull_pos = new Vector3 (0.08f, 4.51f, 0.59f);

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	void Update () {
	
		if (Input.anyKeyDown) {
			update = true;
			print ("AWDAWDAWD");
			if (current_model.Equals ("chess")) {
				current_model = "skull";
			} else {
				current_model = "chess";
			}
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus) { 
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
			update)
		{
			OnTrackingFound();
		}
	} 

	private void OnTrackingFound() {
		
		if (current_model != null) {
			if (myModelTrf != null) {
				Destroy (myModelTrf.gameObject);
			}

			switch (current_model) {
			case "chess":
				myModelTrf = GameObject.Instantiate (chess) as Transform;
				myModelTrf.parent = mTrackableBehaviour.transform;
				myModelTrf.localPosition = chess_position;
				myModelTrf.localRotation = chess_rotate;
				myModelTrf.localScale = chess_size;
				break;

			case "skull":
				myModelTrf = GameObject.Instantiate (skull) as Transform;
				myModelTrf.parent = mTrackableBehaviour.transform;
				myModelTrf.localPosition = skull_pos;
				myModelTrf.localRotation = skull_rotate;
				myModelTrf.localScale = skull_size;
				break;
			}

			if (myModelTrf != null) {
				myModelTrf.gameObject.active = true;
			}
		}
	}
}