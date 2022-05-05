using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WB.Pool
{

    /// <summary>
    /// MonoBehaviour用のプール本体
    /// </summary>
    internal class MonoPool<T> : Pool<T> where T:IPoolObject
    {
        /// <summary>
        /// 未使用のGameObjectはParentの子オブジェクトとする
        /// </summary>
        private Transform Parent { get; }
        
        /// <summary>
        /// コンストラクタ
        /// プール生成とともに、IPoolObjectの配置位置を定める
        /// </summary>
        /// <param name="number"></param>
        /// <param name="getNewInstance"></param>
        /// <param name="parent"></param>
        internal MonoPool(int number, Func<T> getNewInstance,Transform parent) : base(number, getNewInstance)
        {
            //未使用のIPoolObjectの配置場所を定める
            Parent = parent;
        }

        /// <summary>
        /// プールの中から
        /// インスタンスを一つ取り出す
        /// この時取り出したオブジェクトをActiveにする
        /// </summary>
        /// <returns></returns>
        public override T Rent()
        {
            IPoolObject monoObj = base.Rent();
            ((MonoBehaviour)monoObj).gameObject.SetActive(true);
            return (T)monoObj;
        }

        /// <summary>
        /// インスタンスをプールに返却する
        ///
        /// このとき親オブジェクトをParentに設定し、
        /// 非アクティブにする
        /// </summary>
        /// <param name="monoObj">返却するオブジェクト</param>
        public override void Return(T monoObj)
        {
            ((MonoBehaviour)(IPoolObject)monoObj).transform.SetParent(Parent);
            ((MonoBehaviour)(IPoolObject)monoObj).gameObject.SetActive(false);
            base.Return(monoObj);
        }
        
        /// <summary>
        /// 新しくインスタンス化する
        /// </summary>
        /// <param name="id">id(何番目のインスタンスか)</param>
        protected override T InstantiatePoolObj(int id)
        {
            T instance = base.InstantiatePoolObj(id);
            
            //オブジェクトの名前を適当に設定
            ((MonoBehaviour)(IPoolObject)instance).gameObject.name = instance.GetType() + id.ToString();

            return instance;
        }
    }
}
