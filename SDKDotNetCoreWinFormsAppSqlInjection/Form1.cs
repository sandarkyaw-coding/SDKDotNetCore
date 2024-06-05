using SDKDotNetCore.Shared;
using SDKDotNetCore.WinFormsAppSqlInjection;

namespace SDKDotNetCoreWinFormsAppSqlInjection;

public partial class Form1 : Form
{
    private readonly DapperService _dapperService;
    public Form1()
    {
        InitializeComponent();
        _dapperService = new DapperService(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        // string query = $"SELECT * FROM Tbl_User Where email = '{txtEmail.Text.Trim()}' and password = '{txtPassword.Text.Trim()}'";
        string query = $"SELECT * FROM Tbl_User Where email = @Email and password = @Password";
        var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
        {
            Email = txtEmail.Text.Trim(),
            Password = txtPassword.Text.Trim(),
        });
        if (model is null)
        {
            MessageBox.Show("User doesn't exist");
            return;
        }

        MessageBox.Show(model.Email + ". This email user exist.");
    }
}

public class UserModel
{
    public string Email { get; set; }

    public string IsAdmin { get; set; }
}
