using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ForestVisualizer : MonoBehaviour {

	public GameObject TreePrefab;
	public GameObject NodePrefab;

	void Start() {

	}

	void Update() {

	}

	void AdjustCellSize(int divisions) {
		Vector3 cellSize = GetComponent<GridLayoutGroup>().cellSize;
		float totalWidth = RectTransformUtility.PixelAdjustRect(GetComponent<RectTransform>(), GetComponentInParent<Canvas>()).width;
		totalWidth -= GetComponent<GridLayoutGroup>().spacing.x * (divisions - 1);
		cellSize.x = totalWidth / divisions;
		GetComponent<GridLayoutGroup>().cellSize = cellSize;
	}

	void AdjustTreeSizes(float cellWidth) {
		foreach (var tree in transform.GetComponentsInChildren<TreeGridManager>()) {
			tree.AdjustCellSize(cellWidth);
		}
	}

	int FindTree(List<int> tree) {
		for (int t = 0; t < transform.childCount; t++) {
			List<int> nodes = new List<int>();
			Transform treeToCheck = transform.GetChild(t);
			for (int n = 0; n < treeToCheck.childCount; n++) {
				var node = treeToCheck.GetChild(n);
				nodes.Add(int.Parse(node.GetComponent<TextMeshProUGUI>().text));
			}
			if (tree.SequenceEqual(nodes)) {
				return t;
			}
		}
		return -1;
	}

	void AddToTree(GameObject tree, int n) {
		var node = Instantiate(NodePrefab, tree.transform);
		AdjustTreeSizes(GetComponent<GridLayoutGroup>().cellSize.x);
		node.GetComponent<TextMeshProUGUI>().text = n.ToString();
	}

	public void AddTree(List<int> nodes) {
		AdjustCellSize(transform.childCount + 1);
		var tree = Instantiate(TreePrefab, transform);
		foreach (int n in nodes) {
			AddToTree(tree, n);
		}
	}

	public void RemoveTree(List<int> nodes) {
		int index = FindTree(nodes);
		if (index != -1) {
			AdjustCellSize(transform.childCount - 1);
			Destroy(transform.GetChild(index).gameObject);
			AdjustTreeSizes(GetComponent<GridLayoutGroup>().cellSize.x);
		}
	}

	public void JoinTrees(List<int> tree1, List<int> tree2) {
		int index = FindTree(tree1);
		if (index != -1) {
			var tree = transform.GetChild(index).gameObject;
			foreach (int n in tree2) {
				AddToTree(tree, n);
			}
		}
	}
}
