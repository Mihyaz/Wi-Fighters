public interface IState : IComposable
{
    int Health { get; set; }
    int Score { get; set; }
    int Death { get; set; }
    void ResetHealth();
    void Die();
    void Respawn();
    void Kill();
    void Dissolve();
}
