using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SpaceDemo
{
    public class AddressableReplacer : MonoBehaviour
    {
        public string objectAddress;

        public void Start()
        {
            Addressables.LoadAssetAsync<GameObject>(objectAddress).Completed += OnLoadDone;
            GameManager.Instance.AmountOfObjectsToLoad += 1;
        }

        private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            try
            {
                GameObject objectToReplace = Instantiate(obj.Result);
                objectToReplace.transform.position = transform.position;
                objectToReplace.transform.parent = transform.parent;
                GameManager.Instance.AmountOfObjectsToLoad -= 1;
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}