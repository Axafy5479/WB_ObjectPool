using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WB.Pool;

public class PoolObj : MonoBehaviour,IPoolObject
{
    [SerializeField] private Text idText;

    public void SetId(int id)
    {
        idText.text = id.ToString();
    }
    
    public void Initialize(){ }
}
