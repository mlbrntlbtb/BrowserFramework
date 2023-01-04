using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Record is used for interaction with the Test Results Database
    /// </summary>
    public class DlkResultsDatabaseConfigRecord
    {
        public String mServer
        {
            get
            {
                return _Server;
            }
        }
        public String mDatabase
        {
            get
            {
                return _Database;
            }
        }
        public String mUser
        {
            get
            {
                return _User;
            }
        }
        public String mPassword
        {
            get
            {
                return _Password;
            }
        }

        private String _Server;
        private String _Database;
        private String _User;
        private String _Password;

        public DlkResultsDatabaseConfigRecord(String Server, String Database, String User, String Password)
        {
            _Server = Server;
            _Database = Database;
            _User = User;
            _Password = Password;
        }
    }
}