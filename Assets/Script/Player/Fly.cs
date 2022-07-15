using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Player
{
    public class Fly : MonoBehaviour
    {
        [SerializeField] PathMode pm;
        [SerializeField] PathType pt;
        public Vector3[] LandingPoint;

        public delegate void FlyDelegate();
        public static event FlyDelegate FlyEvent;

        public delegate void FlyEndDelegate();
        public static event FlyEndDelegate FlyEndEvent;
        private void Start()
        {
            FlyEvent += CallFly;
            FlyEndEvent += FlyComplete;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fly")
            {
                FlyEvent?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Finish")
            {
                FlyEndEvent?.Invoke();
            }
        }
        public void CallFly()
        {
            transform.DOPath(LandingPoint, 10f, pt, pm);
            transform.GetComponent<Rigidbody>().isKinematic = true;
            PlayerController.instance.anim.SetBool("Run", false);
            PlayerController.instance.anim.SetBool("Fly", true);
            Debug.Log("fly");
        }

        public void FlyComplete()
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            PlayerController.instance.anim.SetBool("Run", true);
            PlayerController.instance.anim.SetBool("Fly", false);
            Debug.Log("END");
        }
    }
}
