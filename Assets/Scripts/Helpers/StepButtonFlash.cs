using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepButtonFlash : MonoBehaviour {

    Button button;

    Color normalColour;
    Color highlightedColour;
    Color pressedColour;
    public Color flashColour;

    float flashSpeed = 1.15f;
    float progress;
    bool increasing = true;

    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        normalColour = button.colors.normalColor;
        highlightedColour = button.colors.highlightedColor;
        pressedColour = button.colors.pressedColor;
    }

    void Update () {
        if (progress >= 1f)
            increasing = false;
        else if (progress <= 0f)
            increasing = true;
        if (increasing)
            progress += flashSpeed * Time.deltaTime;
        else
            progress -= flashSpeed * Time.deltaTime;
        ColorBlock colours = button.colors;
        colours.normalColor =  Color.Lerp(normalColour, flashColour, progress);
        Color alphaUp = flashColour;
        alphaUp.a = 0.5f;
        colours.highlightedColor = Color.Lerp(highlightedColour, alphaUp, progress);
        colours.pressedColor = Color.Lerp(pressedColour, alphaUp, progress);
        button.colors = colours;
    }

    void Click () {
        button.onClick.RemoveListener(Click);
        ColorBlock colours = button.colors;
        colours.normalColor = normalColour;
        colours.highlightedColor = highlightedColour;
        colours.pressedColor = pressedColour;
        button.colors = colours;
        Destroy(this);
    }
}
