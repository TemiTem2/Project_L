using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public int expToNextLevel=100;
    public int skillPoints;

    public StatPoint statPoint = new StatPoint();

    public static PlayerStatManager instance = null;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }
    }

}
