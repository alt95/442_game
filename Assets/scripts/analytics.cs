using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analytics : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}


    // Update is called once per frame
    void Update () {
		
	}

    public void addAbility(string ab)
    {
        string url = "http://localhost/bp/lab.php";
        WWWForm form = new WWWForm();
        form.AddField("ability", ab);
        WWW www = new WWW(url, form);
    }

    public void attackChoice(int level, string ab)
    {
        string url = "http://localhost/bp/choice.php";
        WWWForm form = new WWWForm();
        form.AddField("level", level);
        form.AddField("ability", ab);
        WWW www = new WWW(url, form);
    }

    public void answer(int level, int corr)
    {
        string url = "http://localhost/bp/def.php";
        WWWForm form = new WWWForm();
        form.AddField("level", level);
        form.AddField("corr", corr);
        WWW www = new WWW(url, form);
    }

    public void stat(int level, int win, string time)
    {
        string url = "http://localhost/bp/stat.php";
        WWWForm form = new WWWForm();
        form.AddField("level", level);
        form.AddField("win", win);
        form.AddField("time", time);
        WWW www = new WWW(url, form);
    }
}
