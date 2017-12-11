using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init : MonoBehaviour {

    public int points;
    public int add;
    public int mul;
    public int min;
    public int div;
    public int[] lev;


    private void Awake()
    {
        points = PlayerPrefs.GetInt("points");
        add = PlayerPrefs.GetInt("addL");
        min = PlayerPrefs.GetInt("minL");
        mul = PlayerPrefs.GetInt("mulL");
        div = PlayerPrefs.GetInt("divL");
        lev = new int[7];
        for(int i = 1; i <= 7; i++)
        {
            lev[i - 1] = PlayerPrefs.GetInt("Level" + i);
        }
        
    }
    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("points", points);//points);
        PlayerPrefs.SetInt("addL", add);
        PlayerPrefs.SetInt("minL", min);
        PlayerPrefs.SetInt("mulL", mul);
        PlayerPrefs.SetInt("divL", div);
        for (int i = 1; i <= 7; i++)
        {
            PlayerPrefs.SetInt("Level" + i, lev[i - 1]);
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
