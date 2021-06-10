using System.Data;

namespace Erp.Pro.Utils
{
    public class MyEventArgs : System.EventArgs
    {
        private DataTable dt;

        public MyEventArgs(DataTable dt)
        {
            this.dt = dt;

        }
        public DataTable DT { get { return dt; } }

    }
}
