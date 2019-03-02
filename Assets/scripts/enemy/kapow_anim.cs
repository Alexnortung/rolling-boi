using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapow_anim : MonoBehaviour {

    public float kapow_anim_time = 0.5f;

	// Use this for initialization
	void Start () {
        Debug.Log(transform.position);
        StartCoroutine(disappear());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(kapow_anim_time);
        Destroy(gameObject);
    }

}
