using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class Marker : MonoBehaviour
    {

        private Animator _anmr;

        private void Awake()
        {
            _anmr = GetComponent<Animator>();
        }

        public void MoveTo(Vector3 pos)
        {
            _anmr.SetTrigger("Enabled");
            transform.position = pos;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}