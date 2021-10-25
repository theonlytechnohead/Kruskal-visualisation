using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour {

	public TextMeshProUGUI pseudocode;
	public TextMeshProUGUI code;
	public ForestVisualizer forestVisualizer;
	public CodeSteps steps;

	public int pseudocodeState = -1;
	public int codeState = -1;

	private string highlightStart = "<mark=#00aeaf33>";
	//private string highlightStart = "<mark=#0000ff33>"; // pure blue?
	private string highlightEnd = "</mark>";

	void Start() {
		//forestVisualizer.AddTree(new List<int> { 0, 1, 2, 3 });
		//forestVisualizer.AddTree(new List<int> { 4, 5, 6 });
		//forestVisualizer.JoinTrees(new List<int> { 0, 1, 2, 3 }, new List<int> { 4, 5, 6 });
		//forestVisualizer.RemoveTree(new List<int> { 4, 5, 6 });

		// actual setup stuff
		steps.setOrder(4);
		steps.setEdges(GetDemoEdges());
		steps.kruskalStep = steps.InitialiseForest;
	}

	void setPseduocodeHighlight() {
		string[] text = pseudocode.text.Split('\n');
		if (pseudocodeState == text.Length) {
			pseudocodeState--;
		}
		if (pseudocodeState > 0) {
			text[pseudocodeState - 1] = text[pseudocodeState - 1].Replace(highlightStart, "");
			text[pseudocodeState - 1] = text[pseudocodeState - 1].Replace(highlightEnd, "");
		}
		int tabs = text[pseudocodeState].Split('\t').Length - 1;
		text[pseudocodeState] = new string('\t', tabs) + highlightStart + text[pseudocodeState].Trim() + highlightEnd;
		pseudocode.text = string.Join("\n", text);
	}

	void setCodeHighlight() {
		string[] text = code.text.Split('\n');
		if (codeState == text.Length) {
			codeState--;
		}
		if (codeState > 0) {
			text[codeState - 1] = text[codeState - 1].Replace(highlightStart, "");
			text[codeState - 1] = text[codeState - 1].Replace(highlightEnd, "");
		}
		int tabs = text[codeState].Split('\t').Length - 1;
		text[codeState] = new string('\t', tabs) + highlightStart + text[codeState].Trim() + highlightEnd;
		code.text = string.Join("\n", text);
	}

	public void Step() {
		steps.kruskalStep();
		pseudocodeState++;
		setPseduocodeHighlight();
		codeState++;
		setCodeHighlight();
	}

	void Update() {

	}

	public List<Edge> GetDemoEdges() {
		int order = 4;
		string input = @"0 1 0 4
1 0 3 1
0 3 0 2
4 1 2 0";
		List<Edge> edges = new List<Edge>();
		foreach (int node in Enumerable.Range(0, order)) {
			string[] line = input.Split('\n')[node].Trim().Split(' ');
			foreach (int n in Enumerable.Range(0, line.Length)) {
				if (int.Parse(line[n]) != 0) {
					edges.Add(new Edge(int.Parse(line[n]), node, n));
				}
			}
		}
		return edges;
	}
}
