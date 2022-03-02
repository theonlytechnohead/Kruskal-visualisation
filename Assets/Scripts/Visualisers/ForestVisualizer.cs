using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ForestVisualizer : MonoBehaviour {

    public GameObject TreePrefab;
    public GameObject NodePrefab;
    public RectTransform forestLeft;
    public RectTransform forestRight;
    GridLayoutGroup leftGrid;
    GridLayoutGroup rightGrid;
    float leftIdealCellWidth;
    float rightIdealCellWidth;

    void Start () {
        leftGrid = forestLeft.gameObject.GetComponent<GridLayoutGroup>();
        rightGrid = forestRight.gameObject.GetComponent<GridLayoutGroup>();
        leftIdealCellWidth = leftGrid.cellSize.x;
        rightIdealCellWidth = rightGrid.cellSize.x;
    }

    void Update () {
        float totalWidth = RectTransformUtility.PixelAdjustRect(GetComponent<RectTransform>(), GetComponentInParent<Canvas>()).width;
        float totalChildren = forestLeft.childCount + forestRight.childCount;
        RectTransform deleting = null;
        foreach (Transform child in transform) {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            if (rectTransform != forestLeft && rectTransform != forestRight) {
                deleting = rectTransform;
                totalWidth -= RectTransformUtility.PixelAdjustRect(rectTransform, GetComponentInParent<Canvas>()).width;
            }
        }
        // Update left/right forest width split
        float left = totalWidth / totalChildren * forestLeft.childCount;
        float right = totalWidth / totalChildren * forestRight.childCount;
        Vector2 leftSize = forestLeft.sizeDelta;
        leftSize.x = left;
        forestLeft.sizeDelta = leftSize;
        Vector2 rightSize = forestRight.sizeDelta;
        rightSize.x = right;
        forestRight.sizeDelta = rightSize;

        if (deleting != null) {
            Vector2 deletingPosition = deleting.anchoredPosition;
            deletingPosition.x = leftSize.x;
            deleting.anchoredPosition = deletingPosition;
        }

        // Update individual trees
        Vector2 leftCellSize = leftGrid.cellSize;
        leftCellSize.x = Mathf.Lerp(leftCellSize.x, leftIdealCellWidth, Time.deltaTime);
        leftGrid.cellSize = leftCellSize;
        Vector2 rightCellSize = rightGrid.cellSize;
        rightCellSize.x = Mathf.Lerp(rightCellSize.x, rightIdealCellWidth, Time.deltaTime);
        rightGrid.cellSize = rightCellSize;
        AdjustTreeSizes(leftCellSize.x, rightCellSize.x);
    }

    float GetTotalWidth () {
        float totalWidth = RectTransformUtility.PixelAdjustRect(GetComponent<RectTransform>(), GetComponentInParent<Canvas>()).width;
        foreach (Transform child in transform) {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            if (rectTransform != forestLeft && rectTransform != forestRight) {
                totalWidth -= RectTransformUtility.PixelAdjustRect(rectTransform, GetComponentInParent<Canvas>()).width;
            }
        }
        return totalWidth;
    }

    public void AdjustCellSize (int divisions) {
        float totalWidth;
        totalWidth = GetTotalWidth();
        totalWidth -= forestLeft.GetComponent<GridLayoutGroup>().spacing.x * (divisions - 1);
        leftIdealCellWidth = totalWidth / divisions;
        totalWidth = GetTotalWidth();
        totalWidth -= forestRight.GetComponent<GridLayoutGroup>().spacing.x * (divisions - 1);
        rightIdealCellWidth = totalWidth / divisions;
    }

    public void AdjustTreeSizes (float leftCellWidth, float rightCellWidth) {
        foreach (var tree in forestLeft.GetComponentsInChildren<TreeGridManager>()) {
            tree.AdjustCellSize(leftCellWidth);
        }
        foreach (var tree in forestRight.GetComponentsInChildren<TreeGridManager>()) {
            tree.AdjustCellSize(rightCellWidth);
        }
    }

    int FindTree (List<int> tree) {
        for (int t = 0; t < forestLeft.childCount; t++) {
            List<int> nodes = new List<int>();
            Transform treeToCheck = forestLeft.GetChild(t);
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

    void AddToTree (GameObject tree, int n) {
        GameObject node = Instantiate(NodePrefab, tree.transform);
        AdjustTreeSizes(forestLeft.GetComponent<GridLayoutGroup>().cellSize.x, forestRight.GetComponent<GridLayoutGroup>().cellSize.x);
        node.GetComponent<TextMeshProUGUI>().text = n.ToString();
        tree.AddComponent<Flash>();
    }

    public void AddTree (List<int> nodes) {
        AdjustCellSize(forestLeft.childCount + 1 + forestRight.childCount);
        var tree = Instantiate(TreePrefab, forestLeft);
        foreach (int n in nodes) {
            AddToTree(tree, n);
        }
    }

    public void RemoveTree (List<int> nodes) {
        int index = FindTree(nodes);
        if (index != -1) {
            GameObject toDelete = null;
            List<Transform> toMove = new List<Transform>();
            foreach (Transform child in forestLeft) {
                RectTransform transform = child.GetComponent<RectTransform>();
                if (transform.GetSiblingIndex() == index) {
                    toDelete = child.gameObject;
                } else if (index < transform.GetSiblingIndex()) {
                    toMove.Add(transform);
                }
            }
            AdjustCellSize(forestLeft.childCount - 1 + forestRight.childCount);
            if (toDelete) {
                toDelete.AddComponent<WidthDestroy>();
                toDelete.GetComponent<WidthDestroy>().Anchor(index <= forestLeft.childCount);
            }
            if (toMove.Count > 0) {
                foreach (Transform child in toMove) {
                    child.SetParent(forestRight, false);
                }
            }
        }
    }

    public void ShuffleToLeft () {
        List<Transform> toMove = new List<Transform>();
        foreach (Transform child in forestRight) {
            toMove.Add(child);
        }
        if (toMove.Count > 0) {
            foreach (Transform child in toMove) {
                child.SetParent(forestLeft, false);
            }
        }
    }

    public void JoinTrees (List<int> tree1, List<int> tree2) {
        int index = FindTree(tree1);
        if (index != -1) {
            var tree = forestLeft.GetChild(index).gameObject;
            foreach (int n in tree2) {
                AddToTree(tree, n);
            }
        }
    }
}
