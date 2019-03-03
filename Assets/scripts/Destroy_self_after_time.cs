using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_self_after_time : MonoBehaviour {

    public float delay = 2f;

	// Use this for initialization
	void Start () {
        StartCoroutine(destroySelf());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
