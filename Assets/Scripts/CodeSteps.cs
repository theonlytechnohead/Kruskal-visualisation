using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeSteps : MonoBehaviour {

	public ForestVisualizer forestVisualizer;

	// Function pointer for FSM
	public Func<int> next;

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
		// visualise forest
		foreach (List<int> tree in forest) {
			forestVisualizer.AddTree(tree);
		}
		next = InitialiseQueue;
		return 0;
	}

	PriorityQueue<Edge> queue;

	public int InitialiseQueue() {
		queue = new PriorityQueue<Edge>();
		next = FillQueue;
		return 0;
	}

	// TODO: unpack this foreach loop into individual steps
	public int FillQueue() {
		foreach (var edge in edges) {
			queue.Enqueue(edge);
		}
		next = InitialiseUsedEdges;
		return 0;
	}

	int used_edges;

	public int InitialiseUsedEdges() {
		used_edges = 0;
		next = InitialiseCost;
		return 0;
	}

	int cost;

	public int InitialiseCost() {
		cost = 0;
		next = CheckWhileCondition;
		return 0;
	}

	public int CheckWhileCondition() {
		if (used_edges < order - 1) {
			next = PeekEdge;
		} else {
			// move on to something else, update state?
		}
		return 0;
	}

	Edge edge;

	public int PeekEdge() {
		edge = queue.Peek();
		next = InitialiseTree1;
		return 0;
	}

	List<int> tree1;

	public int InitialiseTree1() {
		tree1 = null;
		next = InitialiseTree2;
		return 0;
	}

	List<int> tree2;

	public int InitialiseTree2() {
		tree2 = null;
		next = InitialiseForeachLoop;
		return 0;
	}

	int i;

	public int InitialiseForeachLoop() {
		i = 0;
		next = DoForeach;
		return 0;
	}

	List<int> tree;

	public int DoForeach() {
		tree = forest[i];
		next = CheckTree1Condition;
		return 0;
	}

	bool foundTree1 = false;

	public int CheckTree1Condition() {
		foundTree1 = tree.Contains(edge.source) && !tree.Contains(edge.destination);
		if (foundTree1)
			next = ApplyTree1;
		else
			next = CheckTree2Condition;
		return 0;
	}

	public int ApplyTree1() {
		if (foundTree1) {
			tree1 = tree;
		}
		next = CheckTree2Condition;
		return 0;
	}

	bool foundTree2 = false;

	public int CheckTree2Condition() {
		foundTree2 = !tree.Contains(edge.source) && tree.Contains(edge.destination);
		if (foundTree2)
			next = ApplyTree2;
		else
			next = CheckSameStreeCondition;
		return 0;
	}

	public int ApplyTree2() {
		if (foundTree2) {
			tree2 = tree;
		}
		next = CheckSameStreeCondition;
		return 0;
	}

	bool inSameTree = false;

	public int CheckSameStreeCondition() {
		inSameTree = tree.Contains(edge.source) && tree.Contains(edge.destination);
		if (inSameTree)
			next = ApplySameTree;
		else
			next = CheckForeach;
		return 0;
	}

	public int ApplySameTree() {
		if (inSameTree) {
			queue.Dequeue();
		}
		return 0;
	}

	public int CheckForeach() {
		if (i < forest.Count) {
			i++;
			next = DoForeach;
		} else {
			next = CheckTwoTreesCondition;
		}
		return 0;
	}

	bool gotTwoTrees = false;

	public int CheckTwoTreesCondition() {
		gotTwoTrees = tree1 != null && tree2 != null;
		if (gotTwoTrees)
			next = ApplyTwoTreesCondition;
		else
			next = null; // TODO: Check while condition
		return 0;
	}

	public int ApplyTwoTreesCondition() {
		if (gotTwoTrees) {
			// move state somwhere, probably next
		} else {
			// move state elsewhere, skip next block
		}
		return 0;
	}

	public int JoinTrees() {
		tree1.AddRange(tree2);
		forestVisualizer.JoinTrees(tree1, tree2);
		return 0;
	}

}
