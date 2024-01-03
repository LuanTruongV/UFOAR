using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class UFOPhysics : MonoBehaviour
    {
        [SerializeField] private float tailForce = 1f;
        [SerializeField] private float maxLiftForce = 1f;
        [SerializeField] private float cyclicForce = 1f;
        [SerializeField] private float CyclicForceMultiplier = 1f;

        [SerializeField] private float autoLevelForce = 2f;

        private Vector3 _flatForward;
        private Vector3 _flatRight;
        private float _rightDot;
        private float _forwardDot;

        public void HanlePhysics(Rigidbody rb, InputController input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
            CalculateAngles();
            AutoBlance(rb);
        }

        private void HandleLift(Rigidbody rb, InputController input)
        {
            Vector3 liftForce = transform.up *
                                (Physics.gravity.magnitude + maxLiftForce);
            liftForce *= rb.mass * Mathf.Pow(input.StickyCollective, 1);
            rb.AddForce(liftForce, ForceMode.Force);
        }

        private void HandleCyclic(Rigidbody rb, InputController input)
        {
            float cyclicXForce = -input.Direction.x * cyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicXForce, ForceMode.Acceleration);
            float cyclicYForce = input.Direction.y * cyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicYForce, ForceMode.Acceleration);
            Vector3 forwardVector = _flatForward * _forwardDot;
            Vector3 rightVector = _flatRight * _rightDot;
            Vector3 finalCyclicDir = forwardVector + rightVector;
            finalCyclicDir = Vector3.ClampMagnitude(finalCyclicDir, 1f) * (cyclicForce * CyclicForceMultiplier);

            rb.AddForce(finalCyclicDir, ForceMode.Force);
        }

        private void HandlePedals(Rigidbody rb, InputController input)
        {
            rb.AddTorque(Vector3.up * (input.Shelve.x * tailForce), ForceMode.Acceleration);
        }

        private void CalculateAngles()
        {
            _flatForward = transform.forward;
            _flatForward.y = 0f;
            _flatForward = _flatForward.normalized;


            _flatRight = transform.right;
            _flatRight.y = 0f;
            _flatRight = _flatRight.normalized;


            _forwardDot = Vector3.Dot(transform.up, _flatForward);
            _rightDot = Vector3.Dot(transform.up, _flatRight);
        }

        public void AutoBlance(Rigidbody rb)
        {
            float rightForce = -_forwardDot * autoLevelForce;
            float forwardForce = _rightDot * autoLevelForce;
            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }
    }
}