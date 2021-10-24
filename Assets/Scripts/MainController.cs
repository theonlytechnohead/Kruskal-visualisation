using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public ForestVisualizer forestVisualizer;

	void Start() {
		forestVisualizer.AddTree(new List<int> { 0, 1, 2, 3 });
		forestVisualizer.AddTree(new List<int> { 4, 5, 6 });
		forestVisualizer.JoinTrees(new List<int> { 0, 1, 2, 3 }, new List<int> { 4, 5, 6 });
		forestVisualizer.RemoveTree(new List<int> { 4, 5, 6 });
	}

	void Update() {

	}
}
