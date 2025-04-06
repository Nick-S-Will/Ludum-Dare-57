namespace LudumDare57.Resources
{
    public interface IGasHolder
    {
        float MaxTankSize { get; }
        float TankSize { get; }
        float Gas { get; }

        bool IncreaseTankSize();

        void Refuel();

        bool HasGas(float amount);

        bool UseGas(float amount);
    }
}