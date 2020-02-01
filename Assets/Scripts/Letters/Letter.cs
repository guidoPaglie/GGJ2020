using System;
using UnityEngine;

namespace Letters
{
    public class Letter : MonoBehaviour
    {
        protected Action _letterRepaired;

        public virtual void Break(Action letterRepaired)
        {
            _letterRepaired = letterRepaired;
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void Reset()
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}