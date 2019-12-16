using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
//using txtDBvarchar;

namespace OpenMes.InputForms
{
    public partial class CustomerTap : Form
    {
        //mssql 접속 에
        static string strConn = "server= localhost,port  ; database=openmes; User ID = openmestest; Password=openmes!QAZ";
        SqlConnection SqlConn = new SqlConnection(strConn);


        public CustomerTap(string text = "")
        {
            InitializeComponent();
            Text = text;

            CustomerTap_Load(null, null);
            this.dgv_Customer.ColumnHeadersDefaultCellStyle.Font = new Font("Gulim", 12, FontStyle.Bold);
            this.dgv_Customer.DefaultCellStyle.Font = new Font("Gulim", 12);
        }

        private void CustomerTap_Load(object sender, EventArgs e)
        {
            Set_ToolBox();
            CustomerTap_load();
        }

        private void Set_ToolBox()
        {
            btn_CompanySave.Visible = true;
            btn_CompanyUpdate.Visible = false;
            cmb_CustomerInOutcategory.SelectedIndex = 0;
            cmb_InOutcategory.SelectedIndex = 0;
            AutoComplteFun();
        }

        private void AutoComplteFun()
        {
            ComFun.AutoComplement ComAutoCom = new ComFun.AutoComplement();
            ComAutoCom.AutoComplteCustName(txtF_Custname);
            ComAutoCom.AutoComplteBizman(txtF_Bizman);
            ComAutoCom.AutoComplteArea(txtF_Area);
            
        }

        private string PDCodegen()
        {
            string strPDcode = "COU";
            // 연번 부여 : strPDcode를 이용하여 검색한다 
            string strSN = "";
            //  SN  번호를 부여
            strSN = "SELECT top 1  CustomerCode  FROM CustInfoTbl where CustomerCode like '" + strPDcode + "%' order by CustomerCode desc ; ";

            SqlConn.Open();
            SqlCommand cmd = new SqlCommand(strSN, SqlConn);
            string ManaCode = Convert.ToString(cmd.ExecuteScalar());
            SqlConn.Close();

            if (ManaCode == "")            {                strPDcode += "00001";            }
            else
            {
                int int_CodeNo = Convert.ToInt32(ManaCode.Substring(3)) + 1;
                strPDcode += int_CodeNo.ToString("00000");
            }

            return strPDcode;
        }

        private void CustomerTap_load()
        {
            // 데이터 그리드 뷰 열 이동, 정렬 가능   
            dgv_Customer.Columns.Clear();
            dgv_Customer.AllowUserToOrderColumns = true;
            dgv_Customer.DataSource = CustinfoseleData().Tables[0];
            //dgv_Customer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            // 선택한 열은 스크롤 고정 
            // dGV_Data.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            // 열 크기 자동 조정 
            dgv_Customer.AutoResizeColumns();
                
            // 홀수행을 다른 색으로 보여 주고 싶을 때 
            dgv_Customer.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;

            // HeaderText(제목) 칼럼명 지정


            string[,] strA_ItemInfo = new string[23, 4] {{"ID",  "ID", "false", "100" },
            { "CustomerCode","업체코드", "false","100"},
            {"InOutcategory", "입출구분" , "true","100"},
            {"CustomerInOutcategory", "사업구분","true","100"},
            {"CustomerName", "거래처명","true","140"},
            {"CompanyName", "사업자명" ,"true","140"},
            {"RepresentativerName", "대표자","true","100"},
            {"RegistrationNo", "사업자번호","true","120"},
            {"CorporationNo", "법인번호","true","100"},
            {"SubLicenseeNo","종사업장","false","100"},
            {"BusinessType",  "업태" , "true","100"} ,
            {"BusinessItem", "종목","true","100"},
            {"CompanyPhoneNo", "전화번호", "true","100"},
            {"CompanyFAXNo", "FAX번호","true","100"},
            {"CompanyAddress", "사업장주소","true","180"},
            {"PostAddress", "우편물주소","true","180"},
            {"PostNo", "우편번호","true","100"},
            {"HomePage", "홈페이지", "true","120"},
            {"CompanyEmail",  "회사메일", "true","120"},
            {"Area", "영업지역", "true","100"},
            {"Bizman",  "영업사원", "true","100"},
            {"PriceGroup", "단가그룹","false","100"},
            {"CustomerMemo", "메모","true","200"} };

            // 

            for (int i = 0; i < 23; i++)
            {
                dgv_Customer.Columns[i].Name = strA_ItemInfo[i, 0]; dgv_Customer.Columns[i].HeaderText = strA_ItemInfo[i, 1];
                dgv_Customer.Columns[i].Visible = Convert.ToBoolean(strA_ItemInfo[i, 2]);
                dgv_Customer.Columns[i].Width = Convert.ToInt32(strA_ItemInfo[i, 3]);
                // dgv_Info.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }

            /*
            dgv_Customer.Columns[0].HeaderText = "ID";
            dgv_Customer.Columns[1].HeaderText = "업체코드";
            dgv_Customer.Columns[2].HeaderText = "입출구분";
            dgv_Customer.Columns[3].HeaderText = "사업구분";
            dgv_Customer.Columns[4].HeaderText = "거래처명";
            dgv_Customer.Columns[5].HeaderText = "사업자명";
            dgv_Customer.Columns[6].HeaderText = "대표자";
            dgv_Customer.Columns[7].HeaderText = "사업자번호";            
            dgv_Customer.Columns[8].HeaderText = "법인번호";
            dgv_Customer.Columns[9].HeaderText = "종사업장";
            dgv_Customer.Columns[10].HeaderText = "업태";
            dgv_Customer.Columns[11].HeaderText = "종목";
            dgv_Customer.Columns[12].HeaderText = "전화번호";
            dgv_Customer.Columns[13].HeaderText = "FAX번호";
            dgv_Customer.Columns[14].HeaderText = "사업장주소";            
            dgv_Customer.Columns[15].HeaderText = "우편물주소";
            dgv_Customer.Columns[16].HeaderText = "우편번호";
            dgv_Customer.Columns[17].HeaderText = "홈페이지";
            dgv_Customer.Columns[18].HeaderText = "회사메일";
            dgv_Customer.Columns[19].HeaderText = "영업지역";
            dgv_Customer.Columns[20].HeaderText = "영업사원";
            dgv_Customer.Columns[21].HeaderText = "단가그룹";
            dgv_Customer.Columns[22].HeaderText = "메모";      
                        
            dgv_Customer.Columns[0].Visible = false;
            dgv_Customer.Columns[1].Visible = false;
            dgv_Customer.Columns[8].Visible = false;
            dgv_Customer.Columns[9].Visible = false;            
            dgv_Customer.Columns[0].Width = 80;
            */

            DGVInfo.DGVFun dgvFun = new DGVInfo.DGVFun();
            this.dgv_Customer.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvFun.dgvUserDetails_RowPostPaint);
            dgv_Customer.TopLeftHeaderCell.Value = "NO";

            foreach (DataGridViewColumn col in dgv_Customer.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Gulim", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private DataSet CustinfoseleData()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet ds = new DataSet();

            dgv_Customer.DataSource = null;

            string cnv_sel = "SELECT  ID, CustomerCode,InOutcategory, CustomerInOutcategory, CustomerName, CompanyName, RepresentativerName, RegistrationNo, CorporationNo, SubLicenseeNo, BusinessType, BusinessItem, CompanyPhoneNo, CompanyFAXNo, CompanyAddress, PostAddress, PostNo, HomePage, CompanyEmail, Area, Bizman, PriceGroup, CustomerMemo" ;
            cnv_sel += " FROM CustInfoTbl";
            cnv_sel += " where CustomerName  like '%" + txtF_Custname.Text + "%' ";
            cnv_sel += " and InOutcategory  like '%" + cmbF_InOutcategory.Text + "%' ";
            cnv_sel += " and CustomerInOutcategory  like '%" + cmbF_CustomerInOutcategory.Text + "%' ";
            cnv_sel += " and Area like '%" + txtF_Area.Text + "%' ";
            cnv_sel += " and Bizman like '%" + txtF_Bizman.Text + "%' ";

            cmd = new SqlCommand(cnv_sel, SqlConn);
            adapter.SelectCommand = cmd;

            adapter.Fill(ds);

            return ds;
        }
        private void Initial_CustControls()
        {

            cmb_InOutcategory.SelectedIndex = 0;
            cmb_CustomerInOutcategory.SelectedIndex = 0;
            txt_CustomerName.Text = "";
            txt_CompanyName.Text = "";
            txt_RepresentativerName.Text = "";
            txt_RegistrationNo.Text = "";
            txt_CorporationNo.Text = "";
            txt_SubLicenseeNo.Text = "";
            txt_BusinessType.Text = "";
            txt_BusinessItem.Text = "";
            txt_CompanyPhoneNo.Text = "";
            txt_CompanyFAXNo.Text = "";
            txt_CompanyAddress.Text = "";
            txt_PostAddress.Text = "";
            txt_PostNo.Text = "";
            txt_Area.Text = "";
            txt_Bizman.Text = "";
            txt_HomePage.Text = "";
            txt_CompanyEmail.Text = "";
        }


        private void fill_seleData()
        {
            dgv_Customer.DataSource = CustinfoseleData().Tables[0];

            // 맨 처음 컬럼 숨김 기능 
            // dgv_Customer.RowHeadersVisible = false;
            // 선택한 열은 스크롤 고정 
           
            //dgv_Customer.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            // 홀수행을 다른 색으로 보여 주고 싶을 때 
            dgv_Customer.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
        }
        

        private void btn_CompanySave_Click(object sender, EventArgs e)
        {

            lblH_CustomerCode.Text = PDCodegen();

                    SqlConn.Open();

                    string strQRYInsert = "insert into [CustInfoTbl] (CustomerCode, InOutcategory, CustomerInOutcategory, CustomerName, CompanyName, RepresentativerName, RegistrationNo, CorporationNo, SubLicenseeNo, BusinessType, BusinessItem, CompanyPhoneNo, CompanyFAXNo, CompanyAddress, PostAddress, PostNo, HomePage, CompanyEmail, [CustomerMemo], Area, Bizman, PriceGroup, [RegDate] ) ";
                    strQRYInsert += " values ('" + lblH_CustomerCode.Text + "','" + cmb_InOutcategory.Text + "','" + cmb_CustomerInOutcategory.Text + "','" + txt_CustomerName.Text + "','" + txt_CompanyName.Text + "','" + txt_RepresentativerName.Text + "','" + txt_RegistrationNo.Text + "','" + txt_CorporationNo.Text + "','" + txt_SubLicenseeNo.Text + "','" + txt_BusinessType.Text + "','" + txt_BusinessItem.Text + "','" + txt_CompanyPhoneNo.Text + "','" + txt_CompanyFAXNo.Text + "','" + txt_CompanyAddress.Text + "','" + txt_PostAddress.Text + "','" + txt_PostNo.Text + "','" + txt_HomePage.Text + "','" + txt_CompanyEmail.Text + "', '" + txt_CustomerMemo.Text + "', '" + txt_Area.Text + "','" + txt_Bizman.Text + "', '" + cmb_PriceGroup.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms") + "' ) ";
                    SqlDataAdapter da = new SqlDataAdapter(strQRYInsert, SqlConn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SqlConn.Close();

                    MessageBox.Show(this, "거래처 '" + txt_CompanyName.Text + "'자료가 등록되었습니다.", "등록 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ViewDataCustomer();
                    Initial_CustControls();


            CustomerTap_load();
           
        }

        private void btn_CompanyUpdate_Click(object sender, EventArgs e)
        {
            if (lblH_CustomerCode.Text == "MAIN")
            {// 매입매출 구분을 공란으로 한다. 
                cmb_InOutcategory.Text = "";
            }

            SqlConn.Open();

            string strQRYCompanyUpdate = " Update [CustInfoTbl] set [InOutcategory] = '" + cmb_InOutcategory.Text + "',  [CustomerInOutcategory] = '" + cmb_CustomerInOutcategory.Text + "', [CustomerName] = '" + txt_CustomerName.Text + "', [CompanyName]= '" + txt_CompanyName.Text + "', [RepresentativerName]= '" + txt_RepresentativerName.Text + "', RegistrationNo='" + txt_RegistrationNo.Text + "', ";
            strQRYCompanyUpdate += "  CorporationNo = '" + txt_CorporationNo.Text + "', SubLicenseeNo='" + txt_SubLicenseeNo.Text + "', BusinessType='" + txt_BusinessType.Text + "', BusinessItem='" + txt_BusinessItem.Text + "', CompanyPhoneNo='" + txt_CompanyPhoneNo.Text + "', CompanyFAXNo='" + txt_CompanyFAXNo.Text + "', CompanyAddress='" + txt_CompanyAddress.Text + "', PostAddress= '" + txt_PostAddress.Text + "', ";
            strQRYCompanyUpdate += " PostNo = '" + txt_PostNo.Text + "', HomePage= '" + txt_HomePage.Text + "', CompanyEmail= '" + txt_CompanyEmail.Text + "' , [CustomerMemo] = '" + txt_CustomerMemo.Text + "' ,  Area = '" + txt_Area.Text + "', Bizman = '" + txt_Bizman.Text + "' , PriceGroup = '" + cmb_PriceGroup.Text + "'  ,  Moddate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms") + "' where CustomerCode = '" + lblH_CustomerCode.Text + "' ";
            SqlDataAdapter da = new SqlDataAdapter(strQRYCompanyUpdate, SqlConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            MessageBox.Show(this, "거래처 '" + txt_CompanyName.Text + "'자료가 수정되었습니다.", "수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CustomerTap_load();
            SqlConn.Close();
        }

        private void dgv_Customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                // lblH_CustomerCode.Text = dgv_Customer.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblH_CustomerCode.Text = dgv_Customer.Rows[e.RowIndex].Cells["CustomerCode"].Value.ToString();

                TextBox[] arrTxtBox;
                arrTxtBox = new TextBox[] { txt_CustomerName, txt_CompanyName, txt_RepresentativerName, txt_RegistrationNo, txt_CorporationNo, txt_SubLicenseeNo, txt_BusinessType, txt_BusinessItem, txt_CompanyPhoneNo, txt_CompanyFAXNo, txt_CompanyAddress, txt_PostAddress, txt_PostNo, txt_HomePage, txt_CompanyEmail, txt_Area, txt_Bizman, txt_CustomerMemo };
                
                foreach (TextBox tb in arrTxtBox)
                {
                    string tbname = tb.Name;
                    string[] ary_Column = tbname.Split('_');
                    string colname = ary_Column[1];
                    tb.Text = dgv_Customer.Rows[e.RowIndex].Cells[colname].Value.ToString();
                }
                                
                ComboBox[] arrCmbBox;
                arrCmbBox = new ComboBox[] { cmb_InOutcategory, cmb_CustomerInOutcategory, cmb_PriceGroup };

                foreach (ComboBox cb in arrCmbBox)
                {
                    string cbname = cb.Name;
                    string[] ary_Column = cbname.Split('_');
                    string colname = ary_Column[1];
                    cb.Text = dgv_Customer.Rows[e.RowIndex].Cells[colname].Value.ToString();
                }

                //cmb_InOutcategory.Text = dgv_Customer.Rows[e.RowIndex].Cells[2].Value.ToString();
                //cmb_InOutcategory.Text = dgv_Customer.Rows[e.RowIndex].Cells["InOutcategory"].Value.ToString();
                //cmb_CustomerInOutcategory.Text = dgv_Customer.Rows[e.RowIndex].Cells[3].Value.ToString();
                //cmb_PriceGroup.Text = dgv_Customer.Rows[e.RowIndex].Cells[21].Value.ToString();
                
                btn_CompanySave.Visible = false;
                btn_CompanyUpdate.Visible = true;

                AutoComplteFun();

            }
            else { }

            /*
            if (e.RowIndex >= 0)
            {
                lblH_CustomerCode.Text = dgv_Customer.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmb_InOutcategory.Text = dgv_Customer.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmb_CustomerInOutcategory.Text = dgv_Customer.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_CustomerName.Text = dgv_Customer.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_CompanyName.Text = dgv_Customer.Rows[e.RowIndex].Cells[5].Value.ToString();
                txt_RepresentativerName.Text = dgv_Customer.Rows[e.RowIndex].Cells[6].Value.ToString();
                txt_RegistrationNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[7].Value.ToString();
                txt_CorporationNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[8].Value.ToString();
                txt_SubLicenseeNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[9].Value.ToString();
                txt_BusinessType.Text = dgv_Customer.Rows[e.RowIndex].Cells[10].Value.ToString();
                txt_BusinessItem.Text = dgv_Customer.Rows[e.RowIndex].Cells[11].Value.ToString();
                txt_CompanyPhoneNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[12].Value.ToString();
                txt_CompanyFAXNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[13].Value.ToString();
                txt_Address.Text = dgv_Customer.Rows[e.RowIndex].Cells[14].Value.ToString();
                txt_PostAddress.Text = dgv_Customer.Rows[e.RowIndex].Cells[15].Value.ToString();
                txt_PostNo.Text = dgv_Customer.Rows[e.RowIndex].Cells[16].Value.ToString();
                txt_HomePage.Text = dgv_Customer.Rows[e.RowIndex].Cells[17].Value.ToString();
                txt_CompanyEmail.Text = dgv_Customer.Rows[e.RowIndex].Cells[18].Value.ToString();
                txt_Area.Text = dgv_Customer.Rows[e.RowIndex].Cells[19].Value.ToString();
                txt_Bizman.Text = dgv_Customer.Rows[e.RowIndex].Cells[20].Value.ToString();
                cmb_PriceGroup.Text = dgv_Customer.Rows[e.RowIndex].Cells[21].Value.ToString();
                txt_CustomerMemo.Text = dgv_Customer.Rows[e.RowIndex].Cells[22].Value.ToString();

                btn_CompanySave.Visible = false;
                btn_CompanyUpdate.Visible = true;

                AutoComplteFun();
            }

            else { }
            */
        }
       

        private void dgv_Customer_KeyUp(object sender, KeyEventArgs e)
        {

            int rowIndex = dgv_Customer.CurrentRow.Index;


            lblH_CustomerCode.Text = dgv_Customer.Rows[rowIndex].Cells["CustomerCode"].Value.ToString();

            TextBox[] arrTxtBox;
            arrTxtBox = new TextBox[] { txt_CustomerName, txt_CompanyName, txt_RepresentativerName, txt_RegistrationNo, txt_CorporationNo, txt_SubLicenseeNo, txt_BusinessType, txt_BusinessItem, txt_CompanyPhoneNo, txt_CompanyFAXNo, txt_CompanyAddress, txt_PostAddress, txt_PostNo, txt_HomePage, txt_CompanyEmail, txt_Area, txt_Bizman, txt_CustomerMemo };

            foreach (TextBox tb in arrTxtBox)
            {
                string tbname = tb.Name;
                string[] ary_Column = tbname.Split('_');
                string colname = ary_Column[1];
                tb.Text = dgv_Customer.Rows[rowIndex].Cells[colname].Value.ToString();
            }

            ComboBox[] arrCmbBox;
            arrCmbBox = new ComboBox[] { cmb_InOutcategory, cmb_CustomerInOutcategory, cmb_PriceGroup };

            foreach (ComboBox cb in arrCmbBox)
            {
                string cbname = cb.Name;
                string[] ary_Column = cbname.Split('_');
                string colname = ary_Column[1];
                cb.Text = dgv_Customer.Rows[rowIndex].Cells[colname].Value.ToString();
            }

            /*
            lblH_CustomerCode.Text = dgv_Customer.Rows[rowIndex].Cells[1].Value.ToString();
            cmb_InOutcategory.Text = dgv_Customer.Rows[rowIndex].Cells[2].Value.ToString();
            cmb_CustomerInOutcategory.Text = dgv_Customer.Rows[rowIndex].Cells[3].Value.ToString();
            txt_CustomerName.Text = dgv_Customer.Rows[rowIndex].Cells[4].Value.ToString();
            txt_CompanyName.Text = dgv_Customer.Rows[rowIndex].Cells[5].Value.ToString();
            txt_RepresentativerName.Text = dgv_Customer.Rows[rowIndex].Cells[6].Value.ToString();
            txt_RegistrationNo.Text = dgv_Customer.Rows[rowIndex].Cells[7].Value.ToString();
            txt_CorporationNo.Text = dgv_Customer.Rows[rowIndex].Cells[8].Value.ToString();
            txt_SubLicenseeNo.Text = dgv_Customer.Rows[rowIndex].Cells[9].Value.ToString();
            txt_BusinessType.Text = dgv_Customer.Rows[rowIndex].Cells[10].Value.ToString();
            txt_BusinessItem.Text = dgv_Customer.Rows[rowIndex].Cells[11].Value.ToString();
            txt_CompanyPhoneNo.Text = dgv_Customer.Rows[rowIndex].Cells[12].Value.ToString();
            txt_CompanyFAXNo.Text = dgv_Customer.Rows[rowIndex].Cells[13].Value.ToString();
            txt_CompanyAddress.Text = dgv_Customer.Rows[rowIndex].Cells[14].Value.ToString();
            txt_PostAddress.Text = dgv_Customer.Rows[rowIndex].Cells[15].Value.ToString();
            txt_PostNo.Text = dgv_Customer.Rows[rowIndex].Cells[16].Value.ToString();
            txt_HomePage.Text = dgv_Customer.Rows[rowIndex].Cells[17].Value.ToString();
            txt_CompanyEmail.Text = dgv_Customer.Rows[rowIndex].Cells[18].Value.ToString();
            txt_Area.Text = dgv_Customer.Rows[rowIndex].Cells[19].Value.ToString();
            txt_Bizman.Text = dgv_Customer.Rows[rowIndex].Cells[20].Value.ToString();
            cmb_PriceGroup.Text = dgv_Customer.Rows[rowIndex].Cells[21].Value.ToString();
            txt_CustomerMemo.Text = dgv_Customer.Rows[rowIndex].Cells[22].Value.ToString();
            */
            btn_CompanySave.Visible = false;
            btn_CompanyUpdate.Visible = true;

            AutoComplteFun();
        }

        private void 새로고침ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls_Iinitialize();
            btn_CompanySave.Visible = true;
            btn_CompanyUpdate.Visible = false;
        }

        private void Controls_Iinitialize()
        {
            Controls_All_Clear();            
        }

        private void Controls_All_Clear()
        {
            TextBox[] arrTxtBox;
            arrTxtBox = new TextBox[] { txt_CompanyName, txt_CustomerName, txt_Area, txt_Bizman, txt_CustomerMemo };
            foreach (TextBox tb in arrTxtBox)
            { tb.Text = ""; }

            txt_RegistrationNo.Text = "";
            txt_RepresentativerName.Text = "";
            txt_BusinessType.Text = "";
            txt_BusinessItem.Text = "";
            txt_CorporationNo.Text = "";
            txt_CompanyPhoneNo.Text = "";
            txt_CompanyFAXNo.Text = "";
            txt_CompanyEmail.Text = "";
            txt_HomePage.Text = "";
            txt_SubLicenseeNo.Text = "";
            txt_CompanyAddress.Text = "";
            txt_PostAddress.Text = "";
            txt_PostNo.Text = "";

            lblH_CustomerCode.Text = "";
            CustomerTap_load();
        }


        private void btn_CustSelect_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            CustomerTap_load(); 
        }
    }
}