using System;
using UnityEngine;
using ForceDirectedGraph;
using ForceDirectedGraph.DataStructure;
using Network = ForceDirectedGraph.DataStructure.Network;

public class GraphVisualiser : MonoBehaviour {

	[SerializeField]
	[Tooltip("The graph displaying the network.")]
	private GraphManager Graph;

	Network network = new Network();

	public void AddNode(int n) {
		network.Nodes.Add(new Node(Guid.NewGuid(), n.ToString(), Color.white));
	}

	public void AddEdge(int source, int destination, int distance) {
		network.Links.Add(new Link(network.Nodes[source].Id, network.Nodes[destination].Id, 0.5f - (0.1f * distance), Color.grey, distance));
	}

	public void HighlightEdge(Edge edge) {
		Graph.SetLinkColour(network.Nodes[edge.source].Id, network.Nodes[edge.destination].Id, Color.blue, Color.cyan, Color.green);
	}

	public void DimEdge(Edge edge) {
		Graph.SetLinkColour(network.Nodes[edge.source].Id, network.Nodes[edge.destination].Id, Color.black, Color.white, Color.white);
	}

	public void DisplayGraph() {
		Graph.Initialize(network);
	}

}
