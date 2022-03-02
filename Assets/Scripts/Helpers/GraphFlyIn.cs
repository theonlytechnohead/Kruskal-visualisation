using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphFlyIn : MonoBehaviour {
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start () {
        startPosition = transform.position;
        transform.position = new Vector2(Camera.main.orthographicSize * 2f, 0);
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector2(Mathf.Lerp(transform.position.x, startPosition.x, Time.deltaTime), 0);
        if (transform.position.x < 0.01f) {
            transform.position = startPosition;
            Destroy(this);
        }
    }
}
