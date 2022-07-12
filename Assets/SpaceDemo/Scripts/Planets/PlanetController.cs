using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class PlanetController : MonoBehaviour
    {
        public string planetName;
        [SerializeField]
        protected PlanetType type;

        [SerializeField]
        protected List<TradeGood> producedGoods;

        [SerializeField]
        protected List<PlanetEvent> possibleEvents;

        protected Storage _storage;
        protected int currentEventIndex;
        protected int currentEventDuration;

        public void ShowPreviewPanel()
        {
            GUIManager.Instance.ShowPlanetPreview(true, planetName, type.ToString(), possibleEvents[currentEventIndex].EventName);
        }

        public void HidePreviewPanel()
        {
            GUIManager.Instance.ShowPlanetPreview(false);
        }

        private void Awake()
        {            
            _storage = GetComponent<Storage>();
            OnDayEnd();
        }

        private void OnEnable()
        {
            TimeManager.Instance.OnDayEnd.AddListener(OnDayEnd);
        }

        private void OnDayEnd()
        {
            ProcessProduction();
            ProcessEvents();
            ProcessPrices();
        }

        private void ProcessProduction()
        {
            foreach (TradeGood good in producedGoods)
            {
                _storage.Add(good, good.GetProductionAmount(type));
            }
        }

        private void ProcessEvents()
        {
            currentEventDuration--;
            if (currentEventDuration <= 0)
            {
                ChoseEvent();
                currentEventDuration = possibleEvents[currentEventIndex].Duration;
            }
        }

        private void ChoseEvent()
        {
            float sumWeight = 0;
            foreach (PlanetEvent pE in possibleEvents)
            {
               sumWeight += pE.randomWeight;
            }
            float weight = Random.Range(0, sumWeight);
            for (int i = 0; i < possibleEvents.Count; i++)
            {
                if (weight > sumWeight)
                {
                    currentEventIndex = i;
                    return;
                }
                sumWeight -= possibleEvents[i].randomWeight;
            }
            currentEventIndex = 0;
        }

        private void ProcessPrices()
        {
            foreach (StoredGood good in _storage.contents)
            {
                good.SetPrice(Mathf.RoundToInt(good.goodData.GetPriceForType(type) * possibleEvents[currentEventIndex].GetCoeffForGood(good.goodData)));
            }
        }
    }
}