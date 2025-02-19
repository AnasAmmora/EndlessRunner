using System.Collections;
using System.Collections.Generic;

public class GameData
{
    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameData();
            return _instance;
        }
    }

    public float LastScore { get; set; }
    public float BestScore { get; set; }

    public int LastGameCoins { get; set; }
    public int TotalCoins { get; set; }

    private GameData()
    {
        LastScore = 0;
        BestScore = 0;
        LastGameCoins = 0;
        TotalCoins = 0;
    }
}
