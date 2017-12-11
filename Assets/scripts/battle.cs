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
    public CanvasGroup feedbackgroup;

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
    public Text feedback;
    public Text hp;
    public Text ehp;

    public int level;

    public analytics an;

    bool player1 = true;

    public System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    void Awake()
    {
        robotAnimator = rob.GetComponent<Animator>();
        alienAnimator = ali.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start() {
        sw.Start();
        //Listener for attack phase
        Button[] opt = new Button[4];
        for (int i = 0; i < operators.Length; i++)
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
        updateHP();
    }

    // Update is called once per frame
    void Update() {

    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
    }

    void updateHP()
    {
        int fhp = (int) (myhp.value * 100);
        int fehp = (int) (enemyhp.value * 100);

        hp.text = "My Health (" + fhp + "/100)";
        ehp.text = "Enemy Health (" + fehp + "/100)";

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
            if (in2 == 0)
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
            float number = playerPower(); // UnityEngine.Random.Range(0.3f, 0.4f);

            robotAnimator.Play("robot_shoot");
            if (PlayerPrefs.GetInt("minL") > 1 && currOperator == "minus")
            {
                enemyhp.value = 0.0f;
                updateHP(); 
            } else
            {
                enemyhp.value = enemyhp.value - number;
                updateHP();
            }
            if (checkEnemyDead())
            {
                alienAnimator.Play("alien_dead");
                string lev = "Level" + level;
                PlayerPrefs.SetInt(lev, 1);
                record(1);
            }
            else
                alienAnimator.Play("alien_hurt");
        }
        else
        {
            float number = UnityEngine.Random.Range(0.1f, 0.2f);
            myhp.value = myhp.value - number;
            updateHP();
            if (checkDead())
            {
                robotAnimator.Play("robot_dead");
                record(0);
            }
            else
                robotAnimator.Play("robot_hurt");
        }

        hideFormula();

        if (!checkDead() && !checkEnemyDead())
        {
            Invoke("enemyTurn", 2);
        } else
        {
            endLevel();
        }
    }

    float playerPower()
    {
        float n = 0;
        int getLev = 0;
        if (currOperator.Equals("add"))
        {
            getLev = PlayerPrefs.GetInt("addL");
        }
        if (currOperator.Equals("minus"))
        {
            getLev = PlayerPrefs.GetInt("minL");
        }
        if (currOperator.Equals("multiply"))
        {
            getLev = PlayerPrefs.GetInt("mulL");
        }
        if (currOperator.Equals("divide"))
        {
            getLev = PlayerPrefs.GetInt("divL");
        }

        //Zero error
        getLev = getLev + 1;
        //Helps us calculate scaling of impact on level and complexity
        float ratio = (float)getLev / level;
        float baseHit = UnityEngine.Random.Range(0.05f, 0.10f);

        //Determine complexity of numbers
        int num1 = Int32.Parse(input1.text);
        int num2 = Int32.Parse(input2.text);

        int multiplier = 1;

        if(num1 > 10 && num2 > 10)
        {
            multiplier = multiplier + 1;
        }

        if(num1 > 60 || num2 > 60)
        {
            multiplier = multiplier + 1;
        }

        if(currOperator.Equals("multiply") || currOperator.Equals("divide"))
        {
            multiplier = multiplier + 1;
        }

        n = ratio * baseHit * multiplier;

        if (n > 0.5f)
            n = 0.5f;

        return n;
    }

    void enemyTurn()
    {
        //choose operator
        float oper = UnityEngine.Random.Range(0.0f, 1.0f);
        if (oper < 0.25f)
        {
            currOperator = "add";
        }
        else if (oper >= 0.25f && oper < 0.5f)
        {
            currOperator = "minus";
        }
        else if (oper >= 0.5f && oper < 0.75f) {
            currOperator = "multiply";
        }
        else
        {
            currOperator = "divide";
        }

        int levelMultiplier = 1;
        //Choose 2 numbers based on level - gets higher for add/sub and little bit higher for mult/div 

        if (currOperator.Equals("add") || currOperator.Equals("minus"))
        {
            levelMultiplier = level + 5;
        }

        if (currOperator.Equals("multiply") || currOperator.Equals("divide"))
        {
            levelMultiplier = level;
        }

        int num1 = UnityEngine.Random.Range(1 + levelMultiplier, 11 + levelMultiplier);
        int num2 = UnityEngine.Random.Range(1 + levelMultiplier, 11 + levelMultiplier);

        //Flip answer to be first number if division
        if(currOperator.Equals("divide"))
        {
            int num3 = num1 * num2;
            num1 = num3;

        }

        showQuestionFrame(num1, num2);

        alienAnimator.Play("alien_shoot");

    }

    void answerQ()
    {
        bool answer = false;
        int n1 = Int32.Parse(num1.text);
        int n2 = Int32.Parse(num2.text);
        int n3 = Int32.Parse(num3.text);
        int corrAns = 0;

        if (currOperator.Equals("add"))
        {
            if (n1 + n2 == n3)
                answer = true;

            corrAns = n1 + n2;
        }
        if (currOperator.Equals("minus"))
        {
            if (n1 - n2 == n3)
                answer = true;

            corrAns = n1 - n2;
        }
        if (currOperator.Equals("multiply"))
        {
            if (n1 * n2 == n3)
                answer = true;

            corrAns = n1 * n2;
        }
        if (currOperator.Equals("divide"))
        {
            if (n1 / n2 == n3)
                answer = true;

            corrAns = n1 / n2;
        }

        if (answer)
        {
            an.answer(level, 1);
        }
        else
        {
            //Feedback Code
            evaluateAnswer((float)corrAns, (float)n3);
            showFeedback();

            an.answer(level, 0);
            float number = UnityEngine.Random.Range(0.2f, 0.3f);
            myhp.value = myhp.value - number;
            updateHP();
            if (checkDead())
            {
                robotAnimator.Play("robot_dead");
                record(0);
            }
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

    void evaluateAnswer(float corr, float ans)
    {
        feedback.text = "";
        if(currOperator.Equals("add") || currOperator.Equals("minus"))
        {
            if((corr - 10 == ans) || (corr + 10 == ans))
            {
                feedback.text = "Looks like you made a carrying error!";
            }
            
            if((corr - 1 == ans) || (corr + 1 == ans))
            {
                feedback.text = "So close! Looks like you made a slight mistake";
            }
        }

        if(currOperator.Equals("multiply"))
        {
            if((corr * 10 == ans))
            {
                feedback.text = "Looks like you put an extra 0!";
            }
            if((corr / 10 == ans))
            {
                feedback.text = "Looks like you missed a 0!";
            }
        }

        if(currOperator.Equals("divide"))
        {
            if(corr == 1)
            {
                feedback.text = "Remember, a number divided by itself is always 1";
            }
        }

        //append correct answer too feedback.text
        feedback.text = feedback.text + " The correct answer was " + corr;
    }

    public void record(int win)
    {
        sw.Stop();
        TimeSpan ts = sw.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
        an.stat(level, win, elapsedTime);
        Debug.Log(level);
        Debug.Log(win);
        Debug.Log(elapsedTime);

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

        if (currOperator.Equals("divide"))
        {
            qstBtn.GetComponentInChildren<Text>().text = "÷";
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
            an.attackChoice(level, "plus");
            changeSignBtn("+");
        }

        if (type.Equals("minus"))
        {
            an.attackChoice(level, "minus");
            changeSignBtn("-");
        }

        if (type.Equals("multiply"))
        {
            an.attackChoice(level, "multiply");
            changeSignBtn("x");
        }

        if (type.Equals("divide"))
        {
            an.attackChoice(level, "divide");
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

    void showFeedback()
    {
        feedbackgroup.alpha = 1f;
        feedbackgroup.blocksRaycasts = true;
        Invoke("hideFeedback", 6);
    }

    void hideFeedback()
    {
        feedbackgroup.alpha = 0;
        feedbackgroup.blocksRaycasts = false;
    }


}
