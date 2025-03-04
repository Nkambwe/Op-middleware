namespace Operators.Moddleware.Data.Transactions {
    /// <summary>
    /// Unit of Work factory interface
    /// </summary>
    public interface IUnitOfWorkFactory {
        /// <summary>
        /// Creates a new instance of a unit of work
        /// </summary>
        /// <returns>Returns created instance of IUnitOfWork</returns>
        IUnitOfWork Create();
    }
}
