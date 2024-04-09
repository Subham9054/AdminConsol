using CodeGen.Model.DataBaseHelper;

namespace CodeGen.Repository.Repository
{
 public class BaseProvider
    {
        public BaseProvider(IDataBaseHelper dataBaseHelper)
        {
            DataBaseHelper = dataBaseHelper;
        }

        public IDataBaseHelper DataBaseHelper { get; }
    }
}

