using LudumDare57.DataSaving;
using UnityEngine;
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
        public float TankSize => tankSegmentCount * startTankSize;
        public float Gas => gas;

        [Header("Attributes")]
        [SerializeField][Min(1e-5f)] private float startTankSize = 10f;
        [SerializeField][Min(1e-5f)] private float maxTankSize = 100f;
        [Header("Events")]
        [SerializeField] private UnityEvent tankSizeChanged;
        [SerializeField] private UnityEvent gasChanged, gasExhausted;

        private int tankSegmentCount;
        private float gas;

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            tankSegmentCount = SaveManager.Instance.TankSegmentCount ?? 1;
            tankSizeChanged.Invoke();

            gas = SaveManager.Instance.Gas ?? TankSize;
            gasChanged.Invoke();
        }

        [ContextMenu(nameof(UpgradeTank))]
        public bool UpgradeTank() => UpgradeTank(1);

        public bool UpgradeTank(int count)
        {
            if (count <= 0 || TankSize + startTankSize > maxTankSize) return false;

            tankSegmentCount += count;
            SaveManager.Instance.TankSegmentCount = tankSegmentCount;

            tankSizeChanged.Invoke();

            return true;
        }

        [ContextMenu(nameof(Refuel))]
        public void Refuel() => Refuel(TankSize);

        private void Refuel(float amount)
        {
            gas = Mathf.Min(gas + amount, TankSize);
            SaveManager.Instance.Gas = gas;

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
            SaveManager.Instance.Gas = gas;

            gasChanged.Invoke();
            if (gas == 0f) gasExhausted.Invoke();

            return true;
        }
    }
}