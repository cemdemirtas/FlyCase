using UnityEngine;
using Interfaces;
using System.Collections.Generic;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        bool oneLoop;
        int randomWayPoint;
        int randomWayPoint2;
        Vector3 GetWayPoint;
        [SerializeField] Transform waypoints;
        [SerializeField] Transform InstantiatedWings;
        private void FixedUpdate()
        {
            randomWayPoint = Random.Range(0, 7);
            randomWayPoint2 = Random.Range(0, 29);
            GetWayPoint = waypoints.GetChild(randomWayPoint).transform.position;

            if (InstantiatedWings.childCount>29)
            {
                InstantiatedWings.GetChild(randomWayPoint2).transform.position = GetWayPoint;
            }

        }


        private void Start()
        {

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteract interactable))
            {
                interactable.Interact();
                //PickedWings.Add(other.transform);
                //We get new wing instead of picked wing from pool.
                PoolingManager.instance.SpawnFromPool("WingGen", transform.position - new Vector3(0f, 0f, 3f), Quaternion.Euler(0, 0, 0));
            }
        }




    }
}
