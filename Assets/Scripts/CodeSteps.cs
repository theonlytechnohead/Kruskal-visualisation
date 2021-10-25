using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeSteps : MonoBehaviour {

	// Function pointer for FSM
	public Func<int> kruskalStep;

	int order;
	List<Edge> edges;


	public void setOrder(int order) {
		this.order = order;
	}

	public void setEdges(List<Edge> edges) {
		this.edges = edges;
	}

	List<List<int>> forest;

	public int InitialiseForest() {
		forest = Enumerable.Range(0, order).ToList().ConvertAll(x => new List<int> { x });
		return 0;
	}

	PriorityQueue<Edge> queue;

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

	int used_edges;

	public int InitialiseUsedEdges() {
		used_edges = 0;
		return 0;
	}

	int cost;

	public int InitialiseCost() {
		cost = 0;
		return 0;
	}

	public int CheckWhileCondition() {
		if (used_edges < order - 1) {
			// do something with state
		} else {
			// move on to something else, update state?
		}
		return 0;
	}

	Edge edge;

	public int PeekEdge() {
		edge = queue.Peek();
		return 0;
	}

	List<int> tree1;

	public int InitialiseTree1() {
		tree1 = null;
		return 0;
	}

	List<int> tree2;

	public int InitialiseTree2() {
		tree2 = null;
		return 0;
	}

	int i;

	public int InitialiseForeachLoop() {
		i = 0;
		return 0;
	}

	List<int> tree;

	public int DoForeach() {
		if (i < forest.Count) {
			tree = forest[i];
			i++;
		} else {
			// move on to something else
		}
		return 0;
	}

	bool foundTree1 = false;

	public int CheckTree1Condition() {
		foundTree1 = tree.Contains(edge.source) && !tree.Contains(edge.destination);
		return 0;
	}

	public int ApplyTree1() {
		if (foundTree1) {
			tree1 = tree;
		}
		return 0;
	}

	bool foundTree2 = false;

	public int CheckTree2Condition() {
		foundTree2 = !tree.Contains(edge.source) && tree.Contains(edge.destination);
		return 0;
	}

	public int ApplyTree2() {
		if (foundTree2) {
			tree2 = tree;
		}
		return 0;
	}

	bool inSameTree = false;

	public int CheckSameStreeCondition() {
		inSameTree = tree.Contains(edge.source) && tree.Contains(edge.destination);
		return 0;
	}

	public int ApplySameTree() {
		if (inSameTree) {
			queue.Dequeue();
		}
		return 0;
	}

}
