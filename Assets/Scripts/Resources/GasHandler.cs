using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LudumDare57.Resources
{
    public class GasHandler : MonoBehaviour, IGasHolder
    {
        public float StartTankSize => startTankSize;
        public float MaxTankSize => maxTankSize;
        public UnityEvent TankSizeChanged => tankSizeChanged;
        public UnityEvent GasChanged => gasChanged;
        public UnityEvent GasExhausted => gasExhausted;
        public float TankSize => tankSize;
        public float Gas => gas;

        [Header("Attributes")]
        [SerializeField][Min(1e-5f)] private float startTankSize = 10f;
        [SerializeField][Min(1e-5f)] private float maxTankSize = 100f;
        [Header("Events")]
        [SerializeField] private UnityEvent tankSizeChanged;
        [SerializeField] private UnityEvent gasChanged, gasExhausted;

        private float tankSize, gas;

        private void Start()
        {
            _ = IncreaseTankSize();
            Refuel();
        }

        [ContextMenu(nameof(IncreaseTankSize))]
        public bool IncreaseTankSize() => IncreaseTankSize(startTankSize);

        public bool IncreaseTankSize(float amount)
        {
            if (amount <= 0f || tankSize + amount > maxTankSize) return false;

            tankSize += amount;
            tankSizeChanged.Invoke();

            return true;
        }

        [ContextMenu(nameof(Refuel))]
        public void Refuel()
        {
            gas = tankSize;
            gasChanged.Invoke();
        }

        public bool HasGas(float amount)
        {
            return amount >= 0f && gas >= amount;
        }

        public bool UseGas(float amount)
        {
            if (amount <= 0f) return false;

            float toUse = Mathf.Min(amount, gas);
            if (!HasGas(toUse)) return false;

            gas -= toUse;
            gasChanged.Invoke();
            if (gas == 0f) gasExhausted.Invoke();

            return true;
        }
    }
}