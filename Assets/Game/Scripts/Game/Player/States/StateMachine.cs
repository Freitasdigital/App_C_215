using Game.Scripts.Game.Interfaces;

namespace Game.Scripts.Game.Player.States
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        private Player _player;

        public StateMachine(Player player)
        {
            _player = player;
        }
        
        public void Initialize(IState startState)
        {
            CurrentState = startState;
            CurrentState.Enter(_player);
        }
        
        public void SwitchState(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            
            nextState.Enter(_player);
        }
        
        public void Update()
        {
            if (CurrentState == null) return;
            
            CurrentState.Update();
        }
    }
}