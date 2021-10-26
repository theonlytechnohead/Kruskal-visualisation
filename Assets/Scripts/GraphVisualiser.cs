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

	public void AddEdge(int source, int destination) {
		network.Links.Add(new Link(network.Nodes[source].Id, network.Nodes[destination].Id, 0.5f, Color.grey));
	}

	public void HighlightEdge(Edge edge) {
		Graph.SetLinkColour(network.Nodes[edge.source].Id, network.Nodes[edge.destination].Id, Color.blue);
	}

	public void DimEdge(Edge edge) {
		Graph.SetLinkColour(network.Nodes[edge.source].Id, network.Nodes[edge.destination].Id, Color.black);
	}

	public void DisplayGraph() {
		Graph.Initialize(network);
	}

}
