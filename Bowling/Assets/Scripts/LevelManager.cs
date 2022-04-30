using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public ball Ball;
    public int level;
    public Animator canvas;
    public Text levelText;


    void Awake()
    {


        level = PlayerPrefs.GetInt("level");
        levelText.text = "Level " + (level + 1);
        level = level % transform.childCount;
        transform.GetChild(level).gameObject.SetActive(true);




    }

    void Update()
    {

    }

    public void play()
    {
        Ball.enabled = true;
        canvas.SetTrigger("start");

    }

    public void fail()
    {
        canvas.SetTrigger("lose");
    }

    public void failRestart()
    {
        Application.LoadLevel(0);
    }

    public void win()
    {
        canvas.SetTrigger("win");
        PlayerPrefs.SetInt("tutorial", 1);
    }
    public void winRestart()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        Application.LoadLevel(0);
    }
}