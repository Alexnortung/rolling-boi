using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageScaler : MonoBehaviour {

    public Camera camera;
    public Image image;

	// Use this for initialization
	void Start () {
        float cw = camera.pixelWidth;
        float ch = camera.pixelHeight;
        float iw = image.rectTransform.rect.xMax - image.rectTransform.rect.xMin;
        float ih = image.rectTransform.rect.yMax - image.rectTransform.rect.yMin;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
