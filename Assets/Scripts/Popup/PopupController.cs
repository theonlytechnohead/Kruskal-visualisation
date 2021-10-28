using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PopupController : MonoBehaviour {

	public GameObject codePopup;
	public GameObject pseudocodePopup;
	RectTransform canvasRect;

	public TextMeshProUGUI codeText;
	int codeIndex;
	public TextMeshProUGUI pseudocodeText;
	int pseudocodeIndex;

	void Start() {
		canvasRect = GetComponent<RectTransform>();
		codePopup.SetActive(false);
		pseudocodePopup.SetActive(false);

	}

	public void CodeInfoSelection(int lineIndex, TMP_LineInfo lineInfo) {
		codeIndex = lineIndex;
		float lineY = (codeText.rectTransform.rect.height + 5f) - lineInfo.lineExtents.min.y;
		codePopup.SetActive(true);
		pseudocodePopup.SetActive(false);
		Vector3 pos = codePopup.GetComponent<RectTransform>().anchoredPosition;
		pos.y = -lineY;
		codePopup.GetComponent<RectTransform>().anchoredPosition = pos;
	}

	public void PseudocodeInfoSelection(int lineIndex, TMP_LineInfo lineInfo) {
		pseudocodeIndex = lineIndex;
		float lineY = (pseudocodeText.rectTransform.rect.yMax + 5f) + lineInfo.lineExtents.min.y + lineInfo.lineHeight;
		pseudocodePopup.SetActive(true);
		codePopup.SetActive(false);
		Vector3 pos = codePopup.GetComponent<RectTransform>().anchoredPosition;
		pos.y = lineY;
		pseudocodePopup.GetComponent<RectTransform>().anchoredPosition = pos;
	}

	void Update() {
		if (codePopup.activeSelf) {
			Vector3 pos = codePopup.GetComponent<RectTransform>().anchoredPosition;
			Vector2 canvasSpaceMouse;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out canvasSpaceMouse);
			pos.x = canvasSpaceMouse.x + (canvasRect.rect.width / 2);
			codePopup.GetComponent<RectTransform>().anchoredPosition = pos;
			UpdatePopupDescription();
		}
		if (pseudocodePopup.activeSelf) {
			Vector3 pos = pseudocodePopup.GetComponent<RectTransform>().anchoredPosition;
			Vector2 canvasSpaceMouse;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out canvasSpaceMouse);
			pos.x = canvasSpaceMouse.x + (canvasRect.rect.width / 2);
			pseudocodePopup.GetComponent<RectTransform>().anchoredPosition = pos;
			UpdatePseudocodePopupDescription();
		}
	}

	void UpdatePopupDescription() {
		if (codeIndex < codeDescriptions.Count) {
			codePopup.GetComponentInChildren<TextMeshProUGUI>().text = codeDescriptions[codeIndex];
		} else {
			codePopup.GetComponentInChildren<TextMeshProUGUI>().text = "No description";
		}
	}

	void UpdatePseudocodePopupDescription() {
		if (pseudocodeIndex < pseudocodeDescriptions.Count) {
			pseudocodePopup.GetComponentInChildren<TextMeshProUGUI>().text = pseudocodeDescriptions[pseudocodeIndex];
		} else {
			pseudocodePopup.GetComponentInChildren<TextMeshProUGUI>().text = "No description";
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		codePopup.SetActive(false);
		pseudocodePopup.SetActive(false);
	}

	private List<string> codeDescriptions = new List<string> {
		"This is the definition of the function, which specifies several key properties. <style=\"Code\">public</style> means that the function is publicly accessible by other code, whilst <style=\"Code\">static</style> ensures that it does not require instantiation to run. <style=\"Code\">int Kruskal</style> says that the function is called 'Kruskal,' and will return a single <style=\"Code\">integer</style> (number). <style=\"Code\">int order</style> is the first parameter it requires, specifying the order of the graph, and <style=\"Code\">List<Edge> edges</style> is naturally a list of the edges between the nodes in the graph.",
		"This tricky line is much easier understood in Python code. It's not very important to understand, as it can be much better visualised instead. It constructs a list of lists, each of which holds a single node.",
		"This queue holds all the edges of the graph, which we need for later. As this is a priority queue, it will create and maintain a sorted order, from least to most by edge distance.",
		"This loop will grab each <style=\"Code\">edge</style> from the <style=\"Code\">edges</style> given to us as part of the graph, and do something with it.",
		"Here we do something with the <style=\"Code\">edge</style>, namely adding it to the <style=\"Code\">queue</style>, which ensures it is sorted appropriately.",
		"This concludes the <style=\"Code\">foreach</style> loop code block",
		"We'll keep track of the number of edges we've used, so we know when to stop. This is just a number (<style=\"Code\">integer</style>)",
		"Here's another number (<style=\"Code\">integer</style>), which will track the 'cost' or total distance required to traverse the graph. This is the whole reason for what we're doing!",
		"Here is where we do the bulk of the work, everything prior was just to set the scene and prepare everything we need. We'll keep doing everything inside this code block over and over, until the <style=\"Code\">used_edges</style> matches the nodes in the graph (minus one because the edges go between the nodes).",
		"We'll grab the next smallest <style=\"Code\">edge</style> here, so we can more easily do things later on.",
		"This will define a place to put a <style=\"Code\">tree</style> of nodes, once we find something suitable.",
		"Ditto from above, it's another tree for when we need to store something later on.",
		"This <style=\"Code\">foreach</style> loop will go through every single <style=\"Code\">tree</style> in our search <style=\"Code\">forest</style>, executing the checks below.",
		"The first check we do sees whether the <style=\"Code\">tree</style> we're looking at has the <style=\"Code\">source</style> node, but not the <style=\"Code\">destination</style> node.",
		"If so, we'll keep it for later.",
		"The second check is for when the <style=\"Code\">tree</style> we're looking at does not have the <style=\"Code\">source</style> node, but does have the <style=\"Code\">destination</style> node.",
		"Equally, we'll keep this one for later, too.",
		"And the last case is for when the <style=\"Code\">tree</style> already has both the <style=\"Code\">source</style> and <style=\"Code\">destination</style> nodes, in which case we don't want to do anything more.",
		"And we'll just ditch this edge, because it's not needed.",
		"This concludes the <style=\"Code\">foreach</style> code block that goes through all the <style=\"Code\">tree</style>s in the search <style=\"Code\">forest</style>.",
		"Now, we only want to go ahead and do this stuff if we've definitely got two <style=\"Code\">tree</style>s to do it with. We can confidently do stuff with them, because of how carefully we picked them earlier. If we didn't find two trees, we'll skip this whole code block.",
		"Here we merge the two <style=\"Code\">tree</style>'s nodes, by taking the nodes from <style=\"Code\">tree2</style> and adding them to <style=\"Code\">tree1</style>.",
		"We don't need <style=\"Code\">tree2</style> anymore, because all of it's nodes are a part of <style=\"Code\">tree1</style> now. Thus, we'll <style=\"Code\">remove</style> it.",
		"We've used an edge! Time to increment (add one) to the number of edges we've used.",
		"And because we have used this edge, we can remove it from our queue. It's been processed!",
		"And the last thing to make sure we do, is to update our running total <style=\"Code\">cost</style> (a.k.a. distance).",
		"This concludes the <style=\"Code\">if</style> statement's code block, and where we'll jump to if we skip it.",
		"This concludes the <style=\"Code\">while</style> loop's code block, and is where we'll end up once we've used all the edges we need.",
		"Here is where we <style=\"Code\">return</style> our final result, the <style=\"Code\">cost</style> (a.k.a. distance) of going through all the nodes in the graph.",
		"It's all done now!"
	};

	private List<string> pseudocodeDescriptions = new List<string> {
		"The first line defines this as a <style=\"Code\">function</style>, named <style=\"Code\">Kruskal</style>. It will require a weighted digraph, called <style=\"Code\">G</style>, and and the costs (weights) for each edge, called <style=\"Code\">c</style>.",
		"Our 'search forest' is of the <style=\"Code\">disjointed sets</style> Abstract Data Type, which is a fancy way of saying a list of lists, which holds the nodes in the graph.",
		"To start with, we'll put each node into a separate list, henceforth 'tree', and store the individual lists in a big list, called <style=\"Code\">A</style>.",
		"This one should be pretty self-explanatory. We're just ordering all the edges in the graph from shortest to longest, for the next step.",
		"Now we'll grab each edge, starting smallest and getting larger, and do some stuff with it. Actually, we'll do stuff with the two nodes that it connects, <style=\"Code\">u</style> and <style=\"Code\">v</style>.",
		"Going back to our list-of-lists search forest, we check to see if node <style=\"Code\">u</style> and node <style=\"Code\">v</style> are in different trees.",
		"<style=\"Code\">u</style> and <style=\"Code\">v</style> are in different trees, so we will note this edge down as being the shortest way between the two nodes.",
		"And to finish off, we'll merge the two trees so that <style=\"Code\">u</style> and <style=\"Code\">v</style> are now in the same tree.",
		"Once we've gone through all the edges, we're done! And we can return the final, shortest-path tree with all the nodes in the graph."
	};
}
