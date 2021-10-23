using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestKruskal : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		int order1 = 4;
		string input1 = @"0 1 0 4
1 0 3 1
0 3 0 2
4 1 2 0";
		List<Edge> edges = new List<Edge>();
		foreach (int node in Enumerable.Range(0, order1)) {
			string[] line = input1.Split('\n')[node].Trim().Split(' ');
			foreach (int n in Enumerable.Range(0, line.Length)) {
				if (int.Parse(line[n]) != 0) {
					edges.Add(new Edge(int.Parse(line[n]), node, n));
				}
			}
		}
		//print(string.Join(" ", edges));
		List<List<int>> forest = Enumerable.Range(0, order1).ToList().ConvertAll(x => new List<int> { x });
		//print(string.Join(" ", forest));
		PriorityQueue<Edge> queue = new PriorityQueue<Edge>();
		foreach (var edge in edges) {
			queue.Enqueue(edge);
		}

		//print(queueString);
		//KruskalScript.printForest(forest);
		int cost1 = KruskalScript.Kruskal(order1, edges);
		print(cost1);
	}

	// Update is called once per frame
	void Update() {

	}
}
