using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

    static Color? startColour = null;
    Material startMaterial;
    public Material flashMaterial;
    Image image;

    float progress = 0f;

    // Start is called before the first frame update
    void Start () {
        image = GetComponent<Image>();
        if (startColour == null) {
            startColour = image.material.GetColor("_TintColor");
        }
        startMaterial = image.material;
        flashMaterial = new Material(startMaterial);
        flashMaterial.name = "FlashBlur";
        image.material = flashMaterial;
        Color flashColour = startColour.Value;
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
            image.material.SetColor("_TintColor", startColour.Value);
            image.material = startMaterial;
            //image.material.SetColor("_TintColor", startColour);
            Destroy(this);
        }
        Color newColour = Color.Lerp(image.material.GetColor("_TintColor"), startColour.Value, progress);
        image.material.SetColor("_TintColor", newColour);
        //print(image.material.GetColor("_TintColor"));
    }

    void OnDestroy () {
        image.material.SetColor("_TintColor", startColour.Value);
        image.material = startMaterial;
    }
}
