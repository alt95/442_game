using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init : MonoBehaviour {

    public int points;
    public int add;
    public int mul;
    public int min;
    public int div;

    private void Awake()
    {
        points = PlayerPrefs.GetInt("points");
        add = PlayerPrefs.GetInt("addL");
        min = PlayerPrefs.GetInt("minL");
        mul = PlayerPrefs.GetInt("mulL");
        div = PlayerPrefs.GetInt("divL");
    }
    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("addL", add);
        PlayerPrefs.SetInt("minL", min);
        PlayerPrefs.SetInt("mulL", mul);
        PlayerPrefs.SetInt("divL", div);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
