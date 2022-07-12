using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(Outline))]
    public class OutlineOnMouseHover : OnMouseOver
    {
        protected Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
        }

        private void Update()
        {
            ShowOutline(IsMouseOver());
        }

        private void ShowOutline(bool value)
        {            
            if(value)
            {
                _outline.enabled = true;
            }else
            {
                _outline.enabled = false;
            }
        }
    }
}