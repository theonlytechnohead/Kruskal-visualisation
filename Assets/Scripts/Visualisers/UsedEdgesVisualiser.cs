using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsedEdgesVisualiser : MonoBehaviour {

	private int used_edges = 0;

	private void Start() {
		transform.parent.gameObject.SetActive(false);
	}

	public void InitialiseUsedEdges() {
		used_edges = 0;
		transform.parent.gameObject.SetActive(true);
		transform.GetComponentInParent<Animation>().Play();
	}

	public void IncremendUsedEdges() {
		used_edges++;
		UpdateDisplay();
	}

	void UpdateDisplay() {
		GetComponent<TextMeshProUGUI>().text = $"Used edges:\t{used_edges}";
	}

}
