namespace TempTake_Server.Repository
{
    public abstract class RepositoryUtils
    {
        protected static int? GetGetNonZeroOrNull(int? id)
        {
            if (id == null) return null;
            return id == 0 ? null : id;
        }
    }
}
