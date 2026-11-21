namespace MVCNetCore_Service_Scope_Transient_Singleton.Models
{
    //Mantiene el 0bjeto  cada vez que hau una peticio HTTP o un request
    public interface IMyServiceSingleton
    {
        string GetMessage();
        int GetCount();
    }

    //Es lo opuesto a Singleton destruye los objetos cada vez peticio HTTP o un request
    public interface IMyServiceTransient
    {
        string GetMessage();
        int GetCount();
    }

    public interface IMyServiceScoped
    {
        string GetMessage();
        int GetCount();
    }

    public class MyService : IMyServiceSingleton, IMyServiceTransient , IMyServiceScoped
    //, IMyServiceScoped
    {
        private int _count;

        private string _message ;
        public MyService()
        {
            _count = 0;
            _message = $"Hello from MyService!{DateTime.Now}";
        }
        public string GetMessage()
        {
            return _message;
        }
        public int GetCount()
        {
            return _count++;
        }

    }
}
