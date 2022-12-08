using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadLevelFromDataFile : MonoBehaviour
{
    public TextAsset dataFile;


    void Start()
    {
        string[] data = dataFile.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int size = data.Length / 9 - 1;
        for(int i = 0; i < size; i++)
        {
            string dataRow = "";
            for (int j = 0; j < 9; j++)
            {
                dataRow += data[i * 9 + j];
            }
            Debug.Log(dataRow);
        }
    }
}

