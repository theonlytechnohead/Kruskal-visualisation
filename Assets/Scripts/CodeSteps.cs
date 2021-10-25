using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeSteps : MonoBehaviour {

	// Function pointer for FSM
	public Func<int> kruskalStep;

	int order;
	List<Edge> edges;

	List<List<int>> forest;
	PriorityQueue<Edge> queue;

	int used_edges;
	int cost;

	public void setOrder(int order) {
		this.order = order;
	}

	public void setEdges(List<Edge> edges) {
		this.edges = edges;
	}

	public int InitialiseForest() {
		forest = Enumerable.Range(0, order).ToList().ConvertAll(x => new List<int> { x });
		return 0;
	}

	public int InitialiseQueue() {
		queue = new PriorityQueue<Edge>();
		return 0;
	}

	public int FillQueue() {
		foreach (var edge in edges) {
			queue.Enqueue(edge);
		}
		return 0;
	}

	public int InitialiseUsedEdges() {
		used_edges = 0;
		return 0;
	}

	public int InitialiseCost() {
		cost = 0;
		return 0;
	}

}
