using UnityEngine;

namespace Wwise.AkCommandBuffer
{
    public class Clock : MonoBehaviour
    {
        private void Update()
        {
            Manager.Instance.Tick();
        }
    }
}