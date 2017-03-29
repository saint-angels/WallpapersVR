using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotator : MonoBehaviour {

    public Vector3 rotationVector;


    public bool rotate;
    public bool resetTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate)
        {
            transform.Rotate(rotationVector * Time.deltaTime, Space.World);
        }

        if(resetTransform)
        {
            transform.rotation = Quaternion.identity;
            resetTransform = false;
        }
    }
}
