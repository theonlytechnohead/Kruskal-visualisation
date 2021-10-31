using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostVisualiser : MonoBehaviour {

	private int cost = 0;

	void Start() {
		transform.parent.gameObject.SetActive(false);
	}

	public void InitialiseCost() {
		cost = 0;
		transform.parent.gameObject.SetActive(true);
		transform.GetComponentInParent<Animation>().Play();
	}

	public void AddCost(int cost) {
		this.cost += cost;
		UpdateDisplay();
	}

	void UpdateDisplay() {
		GetComponent<TextMeshProUGUI>().text = $"Cost (distance):\t{cost}";
	}
}
