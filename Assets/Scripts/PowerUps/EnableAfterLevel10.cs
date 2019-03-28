﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterLevel10 : MonoBehaviour
{

    public Storage Storage;
    private void Start()
    {
        if (Storage.AmountPlayed < 10)
        {
            gameObject.SetActive(false);
        }
    }
}
