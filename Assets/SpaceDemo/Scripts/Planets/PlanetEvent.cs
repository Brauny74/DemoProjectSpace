using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [CreateAssetMenu(fileName = "PlanetEvent", menuName = "SpaceDemo/PlanetEvent", order = 1)]
    public class PlanetEvent : ScriptableObject
    {
        public string EventName;
        [Tooltip("The duration of the event in days.")]
        public int Duration;

        [Serializable]
        public class EventCoeff
        {
            public TradeGood good;
            public float priceCoeff;                 
        }
        public List<EventCoeff> coeffs;
        [Tooltip("This is used in randomizing events.")]
        public float randomWeight;

        public float GetCoeffForGood(TradeGood good)
        {
            foreach (EventCoeff eC in coeffs)
            {
                if (good.goodID == eC.good.goodID)
                {
                    return eC.priceCoeff;
                }
            }
            return 1;
        }
    }
}