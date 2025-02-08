namespace Operators.Moddleware.Services.Access {
    public interface IParameterService {
        Task<bool> GetBooleanParameterAsync(string pWD_USEEXIPRINGPASSWORDS);
        Task<int> GetIntegerParameterAsync(string pWD_NUMBEROFDAYSTOEXPIREY);
    }
}