using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceDemo {
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public Marker markerPrefab;
        private Marker marker;

        private NavMeshAgent _agent;

        private void Awake()
        {
            marker = Instantiate(markerPrefab);
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveTo(Vector3 clickPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            LayerMask terrainMask = LayerMask.GetMask("Terrain");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
            {
                NavMeshPath path = new NavMeshPath();
                _agent.CalculatePath(hit.point, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    _agent.isStopped = false;
                    _agent.SetPath(path);
                    MoveMarker(hit.point);
                }
            }
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        private void MoveMarker(Vector3 pos)
        {
            if (marker == null)
                return;

            marker.gameObject.SetActive(true);
            marker.MoveTo(pos);
        }
    }
}