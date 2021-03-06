using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    /// <summary>
    /// This class stores entity's money
    /// </summary>
    public class Wallet : MonoBehaviour
    {
        public int Money;

        private void OnValidate()
        {
            if (Money < 0)
            {
                Money = Mathf.Abs(Money);
            }
        }

        public void AddMoney(int value)
        {
            Money += value;
        }
    }
}