using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KruskalScript {
	public static int Kruskal(int order, List<Edge> edges) {
		List<List<int>> forest = Enumerable.Range(0, order).ToList().ConvertAll(x => new List<int> { x });
		PriorityQueue<Edge> queue = new PriorityQueue<Edge>();
		foreach (var edge in edges) {
			queue.Enqueue(edge);
		}
		int used_edges = 0;
		int cost = 0;
		while (used_edges < order - 1) {
			printForest(forest);
			Edge edge = queue.Peek();
			List<int> tree1 = null;
			int index1 = 0;
			List<int> tree2 = null;
			int index2 = 0;
			for (int i = 0; i < forest.Count; i++) {
				List<int> tree = forest[i];
				if (tree.Contains(edge.source) && !tree.Contains(edge.destination)) {
					tree1 = tree;
					index1 = i;
				}

				if (!tree.Contains(edge.source) && tree.Contains(edge.destination)) {
					tree2 = tree;
					index2 = i;
				}

				if (tree.Contains(edge.source) && tree.Contains(edge.destination)) {
					queue.Dequeue();
					continue;
				}
			}
			if (tree1 != null && tree2 != null) {
				tree1.Concat(tree2);
				forest = new List<List<int>>() { tree1 };
				//forest.RemoveAt(index2);
				//forest.Remove(tree2);
				used_edges++;
				queue.Dequeue();
				cost += edge.priority;
			}
		}
		return cost;
	}

	public static void printForest(List<List<int>> forest) {
		string output = "forest: ";
		foreach (var tree in forest) {
			output += "[";
			output += string.Join(",", tree);
			output += "] ";
		}
		Debug.Log(output);
	}
}
