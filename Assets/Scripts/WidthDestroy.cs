using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidthDestroy : MonoBehaviour {

    Transform canvas;
    ForestVisualizer forestVisualizer;
    RectTransform rectTransform;
    GridLayoutGroup gridLayoutGroup;

    // Start is called before the first frame update
    void Start () {
        canvas = transform.parent.parent;
        forestVisualizer = transform.parent.GetComponent<ForestVisualizer>();
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    // Update is called once per frame
    void Update () {
        transform.SetParent(canvas, true);
        Vector2 width = rectTransform.sizeDelta;
        width.x = Mathf.Lerp(width.x, 0f, 2f * Time.deltaTime);
        rectTransform.sizeDelta = width;
        gridLayoutGroup.cellSize= width;
        if (width.x < 0.2f) {
            Destroy(gameObject);
        }
    }
}
