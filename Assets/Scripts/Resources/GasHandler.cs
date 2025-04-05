using UnityEngine;

namespace LudumDare57.Resources
{
    public class GasHandler : MonoBehaviour
    {
        [SerializeField][Min(1e-5f)] private float maxGas = 10f;

        private float gas;

        private void Awake()
        {
            Refuel();
        }

        public bool HasGas(float amount)
        {
            return gas >= amount;
        }

        public bool UseGas(float amount)
        {
            if (!HasGas(amount)) return false;

            gas -= amount;

            return true;
        }

        public void Refuel()
        {
            gas = maxGas;
        }
    }
}