using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Pool
{
    /// <summary>
    /// オブジェクトをプールに関するAPIを提供するクラス
    /// </summary>
    public static class WBPool
    {
        /// <summary>
        /// 型Tのインスタンスを保持するプールを生成し取得する
        /// </summary>
        /// <param name="number"></param>
        /// <param name="getNewInstance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Pool<T> MakePool<T>(int number,Func<T> getNewInstance) where T : class, IPoolObject
        {
            return PoolManager.I.MakePool(number, getNewInstance);
        }

    }
}
