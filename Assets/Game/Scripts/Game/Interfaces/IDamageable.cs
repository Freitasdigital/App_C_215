namespace Game.Scripts.Game.Interfaces
{
	public interface IDamageable
	{
		int Lives { get; }
		
		void RestoreHealth(int amount);
		void TakeDamage(int amount);
	}
}