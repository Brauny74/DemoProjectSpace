using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class GoodsList : MonoBehaviour
    {
        public GoodViewUI goodPrefab;
        public RectTransform listTransform;

        public void ClearList()
        {
            for (int i = 0; i < listTransform.childCount; i++)
            {
                Destroy(listTransform.GetChild(i).gameObject);
            }
        }

        public void SetList(List<StoredGood> goods, bool isPlayer = false)
        {
            foreach (StoredGood good in goods)
            {
                GoodViewUI newGood = Instantiate(goodPrefab, listTransform);
                newGood.SetGood(good, good.actualPrice, good.amountInStack, isPlayer);
            }
        }
    }
}