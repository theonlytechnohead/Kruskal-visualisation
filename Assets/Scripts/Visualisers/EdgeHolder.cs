using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHolder : MonoBehaviour {

	public GameObject edgePrefab;
	private GameObject edgeObject;

	void Start() {

	}

	public void InitialiseEdgeHolder(Edge edge) {
		if (edgeObject) {
			Destroy(edgeObject);
			edgeObject = null;
		}
		edgeObject = Instantiate(edgePrefab, transform);
		edgeObject.GetComponent<EdgeVisualiser>().InitialiseEdge(edge, true);
	}

	public void DestroyEdgeHolder() {
		Destroy(edgeObject);
		edgeObject = null;
	}

	void Update() {

	}
}
