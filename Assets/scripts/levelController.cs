using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelController : MonoBehaviour {
    public Button[] levels = new Button[10];

    private void Awake()
    {
        int maxCompleted = 0;

        for(int i = 1; i <= 7; i++)
        {
            int checkComp = PlayerPrefs.GetInt("Level" + (i).ToString());
            if(checkComp == 1)
            {
                maxCompleted = i;
                Debug.Log(i);
                Button b = levels[i-1].GetComponent<Button>();
                ColorBlock cb = b.colors;
                cb.normalColor = Color.green;
                b.colors = cb;
            }
            else
            {
                levels[i-1].interactable = false;
            }

            levels[maxCompleted].interactable = true;
            ColorBlock blk = levels[maxCompleted].colors;
            blk.normalColor = Color.white;
            levels[maxCompleted].colors = blk;
        }
    }
}
