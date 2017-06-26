using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraAspectRatio : MonoBehaviour {
	public Vector2 targetAspects = new Vector2(16f, 15f);

	void Start() {
		float targetaspect = targetAspects.x / targetAspects.y;
		float windowaspect = (float)Screen.width / (float)Screen.height;
		float scaleheight = windowaspect / targetaspect;

		Camera camera = GetComponent<Camera>();

		if (scaleheight < 1.0f) {
			Rect rect = camera.rect;
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			camera.rect = rect;

		} else {
			float scalewidth = 1.0f / scaleheight;
			Rect rect = camera.rect;
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			camera.rect = rect;
		}

	}
}
