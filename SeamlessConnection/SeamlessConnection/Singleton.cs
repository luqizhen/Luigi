namespace SeamlessConnection
{
    public class Singleton
    {
        Singleton _instance;
        protected Singleton()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
        }
    }
}