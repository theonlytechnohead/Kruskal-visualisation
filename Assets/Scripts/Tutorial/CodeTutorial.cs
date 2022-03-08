using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CodeTutorial : MonoBehaviour {

    public CanvasScaler canvas;

    public GameObject nextPanel;
    public GameObject tutorialPanel;
    public GameObject codePanel;
    public GameObject pseudocodePanel;
    public GameObject graph;

    Vector2 codeStart;
    Vector2 pseudocodeStart;
    Vector2 graphStart;

    bool nextToStep = false;
    bool graphIn = false;
    bool codeIn = false;
    bool pseudocodeIn = false;

    Color background;
    Color focusBackground;

    enum states {
        init,
        text,
        pseudocode,
        code,
        fadeBack,
        main
    }

    states state = states.init;

    List<string> text = new List<string>() {
        "", // Leave empty to fill in with current text
        "Suppose you have a bunch of different places you want to get to, all different distances from each other, and you want to get to all of them via the shortest distance. How could you work it out?",
        "You could calculate every possible combination of orders of visiting them, but that quickly gets really complicated to do. Surely there mut be a better way...",
        "And there is! It's called Kruskal's algorithm, check it out right here (hover over a line to see a description)",
        "A much more detailed breakdown (in C# code) is also up here - don't forget to hover over it to read more",
        "Have fun!"
    };

    int textIndex = 0;

    // Start is called before the first frame update
    void Start () {
        background = GetComponent<Camera>().backgroundColor;
        focusBackground = new Color(background.r * 0.2f, background.g * 0.2f, background.b * 0.2f);

        graphStart = graph.transform.position;
        codeStart = codePanel.GetComponent<RectTransform>().anchoredPosition;
        pseudocodeStart = pseudocodePanel.GetComponent<RectTransform>().anchoredPosition;

        codePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-codePanel.GetComponent<RectTransform>().sizeDelta.x, 0f);
        pseudocodePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-pseudocodePanel.GetComponent<RectTransform>().sizeDelta.x, 0f);
        text[0] = tutorialPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }

    // Update is called once per frame
    void Update () {
        tutorialPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text[textIndex];
        if (graphIn) {
            Vector2 graphPosition = graph.transform.position;
            graphPosition.x = Mathf.Lerp(graphPosition.x, 0f, Time.deltaTime);
            graph.transform.position = graphPosition;

            Vector2 tutorialPosition = tutorialPanel.GetComponent<RectTransform>().anchoredPosition;
            tutorialPosition.x = Mathf.Lerp(tutorialPosition.x, -canvas.referenceResolution.x / 4f, Time.deltaTime);
            tutorialPanel.GetComponent<RectTransform>().anchoredPosition = tutorialPosition;

            Vector2 nextPosition = nextPanel.GetComponent<RectTransform>().anchoredPosition;
            nextPosition.x = Mathf.Lerp(nextPosition.x, -canvas.referenceResolution.x / 4f, Time.deltaTime);
            nextPanel.GetComponent<RectTransform>().anchoredPosition = nextPosition;
        }
        if (pseudocodeIn) {
            Vector2 pseudocodePosition = pseudocodePanel.GetComponent<RectTransform>().anchoredPosition;
            pseudocodePosition.x = Mathf.Lerp(pseudocodePosition.x, pseudocodeStart.x, Time.deltaTime);
            pseudocodePanel.GetComponent<RectTransform>().anchoredPosition = pseudocodePosition;
        }
        if (pseudocodeIn && state < states.code) {
            Vector2 nextPosition = nextPanel.GetComponent<RectTransform>().anchoredPosition;
            nextPosition.x = Mathf.Lerp(nextPosition.x, canvas.referenceResolution.x / 4f, Time.deltaTime);
            nextPanel.GetComponent<RectTransform>().anchoredPosition = nextPosition;

            Vector2 graphPosition = graph.transform.position;
            graphPosition.x = Mathf.Lerp(graphPosition.x, graphStart.x, Time.deltaTime);
            graph.transform.position = graphPosition;
        }
        if (pseudocodeIn && state < states.fadeBack) {
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, focusBackground, Time.deltaTime);
        }
        if (nextToStep) {
            Vector2 nextPosition = nextPanel.GetComponent<RectTransform>().anchoredPosition;
            nextPosition.x = Mathf.Lerp(nextPosition.x, nextPanel.GetComponent<RectTransform>().sizeDelta.x / 2f, Time.deltaTime);
            nextPosition.y = Mathf.Lerp(nextPosition.y, 0f, Time.deltaTime);
            nextPanel.GetComponent<RectTransform>().anchoredPosition = nextPosition;
        }
        if (codeIn) {
            Vector2 codePosition = codePanel.GetComponent<RectTransform>().anchoredPosition;
            codePosition.x = Mathf.Lerp(codePosition.x, codeStart.x, Time.deltaTime);
            codePanel.GetComponent<RectTransform>().anchoredPosition = codePosition;

            Vector2 tutorialPosition = tutorialPanel.GetComponent<RectTransform>().anchoredPosition;
            tutorialPosition.x = Mathf.Lerp(tutorialPosition.x, canvas.referenceResolution.x / 4f, Time.deltaTime);
            tutorialPanel.GetComponent<RectTransform>().anchoredPosition = tutorialPosition;
        }
        if (state == states.fadeBack) {
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, background, 2f * Time.deltaTime);
            Vector2 tutorialPosition = tutorialPanel.GetComponent<RectTransform>().anchoredPosition;
            tutorialPosition.x = Mathf.Lerp(tutorialPosition.x, canvas.referenceResolution.x * 1.25f, Time.deltaTime);
            tutorialPanel.GetComponent<RectTransform>().anchoredPosition = tutorialPosition;
            if (Mathf.Abs(background.a - GetComponent<Camera>().backgroundColor.a) < 0.01f) {
                State();
            }
        }
    }

    public void State () {
        state++;
        textIndex++;
        EventSystem.current.SetSelectedGameObject(null);
        switch (state) {
            case states.text:
                graphIn = true;
                if (textIndex < 2)
                    state = states.init;
                switch (textIndex) {
                    case 1:
                        // spawn nodes
                        SpawnNodes();
                        break;
                    case 2:
                        LinkNodes();
                        // do Kruskal?
                        // flash nodes?
                        // flash edges?
                        break;
                    default:
                        break;
                }
                break;
            case states.pseudocode:
                pseudocodeIn = true;
                graphIn = false;
                break;
            case states.code:
                nextToStep = true;
                codeIn = true;
                break;
            case states.fadeBack:
                nextPanel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                nextPanel.transform.GetChild(0).GetComponent<Button>().interactable = false;
                break;
            case states.main:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    void SpawnNodes () {
        GraphVisualiser graphVisualiser = graph.GetComponent<GraphVisualiser>();
        graphVisualiser.AddNode(0);
        graphVisualiser.AddNode(1);
        graphVisualiser.AddNode(2);
        graphVisualiser.AddNode(3);
        graphVisualiser.DisplayGraph();
    }

    void LinkNodes() {
        GraphVisualiser graphVisualiser = graph.GetComponent<GraphVisualiser>();
        graphVisualiser.AddEdge(0, 1, 4);
        graphVisualiser.AddEdge(0, 2, 1);
        graphVisualiser.AddEdge(1, 2, 2);
        graphVisualiser.AddEdge(2, 3, 2);
        graphVisualiser.AddEdge(1, 3, 1);
        graphVisualiser.AddEdge(0, 3, 3);
        graphVisualiser.DisplayGraph();
    }
}
