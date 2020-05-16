public delegate void PlayerDelegate();
public delegate void CreateTriggered(int i);
public delegate void KillFeedTriggered(string a, string b);
public interface IStateEvent
{
    PlayerDelegate PlayerDelegate { get; set; }
    CreateTriggered Create{ get; set; }
    KillFeedTriggered KillFeedRefresh { get; set; }
    event PlayerDelegate OnPlayerDeath;
    event PlayerDelegate OnPlayerRespawn;
}
