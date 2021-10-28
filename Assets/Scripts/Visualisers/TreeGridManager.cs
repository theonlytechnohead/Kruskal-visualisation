using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeGridManager : MonoBehaviour {

	public void AdjustCellSize(float totalWidth) {
		int divisions = transform.childCount;
		Vector3 cellSize = GetComponent<GridLayoutGroup>().cellSize;
		totalWidth -= GetComponent<GridLayoutGroup>().spacing.x * (divisions - 1);
		cellSize.x = totalWidth / divisions;
		GetComponent<GridLayoutGroup>().cellSize = cellSize;
	}

}
