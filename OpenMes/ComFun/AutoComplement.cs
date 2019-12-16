using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace OpenMes.ComFun
{
    class AutoComplement
    {
        //mssql 접속 openmes!QAZ
        static string strConn = "server= localhost,port  ; database=openmes; User ID = openmestest; Password=openmes!QAZ";
        SqlConnection SqlConn = new SqlConnection(strConn);

      

        public void AutoComplteArea(TextBox TBox)
        {
            TBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection acscItem = new AutoCompleteStringCollection();

            string Item_sel = "SELECT Area FROM CustInfoTbl ";

            SqlDataAdapter adapter = new SqlDataAdapter(Item_sel, SqlConn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str_sugUser = dt.Rows[i]["Area"].ToString();
                acscItem.Add(str_sugUser);
            }
            TBox.AutoCompleteCustomSource = acscItem;
        }

        public void AutoComplteBizman(TextBox TBox)
        {
            TBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection acscItem = new AutoCompleteStringCollection();

            string Item_sel = "SELECT Bizman FROM CustInfoTbl ";

            SqlDataAdapter adapter = new SqlDataAdapter(Item_sel, SqlConn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str_sugUser = dt.Rows[i]["Bizman"].ToString();
                acscItem.Add(str_sugUser);
            }
            TBox.AutoCompleteCustomSource = acscItem;
        }

       
        // 거래처 자동 입력
        public void AutoComplteCustName(TextBox TBox)
        {
            TBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection acscItem = new AutoCompleteStringCollection();

            string Item_sel = "SELECT CustomerName FROM CustInfoTbl ";
            SqlDataAdapter adapter = new SqlDataAdapter(Item_sel, SqlConn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sugCustName = dt.Rows[i]["CustomerName"].ToString();
                acscItem.Add(sugCustName);
            }
            TBox.AutoCompleteCustomSource = acscItem;
        }
        
        public string Str2Num(TextBox TBox)
        {
            if (TBox.Text != "")
            {
                string lgsText;
                lgsText = TBox.Text.Replace(",", "");//숫자변환시 콤마로 발생하는 에러 방지
                TBox.Text = String.Format("{0:#,##0}", Convert.ToInt32(lgsText));//천 단위 찍어주기
                TBox.SelectionLength = 0;
            }
            return TBox.Text;
        }

        /// <summary>
        /// 사용자 정의 InputBox
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        
        public DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        

    }
}
