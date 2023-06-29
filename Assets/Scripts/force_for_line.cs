//default

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualPantoFramework
{
    /// <summary>
    /// Applies a linear force on any object with a "MeHandle" or "ItHandle" tag within its area.
    /// </summary>
    public class force_for_line : ForceField
    {
        public Vector3 direction;
        [Range(-1, 1)]
        public float strength;

        protected override float GetCurrentStrength(Collider other)
        {
            return strength;
        }

        protected override Vector3 GetCurrentForce(Collider other)
        {
            return (gameObject.transform.rotation * direction).normalized;
        }
    }
}


//// only it
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace DualPantoFramework
//{
//    /// <summary>
//    /// Applies a linear force on any object with an "ItHandle" tag within its area.
//    /// </summary>
//    public class force_for_line : ForceField
//    {
//        public Vector3 direction;
//        [Range(-1, 1)]
//        public float strength;

//        protected override float GetCurrentStrength(Collider other)
//        {
//            if (other.CompareTag("ItHandle"))
//            {
//                return strength;
//            }
//            else
//            {
//                return 0f;
//            }
//        }

//        protected override Vector3 GetCurrentForce(Collider other)
//        {
//            return (gameObject.transform.rotation * direction).normalized;
//        }
//    }
//}