using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeVisualiser : MonoBehaviour {

	public GameObject TreePrefab;
	public GameObject NodePrefab;

	public RectTransform tree1Holder;
	public RectTransform treeHolder;
	public RectTransform tree2Holder;

	GameObject tree1;
	GameObject tree;
	GameObject tree2;

	Animation tree1Animator;
	Animation tree2Animator;

	void Start() {
		gameObject.SetActive(false);
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(false);
		transform.GetChild(2).gameObject.SetActive(false);
		tree1Animator = tree1Holder.gameObject.GetComponent<Animation>();
		tree2Animator = tree2Holder.gameObject.GetComponent<Animation>();
	}

	void AddToTree(GameObject treeObject, int n) {
		var node = Instantiate(NodePrefab, treeObject.transform);
		treeObject.GetComponent<TreeGridManager>().AdjustCellSize(200);
		node.GetComponent<TextMeshProUGUI>().text = n.ToString();
	}

	public GameObject InstantiateTree(RectTransform parent) {
		gameObject.SetActive(true);
		var t = Instantiate(TreePrefab, parent);
		t.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		t.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
		t.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		t.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
		parent.gameObject.SetActive(false);
		return t;
	}

	public void InitialiseTree1() {
		if (tree1 != null) {
			Destroy(tree1);
		}
		tree1 = InstantiateTree(tree1Holder);
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void InitialiseTree() {
		if (tree != null) {
			Destroy(tree);
		}
		tree = InstantiateTree(treeHolder);
		transform.GetChild(1).gameObject.SetActive(true);
	}

	public void InitialiseTree2() {
		if (tree2 != null) {
			Destroy(tree2);
		}
		tree2 = InstantiateTree(tree2Holder);
		transform.GetChild(2).gameObject.SetActive(true);
	}

	public void SetTree1(List<int> nodes) {
		foreach (int n in nodes) {
			AddToTree(tree1, n);
		}
		tree1Holder.gameObject.SetActive(true);
		tree1Animator.Play();
	}

	public void SetTree(List<int> nodes) {
		foreach (int n in nodes) {
			AddToTree(tree, n);
		}
		treeHolder.gameObject.SetActive(true);
	}

	public void SetTree2(List<int> nodes) {
		foreach (int n in nodes) {
			AddToTree(tree2, n);
		}
		tree2Holder.gameObject.SetActive(true);
		tree2Animator.Play();
	}

	public void ClearTree1() {
		foreach (RectTransform node in tree1.transform) {
			Destroy(node.gameObject);
		}
	}

	public void ClearTree() {
		foreach (RectTransform node in tree.transform) {
			Destroy(node.gameObject);
		}
	}

	public void ClearTree2() {
		foreach (RectTransform node in tree2.transform) {
			Destroy(node.gameObject);
		}
	}

	public void DestroyTree1() {
		Destroy(tree1);
		tree1 = null;
		transform.GetChild(0).gameObject.SetActive(false);
		if (!tree1 && !tree && !tree2) {
			gameObject.SetActive(false);
		}
	}

	public void DestroyTree() {
		Destroy(tree);
		tree = null;
		transform.GetChild(1).gameObject.SetActive(false);
		if (!tree1 && !tree && !tree2) {
			gameObject.SetActive(false);
		}
	}

	public void DestroyTree2() {
		Destroy(tree2);
		tree2 = null;
		transform.GetChild(2).gameObject.SetActive(false);
		if (!tree1 && !tree && !tree2) {
			gameObject.SetActive(false);
		}
	}

	void Update() {

	}
}
