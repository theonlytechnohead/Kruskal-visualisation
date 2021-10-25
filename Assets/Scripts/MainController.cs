using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour {

	public TextMeshProUGUI pseudocode;
	public TextMeshProUGUI code;
	public CodeSteps steps;

	int pseudocodeState = 0;
	int prevPseudocodeState = -1;
	int codeState = 0;
	int prevState = -1;

	private string highlightStart = "<mark=#00aeaf33>";
	//private string highlightStart = "<mark=#0000ff33>"; // pure blue?
	private string highlightEnd = "</mark>";

	void Start() {
		steps.setOrder(4);
		steps.setEdges(GetDemoEdges());
		steps.next = steps.InitialiseForest;
		setPseduocodeHighlight();
		setCodeHighlight();
	}

	void setPseduocodeHighlight() {
		string[] text = pseudocode.text.Split('\n');
		if (pseudocodeState == text.Length) {
			pseudocodeState--;
		}
		if (prevPseudocodeState >= 0) {
			text[prevPseudocodeState] = text[prevPseudocodeState].Replace(highlightStart, "");
			text[prevPseudocodeState] = text[prevPseudocodeState].Replace(highlightEnd, "");
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
		if (prevState >= 0) {
			text[prevState] = text[prevState].Replace(highlightStart, "");
			text[prevState] = text[prevState].Replace(highlightEnd, "");
		}
		int tabs = text[codeState].Split('\t').Length - 1;
		text[codeState] = new string('\t', tabs) + highlightStart + text[codeState].Trim() + highlightEnd;
		code.text = string.Join("\n", text);
	}

	public void Step() {
		prevState = codeState;
		codeState = steps.next();
		prevPseudocodeState = pseudocodeState;
		pseudocodeState = PseudocodeSteps.ProcessState(codeState);
		setPseduocodeHighlight();
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
