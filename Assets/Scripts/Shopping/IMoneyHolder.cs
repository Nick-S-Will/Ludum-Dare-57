namespace LudumDare57.Shopping
{
    public interface IMoneyHolder
    {
        void AddMoney(int amount);

        bool HasMoney(int amount);

        bool UseMoney(int amount);
    }
}