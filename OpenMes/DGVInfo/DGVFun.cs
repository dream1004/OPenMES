using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace OpenMes.DGVInfo
{
    
    class DGVFun
    {
        static string strConn = "server= localhost,port  ; database=openmes; User ID = openmestest; Password=openmes!QAZ";
        SqlConnection SqlConn = new SqlConnection(strConn);

        public void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Color customColor = Color.FromArgb(80, Color.Blue);

            using (SolidBrush b = new SolidBrush(customColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 11, e.RowBounds.Location.Y + 4, sf);
            }
        }
    }
}
