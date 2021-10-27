using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LineSelected : MonoBehaviour, IPointerExitHandler {

	public GameObject popup;
	public RectTransform canvasRect;

	private List<string> descriptions = new List<string> {
		"This is the definition of the function, which specifies several key properties. <style=\"Code\">public</style> means that the function is publicly accessible by other code, whilst <style=\"Code\">static</style> ensures that it does not require instantiation to run. <style=\"Code\">int Kruskal</style> says that the function is called 'Kruskal,' and will return a single <style=\"Code\">integer</style> (number). <style=\"Code\">int order</style> is the first parameter it requires, specifying the order of the graph, and <style=\"Code\">List<Edge> edges</style> is naturally a list of the edges between the nodes in the graph."
	};

	TextMeshProUGUI textMesh;

	int currentIndex;

	private void Start() {
		textMesh = GetComponent<TextMeshProUGUI>();
		popup.SetActive(false);
	}

	public void LineInfoSelection(int lineIndex, TMP_LineInfo lineInfo) {
		currentIndex = lineIndex;
		float lineY = (textMesh.rectTransform.rect.height + 5f) - lineInfo.lineExtents.min.y;
		popup.SetActive(true);
		Vector3 pos = popup.GetComponent<RectTransform>().anchoredPosition;
		pos.y = -lineY;
		popup.GetComponent<RectTransform>().anchoredPosition = pos;
	}

	public void Update() {
		// sets popup X pos
		if (popup.activeSelf) {
			Vector3 pos = popup.GetComponent<RectTransform>().anchoredPosition;
			Vector2 canvasSpaceMouse;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out canvasSpaceMouse);
			pos.x = canvasSpaceMouse.x + (canvasRect.rect.width / 2);
			popup.GetComponent<RectTransform>().anchoredPosition = pos;
			UpdatePopupDescription();
		}
	}

	void UpdatePopupDescription() {
		if (currentIndex < descriptions.Count) {
			popup.GetComponentInChildren<TextMeshProUGUI>().text = descriptions[currentIndex];
		} else {
			popup.GetComponentInChildren<TextMeshProUGUI>().text = "No description";
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		popup.SetActive(false);
	}
}
