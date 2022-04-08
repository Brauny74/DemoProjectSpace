using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class Storage : MonoBehaviour
    {
        public List<StoredGood> contents;

        public void Add(TradeGood good, int amount)
        {
            if (amount > 0)
            {
                AddAmount(good, amount);
            }
            else {
                SubstractAmount(good, amount);
            }            
        }

        private void SubstractAmount(TradeGood good, int amount)
        {
            int index = FindGoodById(good.goodID);
            if (index < 0)
            {
                return;
            }
            else
            {
                contents[index].Substract(amount);
                if (contents[index].amountInStack <= 0)
                {
                    contents.RemoveAt(index);
                }
            }
        }

        private void AddAmount(TradeGood good, int amount)
        {
            int index = FindGoodById(good.goodID);
            if (index < 0)
            {               
                contents.Add(new StoredGood(good, amount));
            }
            else
            {
                contents[index].Add(amount);
            }
        }



        private int FindGoodById(string id)
        {
            for(int i = 0; i < contents.Count; i++)
            {
                if (contents[i].goodData.goodID == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public void UpdateTradePanel(string tradeName, int money)
        {
            GUIManager.Instance.ShowTradePanel(true, contents, tradeName, money);
        }

        public void Clear()
        {
            contents.Clear();
        }
    }
}