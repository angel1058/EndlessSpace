﻿
using UnityEngine;

public class InifiteStarScroll : MonoBehaviour {

    public float StarScrollSpeed = 3.0f;
	// Update is called once per frame
	void Update () {

        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 matOffset = mat.mainTextureOffset;

        matOffset.y += Time.deltaTime * StarScrollSpeed;

        mat.mainTextureOffset = matOffset;

	}
}
