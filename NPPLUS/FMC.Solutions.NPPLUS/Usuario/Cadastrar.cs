using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FMC.Solutions.NPPLUS.Library.Class;



namespace FMC.Solutions.NPPLUS.Usuario
{
    public partial class CadastrarUsuario : DevExpress.XtraEditors.XtraForm
    {
        //private User user;
        private DialogResult msg;
        public string logradouro, bairro, cidade, estado;
        //ICollection<User> listaSupervisor;
        //ICollection<AccessProfile> listaPerfil;

        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        /*
        public CadastrarUsuario(User _user)
        {
            this.user = _user;
            InitializeComponent();
        }
        */
        private void Cadastro()
        {
            /*
            ckAtivo.Checked = user.DtInactive.HasValue ? false : true;
            ckSupervisor.Checked = !user.FlManager ? false : true;
            ckLoginDialer.Checked = user.FlLoginDialer ? true : false;
            txtNome.Text = user.NmUser;
            txtUserAd.Text = user.DsUserAD;
            txtUserCics.Text = user.Non_Oper_CICS_Login;
            txtCodCics.Text = user.Non_Oper_CICS_Cod;
            txtAgenteDialer.Text = user.UserDialer;
            txtDataAdmissao.EditValue = user.DtAdmission;

            //Supervisor
            cbSupervisor.Properties.Items.Add("Nenhum");
            int indexSupervisor = 0;
            int i = 1;
            foreach (var item in listaSupervisor)
            {
                cbSupervisor.Properties.Items.Add(item.NmUser);
                if (user.IdManager == item.IdUser)
                    indexSupervisor = i;
                i++;
            }
            cbSupervisor.SelectedIndex = indexSupervisor;

            int indexPerfil = 0;
            i = 1;
            cbPerfil.Properties.Items.Add("Nenhum");
            UserProfile userProfile = user.UserProfile.Where(p => p.IdUser == user.IdUser).FirstOrDefault();
            foreach (var item in listaPerfil)
            {
                cbPerfil.Properties.Items.Add(item.DsAccessProfile);
                if (userProfile != null && userProfile.IdAccessProfile == item.IdAccessProfile)
                    indexPerfil = i;
                i++;
            }
            cbPerfil.SelectedIndex = indexPerfil;
            */
        }

        private void Supervisor()
        {
            /*
            cbSupervisor.Properties.Items.Add("Nenhum");
            foreach (var item in listaSupervisor)
            {
                cbSupervisor.Properties.Items.Add(item.NmUser);
            }
            cbSupervisor.SelectedIndex = 0;
            */
        }

        private void Perfil()
        {
            /*
            cbPerfil.Properties.Items.Add("Nenhum");
            foreach (var item in listaPerfil)
            {
                cbPerfil.Properties.Items.Add(item.DsAccessProfile);
            }
            cbSupervisor.SelectedIndex = 0;
            */
        }

        private void CadastrarUsuario_Load(object sender, EventArgs e)
        {
            /*
            listaSupervisor = new UserBLL().GetSupervisor();
            listaPerfil = new AccessProfileBLL().GetAll();
            if (user != null)
            {
                Cadastro();
                if (!Permission.Usuario.Atualizar)
                    btnConfirmar.Enabled = false;
            }
            else
            {
                Supervisor();
                Perfil();

                if (!Permission.Usuario.Adicionar)
                    btnConfirmar.Enabled = false;
            }
            */
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                DateTime? dtInactive = ckAtivo.Checked ? (DateTime?)null : DateTime.Now;
                bool flManager = ckSupervisor.Checked;
                bool flLoginDialer = ckLoginDialer.Checked;
                string nmUser = txtNome.Text.Trim();
                string dsUserAD = txtUserAd.Text.Trim();
                string userCics = txtUserCics.Text.Trim();
                string codCics = txtCodCics.Text.Trim();
                string userDialer = txtAgenteDialer.Text.Trim();
                DateTime? dtAdmission = !string.IsNullOrEmpty(txtDataAdmissao.Text) ? Convert.ToDateTime(txtDataAdmissao.EditValue) : (DateTime?)null;
                User supervisor = listaSupervisor.Where(p => p.NmUser == cbSupervisor.SelectedItem.ToString()).FirstOrDefault();
                AccessProfile profile = listaPerfil.Where(p => p.DsAccessProfile == cbPerfil.SelectedItem.ToString()).FirstOrDefault();

                int? idManager = supervisor != null ? supervisor.IdUser : (int?)null;
                int? idProfile = profile != null ? profile.IdAccessProfile : (int?)null;

                if (string.IsNullOrEmpty(nmUser) || string.IsNullOrEmpty(dsUserAD))
                {
                    XtraMessageBox.Show("É obrigatório preencher no mínimo o Nome e o Usuário de AD.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Registrando informações...");

                    UserBLL userBLL = new UserBLL();

                    if (user != null)
                    {
                        user = userBLL.GetBykey(user.IdUser);
                        UserProfile userProfile = user.UserProfile.Where(p => p.IdUser == user.IdUser).FirstOrDefault();

                        //if (dtInactive != null)
                        user.DtInactive = dtInactive;
                        user.FlManager = flManager;
                        user.FlLoginDialer = flLoginDialer;
                        user.NmUser = nmUser;
                        user.DsUserAD = dsUserAD;
                        user.Non_Oper_CICS_Login = userCics;
                        user.Non_Oper_CICS_Cod = codCics;
                        user.UserDialer = userDialer;
                        user.DtAdmission = dtAdmission;
                        user.IdManager = idManager;

                        if (userProfile != null)
                        {
                            if (idProfile != null)
                                userProfile.IdAccessProfile = (int)idProfile;
                        }
                        else
                        {
                            if (idProfile != null)
                                user.UserProfile.Add(new UserProfile() { IdAccessProfile = (int)idProfile, DtInsert = DateTime.Now });
                        }

                        userBLL.Update(user);
                    }
                    else
                    {
                        user = new User();
                        user.NmUser = nmUser;
                        user.DsUserAD = dsUserAD;
                        user.FlManager = flManager;
                        user.IdManager = idManager;
                        user.Non_Oper_CICS_Login = userCics;
                        user.Non_Oper_CICS_Cod = codCics;
                        user.DtAdmission = dtAdmission;

                        user.UserDialer = userDialer;
                        user.FlLoginDialer = flLoginDialer;
                        if (dtInactive != null)
                            user.DtInactive = dtInactive;

                        if (idProfile != null)
                            user.UserProfile.Add(new UserProfile()
                            {
                                IdAccessProfile = (int)idProfile,
                                DtInsert = DateTime.Now
                            });

                        userBLL.Add(user);
                    }
                    this.DialogResult = DialogResult.OK;
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                Log.SaveFile(ex.Message);
                XtraMessageBox.Show("Erro ao inserir/atualizar cadastro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            */
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}