using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestKruskal : MonoBehaviour {
	void Start() {
		int order1 = 4;
		string input1 = @"0 1 0 4
1 0 3 1
0 3 0 2
4 1 2 0";
		List<Edge> edges1 = new List<Edge>();
		foreach (int node in Enumerable.Range(0, order1)) {
			string[] line = input1.Split('\n')[node].Trim().Split(' ');
			foreach (int n in Enumerable.Range(0, line.Length)) {
				if (int.Parse(line[n]) != 0) {
					edges1.Add(new Edge(int.Parse(line[n]), node, n));
				}
			}
		}
		List<List<int>> forest1 = Enumerable.Range(0, order1).ToList().ConvertAll(x => new List<int> { x });
		PriorityQueue<Edge> queue1 = new PriorityQueue<Edge>();
		foreach (var edge in edges1) {
			queue1.Enqueue(edge);
		}
		int cost1 = KruskalScript.Kruskal(order1, edges1);
		print(cost1);

		int order2 = 3;
		string input2 = @"0 1 2
1 0 1
2 1 0";
		List<Edge> edges2 = new List<Edge>();
		foreach (int node in Enumerable.Range(0, order2)) {
			string[] line = input2.Split('\n')[node].Trim().Split(' ');
			foreach (int n in Enumerable.Range(0, line.Length)) {
				if (int.Parse(line[n]) != 0) {
					edges2.Add(new Edge(int.Parse(line[n]), node, n));
				}
			}
		}
		List<List<int>> forest2 = Enumerable.Range(0, order2).ToList().ConvertAll(x => new List<int> { x });
		PriorityQueue<Edge> queue2 = new PriorityQueue<Edge>();
		foreach (var edge in edges2) {
			queue2.Enqueue(edge);
		}
		int cost2 = KruskalScript.Kruskal(order2, edges2);
		print(cost2);
	}

	void Update() {

	}
}
