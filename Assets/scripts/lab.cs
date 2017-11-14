using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab : MonoBehaviour {
    public Text addL;
    public Text minL;
    public Text mulL;
    public Text divL;
    public Text points;

    public Button add;
    public Button min;
    public Button mul;
    public Button div;

    public analytics an;

    // Use this for initialization
    void Start () {
		addL.text = "Level: " + PlayerPrefs.GetInt("addL").ToString();
        minL.text = "Level: " + PlayerPrefs.GetInt("minL").ToString();
        mulL.text = "Level: " + PlayerPrefs.GetInt("mulL").ToString();
        divL.text = "Level: " + PlayerPrefs.GetInt("divL").ToString();

        int getPoints = PlayerPrefs.GetInt("points");

        points.text = "Points: " + getPoints.ToString();

        if(checkPoints())
        {
            add.interactable = true;
            min.interactable = true;
            mul.interactable = true;
            div.interactable = true;
            Button a = add.GetComponent<Button>();
            a.onClick.AddListener(levelAdd);
            Button b = min.GetComponent<Button>();
            b.onClick.AddListener(levelMin);
            Button c = mul.GetComponent<Button>();
            c.onClick.AddListener(levelMul);
            Button d = div.GetComponent<Button>();
            d.onClick.AddListener(levelDiv);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool checkPoints()
    {
        if (PlayerPrefs.GetInt("points") < 1000)
        {
            add.interactable = false;
            min.interactable = false;
            mul.interactable = false;
            div.interactable = false;
            return false;
        }
        else
        {
            return true;
        }
    }

    void levelAdd()
    {
        an.addAbility("plus");

        PlayerPrefs.SetInt("addL", PlayerPrefs.GetInt("addL") + 1);
        addL.text = "Level: " + PlayerPrefs.GetInt("addL").ToString();

        int getPoints = PlayerPrefs.GetInt("points");
        PlayerPrefs.SetInt("points", getPoints - 1000);
        points.text = "Points: " + PlayerPrefs.GetInt("points").ToString();

        checkPoints();
    }

    void levelMin()
    {
        an.addAbility("minus");

        PlayerPrefs.SetInt("minL", PlayerPrefs.GetInt("minL") + 1);
        minL.text = "Level: " + PlayerPrefs.GetInt("minL").ToString();

        int getPoints = PlayerPrefs.GetInt("points");
        PlayerPrefs.SetInt("points", getPoints - 1000);
        points.text = "Points: " + PlayerPrefs.GetInt("points").ToString();

        checkPoints();
    }
    
    void levelMul()
    {
        an.addAbility("mult");

        PlayerPrefs.SetInt("mulL", PlayerPrefs.GetInt("mulL") + 1);
        mulL.text = "Level: " + PlayerPrefs.GetInt("mulL").ToString();

        int getPoints = PlayerPrefs.GetInt("points");
        PlayerPrefs.SetInt("points", getPoints - 1000);
        points.text = "Points: " + PlayerPrefs.GetInt("points").ToString();

        checkPoints();
    }

    void levelDiv()
    {
        an.addAbility("divide");

        PlayerPrefs.SetInt("divL", PlayerPrefs.GetInt("divL") + 1);
        divL.text = "Level: " + PlayerPrefs.GetInt("divL").ToString();

        int getPoints = PlayerPrefs.GetInt("points");
        PlayerPrefs.SetInt("points", getPoints - 1000);
        points.text = "Points: " + PlayerPrefs.GetInt("points").ToString();

        checkPoints();
    }
}
