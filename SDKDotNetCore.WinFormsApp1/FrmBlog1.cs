using SDKDotNetCore.Shared;
using SDKDotNetCore.WinFormsApp1.Models;
using SDKDotNetCore.WinFormsApp1.Queries;

namespace SDKDotNetCore.WinFormsApp1
{
    public partial class FrmBlog1 : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlog1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text.Trim();
                blog.BlogAuthor = txtAuthor.Text.Trim();
                blog.BlogContent = txtContent.Text.Trim();

                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";

                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);
                if(result > 0)
                    ClearControl();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }


        private void ClearControl()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtTitle.Focus();
        }
    }
}
