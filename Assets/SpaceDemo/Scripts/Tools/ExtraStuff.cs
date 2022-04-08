using System.Collections;
using System.Collections.Generic;
using System;

namespace SpaceDemo
{
    public enum PlanetType { Industrial, Research, Farming, Administrative, Resort };

    [Serializable]
    public class StoredGood
    {
        public TradeGood goodData;
        public int amountInStack;
        public int actualPrice;

        public StoredGood(TradeGood data, int initialAmount)
        {
            goodData = data;
            actualPrice = data.basePrice;
            amountInStack = 0;
            Add(initialAmount);
        }

        public void SetPrice(int newPrice)
        {
            actualPrice = newPrice;
        }

        /// <summary>
        /// Adds specified amount to the stored good.
        /// </summary>
        public void Add(int amount)
        {            
           amountInStack += amount;
        }

        /// <summary>
        /// Substracts specified amount from the stored good.
        /// </summary>
        /// <returns>If attempted to substract below 0, returns overflow, otherwise 0.</returns>
        public int Substract(int amount)
        {
            int overflow = amountInStack - amount;
            amountInStack += amount;
            if (amountInStack < 0)
            {
                amountInStack = 0;
            }
            return overflow < 0 ? overflow : 0;
        }

        public override string ToString()
        {
            return amountInStack.ToString() + " " + goodData.unitName + " of " + goodData.displayName;
        }
    }
}