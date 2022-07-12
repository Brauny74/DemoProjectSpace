using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(PlanetController))]
    public class PlanetPreviewOnMouseHover : OnMouseOver
    {
        protected PlanetController _planetController;

        protected int hoverPlanetIndex;
        protected static List<bool> hoverPlanets;

        private void Awake()
        {
            _planetController = GetComponent<PlanetController>();

            if (hoverPlanets == null)
                hoverPlanets = new List<bool>();
            hoverPlanets.Add(false);
            hoverPlanetIndex = hoverPlanets.Count - 1;
        }

        // Update is called once per frame
        private void Update()
        {
            SetCurrenPlanetHover();
            CheckPlanetsHover();
        }

        // Sets specific planet's hover to true, if mouse is over it.
        protected virtual void SetCurrenPlanetHover()
        {
            if (IsMouseOver())
            {
                hoverPlanets[hoverPlanetIndex] = true;
            }
            else
            {
                hoverPlanets[hoverPlanetIndex] = false;
            }
        }

        //Checks, if current planet is hovered over, shows corresponding preview panel, and if none is hovered over, hides preview panel.
        protected virtual void CheckPlanetsHover()
        {
            if (hoverPlanets[hoverPlanetIndex])
            {
                _planetController.ShowPreviewPanel();
            }
            foreach (bool hoverPlanet in hoverPlanets)
            {
                if (hoverPlanet)
                {
                    return;
                }
            }
            _planetController.HidePreviewPanel();
        }
    }
}