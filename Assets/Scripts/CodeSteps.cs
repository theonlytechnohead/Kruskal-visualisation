using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeSteps : MonoBehaviour {

	public ForestVisualizer forestVisualizer;

	// Function pointer for FSM
	public Func<int> next;

	// Code states (line numbers)
	enum state {
		function,
		initForest,
		initQueue,
		edgeForeach,
		enqueueEdge,
		endEdgeForeach,
		initUsedEdges,
		initCost,
		doWhile,
		peekEdge,
		initTree1,
		initTree2,
		doForeach,
		checkTree1,
		applyTree1,
		checkTree2,
		applyTree2,
		checkSameTree,
		applySameTree,
		endForeach,
		checkTwoTrees,
		mergeTrees,
		removeMergedTree,
		incrementUsedEdges,
		popEdge,
		updateCost,
		endIf,
		checkWhile,
		returnCost,
		end
	}

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
		foreach (List<int> tree in forest) {
			forestVisualizer.AddTree(tree);
		}
		next = InitialiseQueue;
		return (int)state.initForest;
	}

	PriorityQueue<Edge> queue;

	public int InitialiseQueue() {
		queue = new PriorityQueue<Edge>();
		next = FillQueue;
		return (int)state.initQueue;
	}

	// TODO: unpack this foreach loop into individual steps
	public int FillQueue() {
		foreach (var edge in edges) {
			queue.Enqueue(edge);
		}
		next = InitialiseUsedEdges;
		return (int)state.endEdgeForeach;
	}

	int used_edges;

	public int InitialiseUsedEdges() {
		used_edges = 0;
		next = InitialiseCost;
		return (int)state.initUsedEdges;
	}

	int cost;

	public int InitialiseCost() {
		cost = 0;
		next = DoWhile;
		return (int)state.initCost;
	}

	public int DoWhile() {
		next = PeekEdge;
		return (int)state.doWhile;
	}

	Edge edge;

	public int PeekEdge() {
		edge = queue.Peek();
		next = InitialiseTree1;
		return (int)state.peekEdge;
	}

	List<int> tree1;

	public int InitialiseTree1() {
		tree1 = null;
		next = InitialiseTree2;
		return (int)state.initTree1;
	}

	List<int> tree2;

	public int InitialiseTree2() {
		tree2 = null;
		next = InitialiseForeachLoop;
		return (int)state.initTree2;
	}

	int i;

	public int InitialiseForeachLoop() {
		i = 0;
		return DoForeach();
	}

	List<int> tree;

	public int DoForeach() {
		tree = forest[i];
		next = CheckTree1Condition;
		return (int)state.doForeach;
	}

	bool foundTree1 = false;

	public int CheckTree1Condition() {
		foundTree1 = tree.Contains(edge.source) && !tree.Contains(edge.destination);
		if (foundTree1)
			next = ApplyTree1;
		else
			next = CheckTree2Condition;
		return (int)state.checkTree1;
	}

	public int ApplyTree1() {
		if (foundTree1) {
			tree1 = tree;
		}
		next = CheckTree2Condition;
		return (int)state.applyTree1;
	}

	bool foundTree2 = false;

	public int CheckTree2Condition() {
		foundTree2 = !tree.Contains(edge.source) && tree.Contains(edge.destination);
		if (foundTree2)
			next = ApplyTree2;
		else
			next = CheckSameStreeCondition;
		return (int)state.checkTree2;
	}

	public int ApplyTree2() {
		if (foundTree2) {
			tree2 = tree;
		}
		next = CheckSameStreeCondition;
		return (int)state.applyTree2;
	}

	bool inSameTree = false;

	public int CheckSameStreeCondition() {
		inSameTree = tree.Contains(edge.source) && tree.Contains(edge.destination);
		if (inSameTree)
			next = ApplySameTree;
		else
			next = CheckForeach;
		return (int)state.checkSameTree;
	}

	public int ApplySameTree() {
		if (inSameTree) {
			queue.Dequeue();
		}
		next = CheckForeach;
		return (int)state.applySameTree;
	}

	public int CheckForeach() {
		if (i < forest.Count - 1) {
			i++;
			next = DoForeach;
		} else {
			next = CheckTwoTreesCondition;
		}
		return (int)state.endForeach;
	}

	bool gotTwoTrees = false;

	public int CheckTwoTreesCondition() {
		gotTwoTrees = tree1 != null && tree2 != null;
		if (gotTwoTrees)
			next = JoinTrees;
		else
			next = EndIf;
		return (int)state.checkTwoTrees;
	}

	public int JoinTrees() {
		forestVisualizer.JoinTrees(tree1, tree2);
		tree1.AddRange(tree2);
		next = RemoveTree;
		return (int)state.mergeTrees;
	}

	public int RemoveTree() {
		forest.Remove(tree2);
		forestVisualizer.RemoveTree(tree2);
		next = IncrementUsedEdges;
		return (int)state.removeMergedTree;
	}

	public int IncrementUsedEdges() {
		used_edges++;
		next = PopEdge;
		return (int)state.incrementUsedEdges;
	}

	public int PopEdge() {
		queue.Dequeue();
		next = UpdateCost;
		return (int)state.popEdge;
	}

	public int UpdateCost() {
		cost += edge.priority;
		next = EndIf;
		return (int)state.updateCost;
	}

	public int EndIf() {
		next = CheckWhile;
		return (int)state.endIf;
	}

	public int CheckWhile() {
		if (used_edges < order - 1) {
			next = DoWhile;
		} else {
			next = ReturnCost;
		}
		return (int)state.checkWhile;
	}

	public int ReturnCost() {
		next = End;
		return (int)state.returnCost;
	}

	public int End() {
		return (int)state.end;
	}

}
