using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostVisualiser : MonoBehaviour {

	private int cost = 0;

	void Start() {
		gameObject.SetActive(false);
	}

	public void InitialiseCost() {
		cost = 0;
		gameObject.SetActive(true);
	}

	public void AddCost(int cost) {
		this.cost += cost;
		UpdateDisplay();
	}

	void UpdateDisplay() {
		GetComponent<TextMeshProUGUI>().text = $"Cost (distance):\t{cost}";
	}
}
