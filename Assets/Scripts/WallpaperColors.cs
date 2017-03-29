using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperColors : MonoBehaviour {

    public Texture[] lTextures;
    public Texture[] rTextures;

    public int cubemapRotation;

    public Sprite[] wallpaperSprites;
    public Sprite[] previewSprites;
    public CollectionType collection;

    [SerializeField]
    GameObject[] selectionMarkers;


    public void SetSelected(int newIdx) {
        for (int i = 0; i < selectionMarkers.Length; i++) {
            selectionMarkers[i].SetActive(false);
        }
        selectionMarkers[newIdx].SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
