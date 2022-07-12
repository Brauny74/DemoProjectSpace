using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class Pooler : BasePooler
    {
        [SerializeField]
        protected GameObject objectToPool;

        protected override void Start()
        {
            _objectToPool = objectToPool;
            FillObjectPool();
        }
    }
}