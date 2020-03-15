public interface ITimer<T> where T : struct
{
    bool Countdown();
    T TimeInSeconds { get; set; }
}