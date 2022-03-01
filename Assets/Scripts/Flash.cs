using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

    Color startColour;
    Material startMaterial;
    public Material flashMaterial;
    Image image;

    float progress = 0f;

    // Start is called before the first frame update
    void Start () {
        image = GetComponent<Image>();
        startColour = image.material.GetColor("_TintColor");
        startMaterial = image.material;
        flashMaterial = new Material(startMaterial);
        flashMaterial.name = "FlashBlur";
        image.material = flashMaterial;
        Color flashColour = startColour;
        flashColour.a = 0.8f;
        image.material.SetColor("_TintColor", flashColour);
        //print(image.material.GetColor("_TintColor"));
    }

    // Update is called once per frame
    void Update () {
        if (progress < 0.015f) {
            progress += 0.01f * Time.deltaTime;
            //print(progress);
        } else {
            image.material = startMaterial;
            Destroy(this);
        }
        Color newColour = Color.Lerp(image.material.GetColor("_TintColor"), startColour, progress);
        image.material.SetColor("_TintColor", newColour);
        //print(image.material.GetColor("_TintColor"));
    }
}
