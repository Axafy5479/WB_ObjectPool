using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Pool
{
    /// <summary>
    /// 複数のプールをまとめるクラス
    /// </summary>
    internal class PoolManager : MonoBehaviour
    {
        #region Singleton

        private static PoolManager instance;
        public static PoolManager I
        {
            get
            {
                PoolManager[] instances = null;
                if (instance == null)
                {
                    instances = FindObjectsOfType<PoolManager>();
                    if (instances.Length == 0)
                    {
                        GameObject go = new GameObject("WB_PoolManager");
                        DontDestroyOnLoad(go);
                        var pool = go.AddComponent<PoolManager>();
                        return pool;
                    }
                    else if (instances.Length > 1)
                    {
                        Debug.LogError("WB_PoolManagerのインスタンスが複数存在します");
                    }
                    else
                    {
                        instance = instances[0];
                    }
                }

                return instance;
            }
        }
        #endregion


        /// <summary>
        /// 型(T)に対応するプールを作成する
        /// </summary>
        /// <param name="number">プールのサイズ</param>
        /// <param name="getNewInstance">型Tのインスタンスを生成する方法</param>
        /// <typeparam name="T">プールの対象となる型</typeparam>
        /// <exception cref="Exception">既に型Tに対応するプールが存在する場合</exception>
        internal Pool<T> MakePool<T>(int number, Func<T> getNewInstance) where T : class, IPoolObject
        {

            //型TがMonoBehaviourを継承しているか否かで、生成するプールを分ける

            Pool<T> pool = null;
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                //MonoBehaviourの場合
                
                //未使用のGameObjectがHierarchy上にばらまかれないよう、
                //親オブジェクト(のTransform)を生成しておく
                Transform parentTrn = new GameObject(typeof(T).ToString()).transform;
                
                //親オブジェクトがシーン遷移時に削除されないよう、このオブジェクト(PoolManager)の子オブジェクトに設定しておく
                //※PoolManagerはDontDestroyOnLoadを実行済み
                parentTrn.SetParent(this.transform);
                
                //MonoBehaviour用のプールをインスタンス化
                pool = new MonoPool<T>(number, getNewInstance, parentTrn);
                
            }
            else
            {
                //MonoBehaviour以外の場合、Poolをインスタンス化
                pool = new Pool<T>(number, getNewInstance);
            }

            //指定の数だけプール内にインスタンスを生成する
            pool.MakeInitialObject();

            return pool;
        }

    }
}
