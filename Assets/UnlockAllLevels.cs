using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{
    public int lastLevelIndex;


    public void UnlockAll()
    {
        for (int i = 2; i <= lastLevelIndex; i++)
        {
            PlayerPrefs.SetInt("levelAt", i);
        }
    }
}
