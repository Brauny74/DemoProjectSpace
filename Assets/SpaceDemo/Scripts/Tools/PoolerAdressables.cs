using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SpaceDemo
{
    public class PoolerAdressables : BasePooler
    {
        public string objectToPoolAddress;

        protected virtual void Start()
        {
            LoadObjectToPool();
        }

        private void LoadObjectToPool()
        {
            Addressables.LoadAssetAsync<GameObject>(objectToPoolAddress).Completed += OnLoadDone;
            GameManager.Instance.AmountOfObjectsToLoad += 1;
        }

        private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            try
            {
                _objectToPool = obj.Result;
                FillObjectPool();
                GameManager.Instance.AmountOfObjectsToLoad -= 1;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}