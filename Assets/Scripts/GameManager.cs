using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    private static GameManager _instance;

    public static GameManager Instance {
        get {
            return _instance;
        }
    }
    public int level = 1; //设置关卡
    public int food = 100;

    public List<New_Enemy> enemylist = new List<New_Enemy>();

    private bool sleepStep = true;
    void Awake() {
        _instance = this;
    }
    public void ReduceFood(int count) {
        food -= count;
    }
    public void AddFood(int count) {
        food += count;
    }
    public void OnPlayerMove() {
        if (sleepStep==true)
        {
            sleepStep = false;
        }
        else
        {
            foreach (var enemy in enemylist)
            {
                enemy.Move();
            }
            sleepStep = true;
        }
    
    }
}
