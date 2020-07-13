namespace CodeAnalyzer.Services {
   public interface IContextService {
        string[] GetProblematicProperties();
        string[] GetProblematicMethods();
   }
    public interface IServiceFactory<T>  {
        T CreateService(string type);
    }
}