using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueVisualiser : MonoBehaviour {

	public GameObject edgePrefab;

	PriorityQueue<Edge> queue;

	void Start() {
		gameObject.SetActive(false);
	}

	public void InitialiseQueue() {
		gameObject.SetActive(true);
		queue = new PriorityQueue<Edge>();
	}

	public void Enqueue(Edge edge) {
		queue.Enqueue(edge);
		foreach (Transform transform in transform.GetChild(1)) {
			queue.Enqueue(transform.GetComponent<EdgeVisualiser>().GetEdge());
			Destroy(transform.gameObject);
		}
		while (queue.Count() > 0) {
			Edge e = queue.Dequeue();
			GameObject edgeObject = Instantiate(edgePrefab, transform.GetChild(1));
			edgeObject.GetComponent<EdgeVisualiser>().InitialiseEdge(e, false);
		}
	}

	public void Dequeue() {
		if (transform.GetChild(1).childCount > 0)
			Destroy(transform.GetChild(1).GetChild(0).gameObject);
	}

	public void ClearQueue() {
		transform.GetChild(1).gameObject.SetActive(false);
	}

	void Update() {

	}
}
