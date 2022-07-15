using System.Collections.Generic;
using UnityEngine;
using Player;
namespace Objects
{
    public class StackController : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpParent;
        [SerializeField] private float _spaceBetweenNodes;
        [SerializeField] private float _lerpDuration;

        private Transform _stackParent;
        private Transform _playerTransform;
        private List<Transform> _stackList = new List<Transform>();

        private void Awake()
        {
            _stackParent = transform; //this game objects
            _playerTransform = FindObjectOfType<PlayerController>().transform;
            _stackList.Add(_playerTransform);

            Collect.OnInteract += UpdateStack; // add event
        }

        private void Update()
        {
            // Simple fix for the child-parent-parent position relationship.
            //transform.localPosition = new Vector3(_playerTransform.position.x * -1, 0, 0);
            //transform.localPosition = new Vector3(0, 0, _playerTransform.position.z * -1);
        }

        private void FixedUpdate()
        {
            //PlayerController.instance.PickedWings.localPosition = new Vector3(PlayerController.instance.PickedWings.position.x,
            //    PlayerController.instance.PickedWings.position.y,
            //    _playerTransform.localPosition.z * -1);
            WaveNodes();

            PlayerController.instance.PickedWings.localPosition = new Vector3(PlayerController.instance.transform.localPosition.x - 2f,
                PlayerController.instance.PickedWings.localPosition.y,
                PlayerController.instance.PickedWings.localPosition.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerController.instance.PickedWings.position = PlayerController.instance.center.localPosition;
            }
        }

        private void WaveNodes()
        {
            for (int i = 1; i < _stackList.Count; i++)
            {
                Vector3 nodePosition = _stackList[i].localPosition;
                Vector3 previousNodePosition = _stackList[i - 1].localPosition;
                nodePosition = new Vector3(
                      i * _spaceBetweenNodes,
                                           PlayerController.instance.PickedWings.localPosition.y, //Stack parent's heigh(wing)
                                           _playerTransform.localPosition.z
                                          );


                _stackList[i].localPosition = nodePosition;
            }
        }

        private void UpdateStack(bool isPickedUp, Transform node) // action assing 2 paramater
        {
            if (isPickedUp)
            {
                _stackList.Add(node);
                node.SetParent(_stackParent);
            }
            else
            {
                _stackList.Remove(node);
                node.SetParent(_pickUpParent);
            }
        }
    }
}