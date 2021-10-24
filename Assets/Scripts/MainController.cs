using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour {

	public TextMeshProUGUI pseudocode;
	public TextMeshProUGUI code;
	public ForestVisualizer forestVisualizer;

	private int pseudocodeState = 0;
	private int codeState = 0;

	private string highlightStart = "<mark=#00aeaf33>";
	//private string highlightStart = "<mark=#0000ff33>"; // pure blue?
	private string highlightEnd = "</mark>";

	void Start() {
		forestVisualizer.AddTree(new List<int> { 0, 1, 2, 3 });
		forestVisualizer.AddTree(new List<int> { 4, 5, 6 });
		forestVisualizer.JoinTrees(new List<int> { 0, 1, 2, 3 }, new List<int> { 4, 5, 6 });
		forestVisualizer.RemoveTree(new List<int> { 4, 5, 6 });
		pseudocodeState++;
		setPseduocodeHighlight();
		codeState += 3;
		setCodeHighlight();
	}

	void setPseduocodeHighlight() {
		string[] text = pseudocode.text.Split('\n');
		int tabs = text[pseudocodeState].Split('\t').Length - 1;
		text[pseudocodeState] = new string('\t', tabs) + highlightStart + text[pseudocodeState].Trim() + highlightEnd;
		pseudocode.text = string.Join("\n", text);
	}

	void setCodeHighlight() {
		string[] text = code.text.Split('\n');
		int tabs = text[pseudocodeState].Split('\t').Length - 1;
		text[codeState] = new string('\t', tabs) + highlightStart + text[codeState].Trim() + highlightEnd;
		code.text = string.Join("\n", text);
	}

	void Update() {

	}
}
