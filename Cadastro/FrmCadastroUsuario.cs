using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tela_de_Autenticação.Cadastro
{
    public partial class FrmCadastroUsuario : Form
    {
        public FrmCadastroUsuario()
        {
            InitializeComponent();
        }

        private void FrmCadastroUsuario_Load(object sender, EventArgs e)

        {

            //conecta com o banco de dados Access  banco.accdb
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=banco.accdb;Persist Security Info=False;";
            using (var connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexão com o banco de dados estabelecida com sucesso!");
                    ListarUsuarios(); // Chama o método para listar usuários ao carregar o formulário
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
                }
            }

        }
        private void ListarUsuarios()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=banco.accdb;Persist Security Info=False;";
            using (var connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Usuario";
                    using (var adapter = new System.Data.OleDb.OleDbDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvUsuario.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao listar usuários: " + ex.Message);
                }
            }
        }
        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=banco.accdb;Persist Security Info=False;";
            using (var connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Usuario (Nome, Senha) VALUES (?, ?)";
                    using (var command = new System.Data.OleDb.OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", nome);
                        command.Parameters.AddWithValue("?", senha);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Usuário cadastrado com sucesso!");
                            txtNome.Clear();
                            txtSenha.Clear();
                            ListarUsuarios(); // Atualiza a lista de usuários após o cadastro
                                                    }
                        else
                        {
                            MessageBox.Show("Falha ao cadastrar usuário.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message);
                }
            }
        }
    }

}
