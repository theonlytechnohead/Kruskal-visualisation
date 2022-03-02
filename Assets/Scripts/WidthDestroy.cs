using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidthDestroy : MonoBehaviour {

    ForestVisualizer forestVisualizer;
    RectTransform rectTransform;
    GridLayoutGroup gridLayoutGroup;

    // Start is called before the first frame update
    void Start () {
        forestVisualizer = transform.parent.parent.GetComponent<ForestVisualizer>();
        rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        transform.SetParent(forestVisualizer.transform, false);
        rectTransform.pivot = new Vector2(0, 0.5f);
    }

    // Update is called once per frame
    void Update () {
        Vector2 width = rectTransform.sizeDelta;
        width.x = Mathf.Lerp(width.x, 0f, 2f * Time.deltaTime);
        rectTransform.sizeDelta = width;
        gridLayoutGroup.cellSize = width;
        if (width.x < 0.15f) {
            forestVisualizer.ShuffleToLeft();
            Destroy(gameObject);
        }
    }

    public void Anchor (bool left) {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (left) {
            rectTransform.pivot = new Vector2(0, 0.5f);
        } else {
            rectTransform.pivot = new Vector2(1, 0.5f);
        }
    }
}
