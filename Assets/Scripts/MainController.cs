using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour {

	public TextMeshProUGUI pseudocode;
	public TextMeshProUGUI code;
	public ForestVisualizer forestVisualizer;

	public int pseudocodeState = -1;
	public int codeState = -1;

	private string highlightStart = "<mark=#00aeaf33>";
	//private string highlightStart = "<mark=#0000ff33>"; // pure blue?
	private string highlightEnd = "</mark>";

	void Start() {
		forestVisualizer.AddTree(new List<int> { 0, 1, 2, 3 });
		forestVisualizer.AddTree(new List<int> { 4, 5, 6 });
		forestVisualizer.JoinTrees(new List<int> { 0, 1, 2, 3 }, new List<int> { 4, 5, 6 });
		forestVisualizer.RemoveTree(new List<int> { 4, 5, 6 });
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
		pseudocodeState++;
		setPseduocodeHighlight();
		codeState++;
		setCodeHighlight();
	}

	void Update() {

	}
}
