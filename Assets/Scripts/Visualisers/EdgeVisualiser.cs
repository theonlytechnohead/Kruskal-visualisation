using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EdgeVisualiser : MonoBehaviour {

	Edge edge;

	void Start() {

	}

	void Update() {

	}

	public void InitialiseEdge(Edge edge, bool coloured) {
		this.edge = edge;
		if (coloured) {
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"<color=#00FFFF>Source:\t{edge.source}</color>";
			transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<color=#0000FF>Distance:\t{edge.priority}</color>";
			transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"<color=#00FF00>Destination:\t{edge.destination}</color>";
		} else {
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Source:\t{edge.source}";
			transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance:\t{edge.priority}";
			transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Destination:\t{edge.destination}";
		}
	}

	public Edge GetEdge() {
		return edge;
	}
}
