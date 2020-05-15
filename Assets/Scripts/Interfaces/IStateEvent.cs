public delegate void PlayerDelegate();
public delegate void CreateTriggered(int i);
public interface IStateEvent
{
    PlayerDelegate PlayerDelegate { get; set; }
    CreateTriggered Create{ get; set; }
    event PlayerDelegate OnPlayerDeath;
    event PlayerDelegate OnPlayerRespawn;
}
