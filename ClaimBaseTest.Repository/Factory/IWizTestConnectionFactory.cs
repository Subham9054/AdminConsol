using System.Data;

namespace ClaimBaseTest.Repository.Factory
{
     public interface IWizTestConnectionFactory
    {
        /// <summary>
        /// Gets the get connection.
        /// </summary>
        /// <value>The get connection.</value>
        IDbConnection GetConnection { get; }
    }
}
