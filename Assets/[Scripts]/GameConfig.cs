using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Singleton */
[System.Serializable]
public class GameConfig
{
    // Instance Members
    public int PreviousScene { get; set; }


    // Step 1. private Static instance 
    private static GameConfig instance;

    // Step 2. make constructor private
    private GameConfig()
    {
        PreviousScene = 0;
    }

    // Step 3. public static Instance method (creational method)
    public static GameConfig Instance()
    {
        return instance ??= new GameConfig();
    }
}   
