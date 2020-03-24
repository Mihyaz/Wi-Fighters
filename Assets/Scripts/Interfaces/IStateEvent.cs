public delegate void PlayerDelegate();
public interface IStateEvent
{
    PlayerDelegate PlayerDelegate { get; set; }
    event PlayerDelegate OnPlayerDeath;
    event PlayerDelegate OnPlayerRespawn;
}
