using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopGrowBehaviour : MonoBehaviour {

    public float growSpeed;
    private float currentSize;

    private void Start()
    {
        currentSize = this.transform.localScale.magnitude;
    }

    // Update is called once per frame
    void Update () {
        currentSize += growSpeed * Time.deltaTime;
        this.transform.localScale = new Vector3(1 * currentSize, 1, 1 * currentSize);
	}
}
