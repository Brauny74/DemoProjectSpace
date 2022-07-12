using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceDemo {
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        protected Marker markerPrefab;
        protected Marker marker;

        protected NavMeshAgent _agent;

        private void Awake()
        {
            marker = Instantiate(markerPrefab);
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
            if (marker == null)
                return;

            marker.gameObject.SetActive(true);
            marker.MoveTo(pos);
        }
    }
}