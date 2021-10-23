using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KruskalScript {
	public int Kruskal(int order, List<List<int>> edges) {
		// define forest
		List<int> nodes = Enumerable.Range(0, order).ToList();
		List<List<int>> forest = nodes.ConvertAll(x => new List<int> { x });
		// define min-heap or priority queue
		// add edges to above
		// track number of edges used
		int used_edges = 0;
		// track cost
		int cost = 0;
		while (used_edges < order - 1) {
			// peek smallest
			List<int> tree1 = null;
			List<int> tree2 = null;
			foreach (List<int> tree in forest) {
				if (tree.Contains(0) && !tree.Contains(1))
					tree1 = tree;
				if (!tree.Contains(0) && tree.Contains(1))
					tree2 = tree;
				if (tree.Contains(0) && tree.Contains(1))
					// pop
					continue;
			}
			if (tree1 != null && tree2 != null) {
				tree1.Concat(tree2);
				forest.Remove(tree2);
				used_edges++;
				// pop
				cost += 0; // distance/weight
			}
		}
		return cost;
	}
}
