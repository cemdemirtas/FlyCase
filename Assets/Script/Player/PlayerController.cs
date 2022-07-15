using UnityEngine;
using Interfaces;
using System.Collections.Generic;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        int randomWayPoint;
        int randomWingSize;
        Vector3 GetWayPoint;
        [SerializeField] Transform waypoints;
        [SerializeField] Transform InstantiatedWings;
        public Animator anim;
        public Transform PickedWings;

        private void Awake()
        {
            anim.GetComponent<Animator>();
            if (instance==null)
            {
                instance = this;
            }
        }

        private void FixedUpdate()
        {
            randomWayPoint = Random.Range(0, 7);
            randomWingSize = Random.Range(0, 29);
            GetWayPoint = waypoints.GetChild(randomWayPoint).transform.position;

            if (InstantiatedWings.childCount>29)
            {
                InstantiatedWings.GetChild(randomWingSize).transform.position = GetWayPoint;
            }
           
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < PickedWings.childCount; i++)
                {
                    PickedWings.GetChild(i).transform.GetComponent<MeshRenderer>().enabled = true;
                }

            }
        }

        private void Start()
        {
            //this.transform.parent = PickedWings;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteract interactable))
            {
                interactable.Interact();
                other.transform.parent=PickedWings;
                //We get new wing instead of picked wing from pool.
                PoolingManager.instance.SpawnFromPool("WingGen", transform.position - new Vector3(0f, 0f, 3f), Quaternion.Euler(0, 0, 0));
                for (int i = 0; i < PickedWings.childCount; i++)
                {
                    PickedWings.GetChild(i).transform.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }




    }
}
