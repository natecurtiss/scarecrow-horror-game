using UnityEngine;

namespace Scarecrow
{
    class Logger : MonoBehaviour
    {
        public void Log(string msg) => Debug.Log(msg);
    }
}