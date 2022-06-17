using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Handles time that passes in-game.
public class TickManager : MonoBehaviour
{
    //Unity's Physics system calls FixedUpdate 50 times per second ( 0.02 between calls )
    //Assuming we want one tick per second...
    public float CurrentTickTime;

    //Keeps track of how many ticks have happened so far.
    public int ElapsedTicks;

    //Let's assume for now we want our round to last 600 ticks/seconds - or 10 minutes.
    public int LevelTicks = 600;

    //We call changes on this as we move along.
    private GameStateManager GameStateManager;

    public delegate void OnLevelOver();
    public static event OnLevelOver onLevelOver;

    // Use this for initialization
    void Start()
    {
        this.GameStateManager = gameObject.gameObject.GetComponent<GameStateManager>();
    }

    void FixedUpdate()
    {

        if(ElapsedTicks == LevelTicks)
        {



        }

        if(CurrentTickTime >= 1f)
        {
            GameStateManager.OnTickPassed();
            ElapsedTicks++;
            CurrentTickTime = 0f;

        }

        CurrentTickTime += Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {

    }
}