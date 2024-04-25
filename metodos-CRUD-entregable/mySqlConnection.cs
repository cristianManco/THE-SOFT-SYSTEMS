using System;

namespace metodos_CRUD_entregable
{
    public class mySqlConnection
    {
        private string dbConnected;

        public mySqlConnection(string dbConnected)
        {
            this.dbConnected = dbConnected;
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}