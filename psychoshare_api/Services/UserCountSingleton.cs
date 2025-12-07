public class UserCounterSingleton
{
    private static UserCounterSingleton _instance =  new UserCounterSingleton();

    private int _userCount = 0;
    private UserCounterSingleton() { }

    public static UserCounterSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserCounterSingleton();
            }
            return _instance;
        }
    }
    
    public void IncrementUserCount()
    {
        _userCount++;
    }

    public void DecrementUserCount()
    {
        if (_userCount > 0)
        {
            _userCount--;
        }
    }
    public int GetCurrentUserCount()
    {
        return _userCount;
    }
}
