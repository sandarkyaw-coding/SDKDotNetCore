using SDKDotNetCore.Shared;
using SDKDotNetCore.WinFormsApp1.Models;
using SDKDotNetCore.WinFormsApp1.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDKDotNetCore.WinFormsApp1
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;
        // private const int _edit = 1;
        // private const int _delete = 2;
        public FrmBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource = lst;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int columnIndex = e.ColumnIndex;
            //int rowIndex = e.RowIndex;
            if (e.RowIndex == -1) return;

            #region If Case

            var BlogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);
            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog1 frm = new FrmBlog1(BlogId);
                frm.ShowDialog();

                BlogList();
            }
            else if(e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(BlogId);
            }
            #endregion
            /* EnumFormControlType enumFormControlType = EnumFormControlType.None;
            switch (enumFormControlType)
            {
                case EnumFormControlType.None:
                   break;
                case EnumFormControlType.Edit:
                    break;
                case EnumFormControlType.Delete:
                    break;
                case EnumFormControlType.Create:
                    break;
                case EnumFormControlType.Confirm:
                    break;
            }*/
            #region Switch Case

            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;

            switch (enumFormControlType) {
               
                case EnumFormControlType.Edit:
                    FrmBlog1 frm = new FrmBlog1(BlogId);
                    frm.ShowDialog();

                    BlogList();
                    break; 
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;

                    DeleteBlog(BlogId);
                    break;
                case EnumFormControlType.None:
                default:
                    MessageBox.Show("Invalid Case.");
                    break;
            }
            #endregion
        }

        private void DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [BlogId] = @BlogId";

            int result = _dapperService.Execute(query, new {BlogId = id });
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            MessageBox.Show(message);

            BlogList();
        }
    }
}
