using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


namespace SpaceDemo {
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        protected Marker MyMarker;
        protected NavMeshAgent _agent;

        [Inject]
        public void Constructor(Marker marker)
        {
            MyMarker = marker;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public virtual void MoveToClick(Vector3 clickPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            LayerMask terrainMask = LayerMask.GetMask("Terrain");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
            {
                MoveTo(hit.point);
            }
        }

        protected virtual void MoveTo(Vector3 movePos)
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(movePos, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                _agent.isStopped = false;
                _agent.SetPath(path);
                MoveMarker(movePos);
            }
        }

        public virtual void Stop()
        {
            MoveTo(transform.position);
            _agent.isStopped = true;
        }

        protected virtual void MoveMarker(Vector3 pos)
        {
            if (MyMarker == null)
                return;

            MyMarker.MoveTo(pos);
        }
    }
}