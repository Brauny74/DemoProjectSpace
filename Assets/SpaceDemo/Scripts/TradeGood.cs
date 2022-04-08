using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [CreateAssetMenu(fileName = "TradeGood", menuName = "SpaceDemo/TradeGood", order = 0)]
    public class TradeGood : ScriptableObject
    {
        [System.Serializable]
        public class CoeffForType
        {
            public PlanetType type;
            public float coeff;
        }

        [System.Serializable]
        public class DailyProductionByType
        {
            public PlanetType type;
            public int minProduction;
            public int maxProduction;
        }

        public string goodID;
        public string displayName;
        public Sprite icon;
        public int basePrice;
        public string unitName;
        [Tooltip("The good prices are multiplied by this coefficent. Only picks first one in the list, so don't add twice.")]
        public List<CoeffForType> coeffs;
        [Tooltip("How many goods the planet produces per day. Only picks first one in the list, so don't add twice.")]
        public List<DailyProductionByType> productions;

        /// <summary>
        /// Returns the base price of the good depending on the planet type
        /// </summary>
        /// <param name="pt">Enum for planet type</param>
        /// <returns>The price with the coeffs applied</returns>
        public int GetPriceForType(PlanetType pt)
        {
            foreach (CoeffForType c in coeffs)
            {
                if (c.type == pt)
                {
                    return Mathf.RoundToInt(basePrice * c.coeff);
                }
            }
            return basePrice;
        }

        /// <summary>
        /// Returns a random amount of goods the planet might produce (or consume, if the numbers are negative) per day
        /// </summary>
        /// <param name="pt">Enum for planet type</param>
        /// <returns>The result of the daily production. 0, if the planet type doesn't produce or consume the type</returns>
        public int GetProductionAmount(PlanetType pt)
        {
            foreach (DailyProductionByType p in productions)
            {
                if (p.type == pt)
                {
                    return Random.Range(p.minProduction, p.maxProduction+1);
                }
            }
            return 0;
        }

        public override string ToString()
        {
            return goodID + ": " + "1 " + unitName + " of " + displayName;
        }
    }
}