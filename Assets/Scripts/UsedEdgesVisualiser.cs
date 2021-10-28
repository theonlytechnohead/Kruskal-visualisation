using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsedEdgesVisualiser : MonoBehaviour {

	private int used_edges = 0;

	private void Start() {
		gameObject.SetActive(false);
	}

	public void InitialiseUsedEdges() {
		used_edges = 0;
		gameObject.SetActive(true);
	}

	public void IncremendUsedEdges() {
		used_edges++;
		UpdateDisplay();
	}

	void UpdateDisplay() {
		GetComponent<TextMeshProUGUI>().text = $"Used edges:\t{used_edges}";
	}

}
