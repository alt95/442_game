using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alien : MonoBehaviour {
    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("w"))
        {
            myAnimator.Play("alien_shoot");
        }
        if(Input.GetKeyDown("space"))
        {
            myAnimator.Play("alien_hurt");
        }
        if(Input.GetKeyDown("d"))
        {
            myAnimator.Play("alien_dead");
        }

    }
}
