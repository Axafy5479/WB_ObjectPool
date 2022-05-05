using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WB.Pool;

public class PoolObj : MonoBehaviour,IPoolObject
{
    [SerializeField] private Text idText;
    private Action ReturnObjMethod { get; set; }
    public void SetId(int id)
    {
        idText.text = id.ToString();
    }
    
    public void Initialize(){ }

    public void OnClick()
    {
        ReturnObjMethod();
    }
    public void SetReturnMethod(Action returnMethod)
    {
        ReturnObjMethod = returnMethod;
    }
}
