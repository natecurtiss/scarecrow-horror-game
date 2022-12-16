using UnityEngine;

namespace Scarecrow
{
    class Papers : MonoBehaviour
    {
        public static int Taken { get; private set; }
        public static int Total { get; private set; }
        public void Spawn() => Total++;
        public void Took() => Taken++;
    }
}