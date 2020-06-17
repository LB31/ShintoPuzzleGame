﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class God
{
    public int id;
    public string godName;
    public string puzzleText;
    public string puzzleAnswer;
    public string[] dialogs;
    
}

[System.Serializable]
public class Gods2
{
    public God[] gods;

}
