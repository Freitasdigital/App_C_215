namespace Game.Scripts.Game.Interfaces
{
    public interface IState
    {
        public void Enter(Player.Player player);
        public void Update();
        public void Exit();
    }
}