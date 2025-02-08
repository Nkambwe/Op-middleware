namespace Operators.Moddleware.Helpers {
    public interface IServiceLogger {

        string Id { get;set;}
        string Channel { set; get; }
        void LogToFile(string message, string type = "MSG");
    }
}
