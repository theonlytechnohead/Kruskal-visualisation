using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EdgeVisualiser : MonoBehaviour {
	void Start() {

	}

	void Update() {

	}

	public void InitialiseEdge(Edge edge) {
		transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Source:\t{edge.source}";
		transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance:\t{edge.priority}";
		transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Destination:\t{edge.destination}";
	}
}
