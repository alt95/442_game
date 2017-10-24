using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battle : MonoBehaviour {
    public Slider myhp;
    public Slider enemyhp;

    public Button[] operators;

    public CanvasGroup attack;
    public CanvasGroup formulaGroup;
    public CanvasGroup question;
    public CanvasGroup endlevel;

    public Button signBtn;
    public Button attackBtn;
    public Button defendBtn;
    public Button qstBtn;

    public robot rob;
    public Animator robotAnimator;
    public alien ali;
    public Animator alienAnimator;

    private string currOperator;

    public InputField input1;
    public InputField input2;
    public InputField input3;

    public InputField num1;
    public InputField num2;
    public InputField num3;

    public Text status;
    public Text points;

    bool player1 = true;

    void Awake()
    {
        robotAnimator = rob.GetComponent<Animator>();
        alienAnimator = ali.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        //Listener for attack phase
        Button[] opt = new Button[4];
        for(int i = 0; i < operators.Length; i++)
        {
            opt[i] = operators[i].GetComponent<Button>();
            string name = opt[i].name;
            opt[i].onClick.AddListener((() => formula(name)));
        }
        //Listener for formula phase
        Button launchAttack = attackBtn.GetComponent<Button>();
        launchAttack.onClick.AddListener(playerMove);

        Button defendAttack = defendBtn.GetComponent<Button>();
        defendAttack.onClick.AddListener(answerQ);

        points.text = "Points: " + PlayerPrefs.GetInt("points").ToString();
	}
	
	// Update is called once per frame
	void Update () {

	}
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }


    void playerMove()
    {
        bool answer = false;
        //ADD CHECK FOR THIS
        int in1 = Int32.Parse(input1.text);
        int in2 = Int32.Parse(input2.text);
        int in3 = Int32.Parse(input3.text);
        
        //--Add check here to show warning message if not filled fields--//

        if (currOperator.Equals("add"))
        {
            if (in1 + in2 == in3)
                answer = true;
        }
        if (currOperator.Equals("minus"))
        {
            if (in1 - in2 == in3)
                answer = true;
        }
        if (currOperator.Equals("multiply"))
        {
            if (in1 * in2 == in3)
                answer = true;
        }
        if (currOperator.Equals("divide"))
        {
            if(in2 == 0)
            {
                answer = false;
            } else
            {
                if (in1 / in2 == in3)
                    answer = true;
            }
        }
        if (answer)
        {
            float number = UnityEngine.Random.Range(0.3f, 0.4f);
            robotAnimator.Play("robot_shoot");
            if(PlayerPrefs.GetInt("minL") > 1 && currOperator == "minus")
            {
                enemyhp.value = 0.0f;
            } else
            {
                enemyhp.value = enemyhp.value - number;
            }
            if (checkEnemyDead())
                alienAnimator.Play("alien_dead");
            else
                alienAnimator.Play("alien_hurt");
        }
        else
        {
            float number = UnityEngine.Random.Range(0.1f, 0.2f);
            myhp.value = myhp.value - number;
            if (checkDead())
                robotAnimator.Play("robot_dead");
            else
                robotAnimator.Play("robot_hurt");
        }

        hideFormula();

        if(!checkDead() && !checkEnemyDead())
        {
            Invoke("enemyTurn", 2);
        } else
        {
            endLevel();
        }
    }

    void enemyTurn()
    {
        //choose operator
        float oper = UnityEngine.Random.Range(0.0f, 0.75f);
        if (oper < 0.25f)
        {
            currOperator = "add";
        }
        else if (oper >= 0.25f && oper < 0.5f)
        {
            currOperator = "minus";
        }
        else {
            currOperator = "multiply";
        }
            

        //Choose 2 numbers
        int num1 = UnityEngine.Random.Range(1, 11);
        int num2 = UnityEngine.Random.Range(1, 11);

        showQuestionFrame(num1, num2);

        alienAnimator.Play("alien_shoot");

    }

    void answerQ()
    {
        bool answer = false;
        int n1 = Int32.Parse(num1.text);
        int n2 = Int32.Parse(num2.text);
        int n3 = Int32.Parse(num3.text);

        if (currOperator.Equals("add"))
        {
            if (n1 + n2 == n3)
                answer = true;
        }
        if (currOperator.Equals("minus"))
        {
            if (n1 - n2 == n3)
                answer = true;
        }
        if (currOperator.Equals("multiply"))
        {
            if (n1 * n2 == n3)
                answer = true;
        }

        if (answer)
        {

        }
        else
        {
            float number = UnityEngine.Random.Range(0.2f, 0.3f);
            myhp.value = myhp.value - number;
            if (checkDead())
                robotAnimator.Play("robot_dead");
            else
                robotAnimator.Play("robot_hurt");
        }

        hideQuestion();
        num3.text = "";
        if (!checkDead() && !checkEnemyDead())
        {
            Invoke("resetTurn", 2);
        }
        else
        {
            endLevel();
        }
    }

    void endLevel()
    {
        //back to level selection screen
        if(checkDead())
        {
            status.text = "You have lost this level! Try again!";
        } else if(checkEnemyDead())
        {
            int i = PlayerPrefs.GetInt("points");
            int j = i + 1000;

            status.text = "You have saved this location! Good job! Total Points Earned " + j.ToString();
            points.text = "Points: " + j.ToString();
            PlayerPrefs.SetInt("points", j);
        } else
        {
            status.text = "It's a tie???! Try again!";
        }

        endlevel.alpha = 1f;
        endlevel.blocksRaycasts = true;

    }

    void delay()
    {

    }

    void showQuestionFrame(int a, int b)
    {
        if (a > b)
        {
            num1.text = a.ToString();
            num2.text = b.ToString();
        }
        else
        {
            num1.text = b.ToString();
            num2.text = a.ToString();
        }
        if (currOperator.Equals("add"))
        {
            qstBtn.GetComponentInChildren<Text>().text = "+";
        }

        if (currOperator.Equals("minus"))
        {
            qstBtn.GetComponentInChildren<Text>().text = "-";
        }

        if (currOperator.Equals("multiply"))
        {
            qstBtn.GetComponentInChildren<Text>().text = "x";
        }

        showQuestion();
    }
    void resetTurn()
    {
        input1.text = "";
        input2.text = "";
        input3.text = "";
        showAttack();
    }

    bool checkDead()
    {
        if (myhp.value == 0.0f)
            return true;
        else
            return false;
    }

    bool checkEnemyDead()
    {
        if (enemyhp.value == 0.0f)
            return true;
        else
            return false;
    }

    void formula(string type)
    {
        Debug.Log(type);
        currOperator = type;
        hideAttack();
        showFormula();

        if(type.Equals("add"))
        {
            changeSignBtn("+");
        }

        if (type.Equals("minus"))
        {
            changeSignBtn("-");
        }

        if (type.Equals("multiply"))
        {
            changeSignBtn("x");
        }

        if (type.Equals("divide"))
        {
            changeSignBtn("÷");
        }
    }

    void changeSignBtn(string t)
    {
        signBtn.GetComponentInChildren<Text>().text = t;
    }

    void showAttack()
    {
        attack.alpha = 1f;
        attack.blocksRaycasts = true;
    }

    void hideAttack()
    {
        attack.alpha = 0;
        attack.blocksRaycasts = false;
    }

    void showFormula()
    {
        formulaGroup.alpha = 1f;
        formulaGroup.blocksRaycasts = true;
    }

    void hideFormula()
    {
        formulaGroup.alpha = 0;
        formulaGroup.blocksRaycasts = false;
    }

    void showQuestion()
    {
        question.alpha = 1f;
        question.blocksRaycasts = true;
    }

    void hideQuestion()
    {
        question.alpha = 0;
        question.blocksRaycasts = false;
    }

    void player1Turn()
    {

    }

    void aiTurn()
    {

    }
}
