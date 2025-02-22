namespace Operators.Moddleware.Data.Entities {
    /// <summary>
    /// Interface maintains an entity on the database by simply marking it as deleted
    /// </summary>
    public interface ISoftDelete {
        bool IsDeleted {get;set;}
    }
}