using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.Class.Model;
using FMC.Solutions.NPPLUS.Library.APIDialer;
using FMC.Solutions.NPPLUS.Perfil;
using FMC.Solutions.NPPLUS.Relatorio;
using FMC.Solutions.NPPLUS.Usuario;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using FMC.Solutions.NPPLUS.Library.REST;
using FMC.Solutions.NPPLUS.Library.REST.Models;

namespace FMC.Solutions.NPPLUS
{
    public partial class Main : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        //Stopwatch stopWatch = new Stopwatch();
        RepositoryItemButtonEdit repoEnable = new RepositoryItemButtonEdit();
        RepositoryItemButtonEdit repoDisable = new RepositoryItemButtonEdit();
        RepositoryItemButtonEdit repoEndCall = new RepositoryItemButtonEdit();
        Telefone LigacaoManual;

        Stopwatch watchLigacao = new Stopwatch();
        Stopwatch watchTimerPause = new Stopwatch();
        bool LoadAccount = false;
        string timeLoading = "TEMPO PARA CARREGAMENTO DA CONTA" + Environment.NewLine + Environment.NewLine;
        public LoadType TipoPesquisa;
        public string NumeroPesquisa;

        public ICollection<Library.Class.Model.Produto> products = new Collection<Library.Class.Model.Produto>();
        public PersonResponse Person;
        public ICollection<ParameterResponse> ParametersResponse;

        private string cardSelected = string.Empty;
        public Library.Class.Model.Produto productSelected;
        public string TipoLigacao { get { return lblTipoLigacao.Text; } }

        public string TipoPromessa = string.Empty;
        public bool FlCloseCall = false;

        public ICollection<WorkedProduct> WorkedProduct = new Collection<WorkedProduct>();
        public UserAuthenticate User;
        IList<UserProfile> UserProfile;

        //private ICollection<ExtratoDetalhado> extratoDetalhado = null;
        //ICollection<MemoHistory> ocorrenciaResumida = null;
        //ICollection<ActionHistory> ocorrenciaDetalhada = null;
        //ICollection<Mensagem> mensagemPermanente = null;

        PrivateFontCollection pFontCollection = new PrivateFontCollection();
        //AccountUnavailableBLL accountUnavailableBLL = new AccountUnavailableBLL();

        //public TransferChannel transferChannel = new TransferChannel();

        public string PhoneDiscado = string.Empty;

        bool erroDialer = false;
        public string AgentDialer;

        private ICollection<Pause> ListaPausas;

        public Main(UserAuthenticate user, ICollection<ParameterResponse> parametersResponse)
        {
            InitializeComponent();
            this.User = user;
            Permission.UserProfile = user.Profile;
            this.ParametersResponse = parametersResponse;
            ListaPausas = FisAPI.GetPauses(user.OAuth.access_token);
            staticName.Caption = user.DsName;
        }

        /*
        public Main(User user, string userP2)
        {
            this.user = user;
            this.userP2 = userP2;
            InitializeComponent();
        }
        */

        private void Main_StyleChanged(object sender, EventArgs e)
        {
            /*
            var skin = UserLookAndFeel.Default.SkinName;

            if (user.UserProfileScreen == null || user.UserProfileScreen.Count == 0)
            {
                UserBLL userBLL = new UserBLL();
                var usuario = userBLL.GetBykey(user.IdUser);
                usuario.UserProfileScreen.Add(new UserProfileScreen
                {
                    FlEstatementDetail = true,
                    FlOccurrence = true,
                    FlOccurrenceDetail = false,
                    Theme = skin,
                    DtUpdate = DateTime.Now
                });
                userBLL.Update(usuario);
            }
            else
            {
                UserBLL userBLL = new UserBLL();
                var usuario = userBLL.GetBykey(user.IdUser);
                usuario.UserProfileScreen.FirstOrDefault().DtUpdate = DateTime.Now;
                usuario.UserProfileScreen.FirstOrDefault().Theme = skin;
                userBLL.UpdateNoContext(usuario);
            }
            */
        }

        string conta = "";
        string telefone = "";
        public Bilhete bilhete;
        private int IdPausa;

        private Bilhete GetBilhete
        {
            get
            {
                try
                {
                    return DialerConnection.GetBilhete(this.AgentDialer);
                }
                catch (Exception ex)
                {
                    if (!erroDialer)
                    {
                        erroDialer = true;
                        Log.SaveFile("ERRO Dialer.Bilhete" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                        //XtraMessageBox.Show("Aconteceu algum problema de conexão com o Dialer, efetue o login novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    return null;
                }
            }
        }

        private void IconsRepository()
        {
            repoEndCall.Buttons[0].Enabled = true;
            repoEndCall.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEndCall.Buttons[0].ImageOptions.SvgImage = svgImageCollection[8];
            repoEndCall.Buttons[0].ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            repoEndCall.ContextImageOptions.Alignment = ContextImageAlignment.Far;
            repoEndCall.Buttons[0].ImageOptions.SvgImageSize = new Size(16, 16);
            repoEndCall.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEndCall.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnDiscar_ButtonClick);

            repoEnable.Buttons[0].Enabled = true;
            repoEnable.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoEnable.Buttons[0].ImageOptions.SvgImage = svgImageCollection[7];
            repoEnable.Buttons[0].ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            repoEnable.ContextImageOptions.Alignment = ContextImageAlignment.Far;
            repoEnable.Buttons[0].ImageOptions.SvgImageSize = new Size(16, 16);
            repoEnable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoEnable.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnDiscar_ButtonClick);

            repoDisable.Buttons[0].Enabled = false;
            repoDisable.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            repoDisable.Buttons[0].ImageOptions.SvgImage = svgImageCollection[7];
            repoDisable.Buttons[0].ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            repoDisable.ContextImageOptions.Alignment = ContextImageAlignment.Far;
            repoDisable.Buttons[0].ImageOptions.SvgImageSize = new Size(16, 16);
            repoDisable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoDisable.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnDiscar_ButtonClick);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                // this.btnTimer.ImageOptions.SvgImage = svgImageCollection[13];

                lblTotalAccountDelay.Text = "";
                lblTotalAccountDelay.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 8, FontStyle.Regular, GraphicsUnit.Point);

                IconsRepository();
                pbBoxProduto.Hide();

                //Login Dialer
                if (this.User.FlLoginDialer)
                    LoginDialer();

                //Permissions 
                if (!Permission.Usuario.Listar && !Permission.Usuario.Adicionar) accGroupUser.Visible = false;
                if (!Permission.Usuario.Listar) accUsuarios.Visible = false;
                if (!Permission.Usuario.Adicionar) accAddUser.Visible = false;

                if (!Permission.Perfil.Listar && !Permission.Perfil.Adicionar) accGroupPerfil.Visible = false;
                if (!Permission.Perfil.Listar) accPerfil.Visible = false;
                if (!Permission.Perfil.Adicionar) accAddPerfil.Visible = false;

                if (!Permission.Relatorio.Operacional && !Permission.Relatorio.Gerencial) accGroupRelatorio.Visible = false;
                if (!Permission.Relatorio.Operacional) accRelatorioOperacional.Visible = false;
                if (!Permission.Relatorio.Gerencial) accRelatorioGerencial.Visible = false;

                if (!Permission.Configuracao.Tema && !Permission.Configuracao.Carregamento) barSubItem1.Visibility = BarItemVisibility.Never;
                if (!Permission.Configuracao.Tema) barTemas.Visibility = BarItemVisibility.Never;
                if (!Permission.Configuracao.Carregamento) btnCarregamento.Visibility = BarItemVisibility.Never;

                if (!Permission.Configuracao.Parametros.Adicionar && !Permission.Configuracao.Parametros.Listar) btParametros.Visibility = BarItemVisibility.Never;
                if (!Permission.Configuracao.Parametros.Adicionar) btnAddParameter.Visibility = BarItemVisibility.Never;
                if (!Permission.Configuracao.Parametros.Listar) btnListParameter.Visibility = BarItemVisibility.Never;

                /*
if (user.UserProfileScreen.Count > 0)
{
//UserLookAndFeel.Default.UseDefaultLookAndFeel = false;
UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
UserLookAndFeel.Default.SetSkinStyle(user.UserProfileScreen.FirstOrDefault().Theme);
}

if (user.FlLoginDialer && !string.IsNullOrEmpty(userP2.Trim()))
{
gcDiscador.Enabled = true;

//Verificar Token
LoginDialer();
}
else
{
barbuttonDialer.Visibility = BarItemVisibility.Never;
gcDiscador.Enabled = false;
}
*/

                if (!DialerConnection.LogadoDialer)
                {
                    //   ckPause.Enabled = false;
                    //   ckTransfer.Enabled = false;
                }
                else
                {
                    // ckPause.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }
            UserLookAndFeel.Default.StyleChanged += Main_StyleChanged;
            SkinHelper.InitSkinPopupMenu(SkinsLink);

            staticVersion.Caption = " v." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Splash.Visible(splashScreenManager1, false);
        }


        private Font CustomFont(string font, string extension, float size, FontStyle style, GraphicsUnit unit)
        {
            pFontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Library", "Fonts", font + "." + extension));
            return new Font(pFontCollection.Families[0], size, style, unit);
        }

        private void btnAcordo_Click(object sender, EventArgs e)
        {
            try
            {
                if (productSelected.DiasAtraso > Convert.ToInt32(ParametersResponse.Where(P => P.Key == "ATRASO_ACORDO").FirstOrDefault().Value))
                {
                    var telefonePrincipal = Person.Phones.Where(p => p.IdPhoneStatus == 1).FirstOrDefault();
                    if (telefonePrincipal == null)
                    {
                        XtraMessageBox.Show("É necessário selecionar o telefone preferencial do cliente!.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    BloqueiaAcao();
                    Acordo acordo = new Acordo();
                    DialogResult drAcordo = acordo.ShowDialog(this);
                    if (drAcordo == DialogResult.OK)
                    {
                        var workedProduct = WorkedProduct.Where(p => p.Produto == productSelected).FirstOrDefault();
                        MensagemConclusao msgConclusao = new MensagemConclusao(null, workedProduct.StatusLead.Agreement.FirstOrDefault());
                        DialogResult drMsgConclusao = msgConclusao.ShowDialog(this);
                        if (drMsgConclusao == DialogResult.OK)
                        {
                            Splash.Visible(splashScreenManager1, true);
                            splashScreenManager1.SetWaitFormDescription("Atualizando informações...");

                            var result = FisAPI.PostStatusLead(workedProduct.StatusLead, productSelected.CPF, telefonePrincipal.NrPhone, User.OAuth.access_token);

                            Splash.Visible(splashScreenManager1, false);

                            AbrirBoleto();

                            btnAcordo.Enabled = false;
                            btnPromessa.Enabled = false;
                            btnLibera.Enabled = false;
                        }
                        Application.DoEvents();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Produto não disponível para realização de ACORDO.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                Log.SaveFile("EXCEPTION => [btnAcordo_Click]" + ex.Message);
                XtraMessageBox.Show("Falha ao realizar o ACORDO.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
                HabilitaAcao(this.productSelected.DisponivelCobranca);
            }
        }

        private void btnPromessa_Click(object sender, EventArgs e)
        {
            try
            {
                BloqueiaAcao();
                if (btnPromessa.Text.Contains("Cancelar"))
                {
                    MessageBox cancelPPGT = new MessageBox("Cancelar Promessa", "Deseja CANCELAR esta promessa?", "Ao confirmar, a PROMESSA DE PAGAMENTO agendada será removida.", true, "Não", "Sim");
                    DialogResult cancel = cancelPPGT.ShowDialog(this);
                    //DialogResult cancel = DialogResult.Cancel;
                    if (cancel == DialogResult.OK)
                    {
                        var workedProduct = WorkedProduct.Where(p => p.Produto == productSelected).FirstOrDefault();
                        WorkedProduct.Remove(workedProduct);
                        //AppearanceObject clone = btnAcordo.Appearance.BackColor;

                        btnPromessa.Text = "Promessa";
                        btnPromessa.Appearance.BackColor = btnAcordo.Appearance.BackColor;
                        HabilitaAcao(this.productSelected.DisponivelCobranca);

                    }
                    else
                    {
                        HabilitaAcao(this.productSelected.DisponivelCobranca);
                        btnAcordo.Enabled = false;
                        btnPromessa.Enabled = false;
                        btnLibera.Enabled = false;

                        btnPromessa.Enabled = true;
                        btnPromessa.Appearance.BackColor = DXSkinColors.FillColors.Danger;
                        btnPromessa.Text = "Cancelar Promessa";
                    }
                }
                else
                {
                    Promessa promessa = new Promessa();
                    DialogResult drResult = promessa.ShowDialog(this);
                    if (drResult == DialogResult.OK)
                    {
                        var workedProduct = WorkedProduct.Where(p => p.Produto == productSelected).FirstOrDefault();
                        MensagemConclusao msgConclusao = new MensagemConclusao(workedProduct.StatusLead.Promisse.FirstOrDefault(), null);
                        DialogResult drMsgConclusao = msgConclusao.ShowDialog(this);

                        //PROMESSA
                        btnAcordo.Enabled = false;
                        btnPromessa.Enabled = false;
                        btnLibera.Enabled = false;

                        btnPromessa.Enabled = true;
                        btnPromessa.Appearance.BackColor = DXSkinColors.FillColors.Danger;
                        btnPromessa.Text = "Cancelar Promessa";
                    }
                    else
                    {
                        btnAcordo.Enabled = true;
                        btnPromessa.Enabled = true;
                        btnLibera.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [btnPromessa_Click]" + ex.Message);
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
                HabilitaAcao(this.productSelected.DisponivelCobranca);
            }
        }

        private void btnBoleto_Click(object sender, EventArgs e)
        {
            AbrirBoleto();
        }

        private void AbrirBoleto()
        {
            try
            {
                BloqueiaAcao();
                Boleto boleto = new Boleto();
                DialogResult drResult = boleto.ShowDialog(this);
                if (drResult == DialogResult.OK)
                {
                    //pbBoxProduto.Show();
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Atualizando informações...");
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION [AbrirBoleto()] " + ex.Message);
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
                HabilitaAcao(this.productSelected.DisponivelCobranca);
            }
        }

        public void AtualizarCadastro()
        {
            try
            {
                BloqueiaAcao();
                AtualizarEndereco ae = new AtualizarEndereco();

                DialogResult drResult = ae.ShowDialog(this);
                if (drResult == DialogResult.OK)
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Atualizando informações cadastrais...");
                    PopulaBoxTelefone();
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [AtualizarCadastro()]" + ex.Message);
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
                HabilitaAcao(this.productSelected.DisponivelCobranca);
            }
        }

        private void btnAtualizarCadastro_Click(object sender, EventArgs e)
        {
            AtualizarCadastro();
        }

        private void btnAbrirAcordos_Click(object sender, EventArgs e)
        {
            ExtratoAcordo extratoAcordo = new ExtratoAcordo(this);
            DialogResult drExtratoAcordo = extratoAcordo.ShowDialog(this);
        }

        private void btnExtratoResumido_Click(object sender, EventArgs e)
        {
            ExtratoResumido extratoResumido = new ExtratoResumido(this);
            DialogResult drExtrato = extratoResumido.ShowDialog();
        }


        private void AbrirCarregaConta(string cpf)
        {
            try
            {
                CarregarConta carregarConta = new CarregarConta(cpf);
                DialogResult resultCarregarConta = carregarConta.ShowDialog(this);

                if (resultCarregarConta == DialogResult.OK)
                {
                    if (TipoPesquisa != LoadType.NULL && !string.IsNullOrEmpty(NumeroPesquisa))
                    {
                        barbtnLoadConta.Enabled = false;
                        //bwConsultaP2.~CancelAsync()~;
                        //bwProduto.CancelAsync();
                        if (CarregarConta())
                        {
                            try
                            {
                                if (lblTipoLigacao.Text != "-")
                                    timerDialer.Stop();

                                Produto();
                                Application.DoEvents();
                            }
                            catch { }
                            finally
                            {
                                if (lblTipoLigacao.Text != "-")
                                    timerDialer.Start();
                            }
                        }
                    }
                }
                else
                {
                    if (FlCloseCall)
                    {
                        /*
                        Concluir concluir = new Concluir(lead, user);
                        DialogResult drConcluir = concluir.ShowDialog(this);
                        if (drConcluir == DialogResult.OK)
                        {
                            //Finaliza no Dialer
                            Splash.Visible(splashScreenManager1, true);
                            splashScreenManager1.SetWaitFormDescription("Encerrando atendimento...");
                            EndCall();
                        }
                        FlCloseCall = false;
                        */
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //this.Cursor = Cursors.Default;
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void barbtnLoadConta_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                AbrirCarregaConta("");
                //Pesquisa
                //ddListaCartao.DropDownControl = CreateListaCartaoPopupMenu();
                //cbContas.Properties.Items.AddRange();
                //LoadAccount loadAccount = new LoadAccount();
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO [barbtnLoadConta_ItemClick]");
            }
            finally
            {
                //this.Cursor = Cursors.Default;
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private bool CarregarConta()
        {
            /*
              if (products != null && (products.Any(p => p.Card.Contains(NumeroPesquisa)) || products.Any(p => p.Account.Contains(NumeroPesquisa))))
                {
                barbtnLoadConta.Enabled = true;
                return false;
            }
            if (va87 != null && va87.Documento.Contains(NumeroPesquisa))
            {
                barbtnLoadConta.Enabled = true;
                return false;
            }
            */
            return true;
        }

        private void BuscarCartao(string cpf)
        {
            try
            {
                splashScreenManager1.SetWaitFormDescription("Carregando dados do cliente...");
                string textFormat = "{0} - {1} - {2} DIAS";

                Person = FisAPI.GetPerson(cpf, AppSettings.PRODUCT_TYPE, User.OAuth.access_token);

                if (Person != null)
                {
                    lblNomeCliente.Text = Person.Name.Trim().ToUpper();
                    lblCPF.Text = Person.CPF.Trim();
                    lblDataNascimento.Text = Person.DtBirth.ToString("dd/MM/yyyy");
                    lblSexo.Text = Person.RG;
                    btnPromessa.Text = "Promessa";
                    btnPromessa.Appearance.BackColor = btnAcordo.Appearance.BackColor;
                    Application.DoEvents();

                    if (Person.Cards.Count > 0)
                    {
                        //    var bilhete = GetBilhete;
                        foreach (CardResponse card in Person.Cards)
                        {
                            //string produto = Convert.ToUInt64(Regex.Replace(card.NumeroCartao, "[^0-9]", "")).ToString("0000 0000 0000 0000");
                            string produto = card.Account;

                            string tipo = "-";
                            if (bilhete != null)
                            {
                                if (TipoLigacao == "AGUARDANDO")
                                    tipo = "Aguardando";
                                else if (TipoLigacao == "INBOUND")
                                    tipo = "Inbound";
                                else if (TipoLigacao == "MANUAL" || TipoLigacao == "-")
                                    tipo = "Manual";
                                else if (TipoLigacao == "OUTBOUND")
                                    tipo = "Outbound";
                                else if (TipoLigacao == "TRANSFERENCIA")
                                    tipo = "Transferência";
                            }

                            if (TipoLigacao == null || TipoLigacao != "OUTBOUND" || TipoLigacao == "OUTBOUND") //|| (isEstoque && TipoLigacao == "OUTBOUND") || (!isEstoque && TipoLigacao == "OUTBOUND"))
                            {
                                //var lead = card.Lead.OrderByDescending(p => p.DtInsert).FirstOrDefault();

                                products.Add(new Library.Class.Model.Produto
                                {
                                    Select = card.Account + " - " + card.CardNumber,
                                    IdProductType = card.IdProductType,
                                    IdProduct = card.IdProduct,
                                    IdLead = card.IdLead,
                                    CPF = Person.CPF,
                                    Account = card.Account,
                                    Card = card.CardNumber,
                                    Saldo = card.VlFull,
                                    DiasAtraso = card.Age,
                                    DiaVencimento = card.DtDue.HasValue ? card.DtDue.Value.Day : 1,
                                    NomeCartao = card.CardName, //pegar nome do cartão,
                                    DisponivelCobranca = card.AvailableBilling
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + Environment.NewLine;
                }
                Log.SaveFile("EXCEPTION => [BuscarCartao()] => " + erro);
                //Erro ao buscar cartões
            }

        }

        //private void Produto(TipoChamada? tipo = null)
        private void Produto()
        {
            try
            {
                LimparBoxes();

                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Carregando dados da conta...");

                if (lblTipoLigacao.Text != "-")
                    btnLibera.Enabled = false;

                if (TipoPesquisa == LoadType.CPF)
                {
                    BuscarCartao(NumeroPesquisa);
                }
                else if (TipoPesquisa == LoadType.CARD)
                {
                    //BuscarCartao(ariq.Page11.CpfCnpj);
                    BuscarCartao(NumeroPesquisa);
                }
                else if (TipoPesquisa == LoadType.ACCOUNT)
                {
                    /*
                    Product product = new ProductBLL().GetByDsProduct(NumeroPesquisa);
                    if (product != null)
                    {

                        BuscarCartao(product.Account.NrCNPJCPF);
                    }
                    else
                    {
                        ARIQ ariq = SessionP2.Session.ARIQ(NumeroPesquisa, "");
                        if (!string.IsNullOrEmpty(ariq.Page11.CpfCnpj))
                        {
                            Log.SaveFile(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ":  Buscar Cartões CPF: " + ariq.Page11.CpfCnpj);
                            BuscarCartao(ariq.Page11.CpfCnpj);
                            Log.SaveFile(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ":  Final da Busca por Cartões");
                        }
                        else
                        {
                            Splash.Visible(splashScreenManager1, false);
                            barbtnLoadConta.Enabled = true;
                            XtraMessageBox.Show("Conta não encontrada na base de dados. Favor pesquisar por CPF ou Cartão", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    */
                }
                //Organizando os produtos encontrados por maior dia de atraso
                if (products != null && products.Count > 0)
                {
                    products = products.OrderByDescending(p => p.DiasAtraso).ThenByDescending(p => p.Card).ToList();
                    string lastAccount = string.Empty;
                    foreach (var item in products.ToList())
                    {
                        if (item.Account == lastAccount)
                            products.Remove(item);
                        lastAccount = item.Account;
                    }
                    Popular();
                }
                else
                {
                    //this.Cursor = Cursors.Default;
                    Splash.Visible(splashScreenManager1, false);
                    barbtnLoadConta.Enabled = true;
                    XtraMessageBox.Show("Nenhum produto encontrado! Refaça a busca.", "Problemas durante a busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);

                barbtnLoadConta.Enabled = true;
                XtraMessageBox.Show("Erro ao carregar o produto! Refaça a busca.", "Problemas durante a busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveFile("ERRO AO CARREGAR PRODUTO => " + ex.Message);
                throw ex;
            }
            finally
            {
                barbtnLoadConta.Enabled = true;
            }
        }

        private Color ThemeForeColor()
        {
            Skin Skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            return Skin.Colors.GetColor("ControlText");

        }

        private void Popular(string itemSelected = "")
        {
            if (products != null && products.Count > 0)
            {
                //Inicio Atualização de tela pelo Box Produto
                BoxProduto(itemSelected);

                if (products.Where(p => p.DiasAtraso > 0).Count() > 0)
                {
                    lblTotalAccountDelay.Text = "[ " + products.Where(p => p.DiasAtraso > 0).Count() + " CONTA(S) EM ATRASO PARA NEGOCIAÇÃO ]";
                    lblTotalAccountDelay.ForeColor = Color.Red;
                }
                else
                {
                    lblTotalAccountDelay.Text = "[ NENHUMA CONTA EM ATRASO ]";
                    lblTotalAccountDelay.ForeColor = ThemeForeColor();
                }
            }
            Application.DoEvents();
        }

        private void ConsultaP2(string cardSelected)
        {

            try
            {
                CardResponse card = Person.Cards.Where(p => p.Account == cardSelected.Trim()).FirstOrDefault();

                lblResumoDiasAtraso.Text = card.Age.ToString();

                lblResumoDataVencimento.Text = card.DtDue.HasValue ? card.DtDue.Value.ToString("dd/MM/yyyy") : "-";
                lblResumoValorMinimo.Text = "-";//string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", card.VlMinimum);
                lblResumoValorTotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", card.VlDue);
                lblResumoSaldoAtualizado.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", card.VlFull);
                var agreements = card.StatusLeadResponse.Where(p => p.AgreementResponse != null).Select(p => p.AgreementResponse).ToList();
                if (agreements != null)
                {
                    var agreement = agreements.OrderByDescending(p => p.DtInsert).FirstOrDefault();
                    if (agreement != null)
                        lblSituacaoAcordo.Text = agreement.Status;
                }
                Application.DoEvents();

                //Extrato Resumido
                splashScreenManager1.SetWaitFormDescription("Carregando resumo da cobrança...");
                //PopulaExtratoResumido(va82);

                //Box Restrições
                //    PopulaBoxRestricao(vo26);

                //Carregar Extrato Detalhado
                //if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlEstatementDetail))
                //{
                //Consultar VA81
                //    splashScreenManager1.SetWaitFormDescription("Carregando extrato detalhado...");
                //    VA81 va81 = SessionP2.Session.VA81(productSelected.Card.Trim(), productSelected.OrgCMS.Trim());
                //    extratoDetalhado = va81.ListaExtratoDetalhado().OrderByDescending(p => p.DataFatura).ToList();
                //
                //    bool viewFlEstatementDetail = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlEstatementDetail)) ? true : false;
                //    PopulaBoxExtratoDetalhado(extratoDetalhado, viewFlEstatementDetail);
                //}

                //bool viewFlOccurrence = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrence)) ? true : false;
                //PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, viewFlOccurrence);

                //Consultar ARQN



                //    PopulaBoxResumoCobranca(arss);

                //Carregar Telefones da Conta
                try
                {
                    splashScreenManager1.SetWaitFormDescription("Carregando telefones...");
                    PopulaBoxTelefone();

                    splashScreenManager1.SetWaitFormDescription("Carregando ocorrências...");
                    PopulaBoxOcorrenciaDetalhada();

                    splashScreenManager1.SetWaitFormDescription("Carregando situação cadastral...");
                    UpdateIconAtualizarCadastro();

                    splashScreenManager1.SetWaitFormDescription("Habilitando ações...");
                }
                catch (Exception ex)
                {
                    Log.SaveFile(ex.Message + " -- " + ex.StackTrace);
                }

                HabilitaAcao(this.productSelected.DisponivelCobranca);

            }
            catch (Exception ex)
            {
                barbtnLoadConta.Enabled = true;
                btnConcluir.Enabled = true;
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Ocorreu uma falha ao carregar a conta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error, DevExpress.Utils.DefaultBoolean.True);
                Log.SaveFile("ERRO AO CARREGAR [ConsultaP2] => " + productSelected.Account + Environment.NewLine + ex.Message);
                throw ex;
            }
        }

        private void BoxProduto(string itemSelected = "")
        {
            int i = 0;
            if (string.IsNullOrEmpty(itemSelected))
            {
                foreach (var product in products)
                {
                    cbListaCartao.Properties.Items.Add(product.Select);
                }
            }

            if ((!string.IsNullOrEmpty(itemSelected) && itemSelected != productSelected.Select) || TipoPesquisa == LoadType.CARD || TipoPesquisa == LoadType.ACCOUNT)
            {
                foreach (var item in products)
                {
                    if (TipoPesquisa == LoadType.CARD)
                    {
                        if (item.Card.Contains(NumeroPesquisa))
                        {
                            productSelected = item;
                            break;
                        }
                    }
                    else if (TipoPesquisa == LoadType.ACCOUNT)
                    {
                        if (item.Account.Contains(NumeroPesquisa))
                        {
                            productSelected = item;
                            break;
                        }
                    }
                    else if (item.Select.Contains(itemSelected))
                    {
                        productSelected = item;
                        break;
                    }
                    i++;
                }
                TipoPesquisa = LoadType.NULL;
            }
            else
            {
                //if (productSelected == null || productSelected.Select != products.FirstOrDefault().Select)
                if (productSelected == null)
                    productSelected = products.FirstOrDefault();
            }
            if (string.IsNullOrEmpty(itemSelected))
                cbListaCartao.SelectedIndex = i;

            PopulaBoxProduto();
        }

        /*
        private void PopulaExtratoResumido(VA82 va82)
        {
            if (va82.Page == VA82.CurrentPage.FaturaAnterior1 && va82.PrefixoData != null)
            {
                lblDataMesAnterior.Text = va82.DataVencimento.HasValue ? va82.DataVencimento?.ToString("dd/MM/yyyy") : "-";
                lblSaldoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);
                //if()
                //Log.SaveFile()
            }
            else if (va82.Page == VA82.CurrentPage.FaturaCorrente && va82.PrefixoData != null)
            {
                lblDataFaturaAtual.Text = va82.DataVencimento.HasValue ? va82.DataVencimento?.ToString("dd/MM/yyyy") : "-";
                lblSaldoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);

            }
            else if (va82.Page == VA82.CurrentPage.ProximaFatura && va82.PrefixoData != null)
            {
                lblDataAFaturar.Text = va82.DataVencimento.HasValue ? va82.DataVencimento?.ToString("dd/MM/yyyy") : "-";
                lblSaldoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);

            }
            Application.DoEvents();
        }
        */


        /*
        private void PopulaBoxResumoCobranca(ARXQ arxq)
        {
            try
            {
                ICollection<AgreementHistory> agreementHistory = arxq.AgreementHistory;
                if (arxq != null && agreementHistory != null && agreementHistory.Count > 0)
                {
                    AgreementHistory ah = null;

                    if (agreementHistory.Count > 1)
                    {
                        agreementHistory = agreementHistory.OrderByDescending(p => p.Rec).ThenByDescending(p => p.AgreementDate).ToList();
                        IList<string> ignoreList = new List<string>() { "B", "0", "C", };
                        foreach (var ag in agreementHistory)
                        {
                            if (!ignoreList.Any(p => p == ag.IndAgreement))
                            {
                                ah = ag;
                                break;
                            }
                        }
                        //for (int i = 0; i < agreementHistory.Count; i++)
                        //{
                        //    if (!ignoreList.Any(p => p == agreementHistory[i].IndAgreement))
                        //    {
                        //        ah = agreementHistory[i];
                        //        break;
                        //    }
                        //}
                        if (ah == null)
                            ah = arxq.AgreementHistory[1];
                    }
                    else
                        ah = arxq.AgreementHistory.FirstOrDefault();

                    if (ah != null)
                    {
                        StatusAcordoBLL statusAcordoBll = new StatusAcordoBLL();
                        TipoBloqueioBLL tipoBloqueioBll = new TipoBloqueioBLL();
                        StatusAcordo statusAcordo = statusAcordoBll.GetByCod(ah.IndAgreement);

                        ICollection<TipoBloqueio> tipoBloqueio = new Collection<TipoBloqueio>();
                        if (!string.IsNullOrEmpty(ah.DetailAgreement.CurrntBlkCd1))
                            tipoBloqueio.Add(tipoBloqueioBll.GetByCod(ah.DetailAgreement.CurrntBlkCd1));
                        if (!string.IsNullOrEmpty(ah.DetailAgreement.CurrntBlkCd2))
                            tipoBloqueio.Add(tipoBloqueioBll.GetByCod(ah.DetailAgreement.CurrntBlkCd2));

                        if (tipoBloqueio.Count > 0)
                        {
                            string txtBlock = string.Empty;
                            foreach (var tb in tipoBloqueio)
                            {
                                if (string.IsNullOrEmpty(txtBlock))
                                    txtBlock = tb.DsTipoBloqueio;
                                else
                                    txtBlock += " / " + tb.DsTipoBloqueio;
                            }

                            lblBloqueio.Text = txtBlock;
                        }
                        else
                        {
                            lblBloqueio.Text = "-";
                        }

                        if (statusAcordo != null)
                        {
                            lblSituacaoAcordo.Text = statusAcordo.DsStatusAcordo;
                            if (statusAcordo.DsStatusAcordo.StartsWith("ACORDO"))
                                lblSituacaoAcordo.ForeColor = Color.Red;
                            else
                                lblSituacaoAcordo.ForeColor = ThemeForeColor();
                        }
                        else
                        {
                            lblSituacaoAcordo.Text = "-";
                        }
                    }
                    else
                    {
                        lblSituacaoAcordo.Text = "-";
                    }
                }
                else
                {
                    lblSituacaoAcordo.Text = "-";
                    lblBloqueio.Text = "-";
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [PopulaBoxResumoCobranca()] " + ex.Message);
            }
            Application.DoEvents();
        }
        */

        private void PopulaBoxProduto()
        {
            this.lblTextSaldo.Text = "Fatura Fechada";
            //if (productSelected.DiasAtraso > 0) this.lblTextSaldo.Text = "Fatura Fechada";
            this.lblDiasAtraso.Text = productSelected.DiasAtraso.ToString();
            //this.lblDiaVencimento.Text = productSelected.DiaVencimento.ToString();
            this.lblSaldo.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", productSelected.Saldo);
            Application.DoEvents();

        }

        /*
        private void PopulaBoxExtratoDetalhado(ICollection<ExtratoDetalhado> extrato, bool view)
        {
            if (view)
            {
                gcExtratoDetalhado.DataSource = extrato;
                gcExtratoDetalhado.RefreshDataSource();
                gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[3];
            }
            Application.DoEvents();
            //Timer("BOX EXTRATO DETALHADO");
        }
        */

        private void PopulaBoxOcorrenciaDetalhada()
        {

            gcOcorrenciaDetalhada.DataSource = Person.Cards.Where(p => p.Account == cardSelected).FirstOrDefault().StatusLeadResponse.OrderByDescending(p => p.DtStatusLead).ToList();
            gcOcorrenciaDetalhada.RefreshDataSource();

            Application.DoEvents();
        }

        private void PopulaBoxTelefone()
        {
            try
            {
                ICollection<Telefone> listaTelefone = new HashSet<Telefone>();

                foreach (var phone in Person.Phones)
                {
                    string p = phone.NrPhone;
                    if (p.Length >= 11)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                    else if (p.Length >= 10)
                        p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");

                    if (p.Length >= 10)
                    {
                        if (!listaTelefone.Any(ph => Regex.Replace(ph.Numero, "[^0-9]", "") == Regex.Replace(p, "[^0-9]", "")))
                            listaTelefone.Add(
                                new Telefone
                                {
                                    idTelefone = phone.IdPhone,
                                    Numero = p,
                                    Tipo = phone.PhoneType,
                                    Ativo = phone.IdPhoneStatus != 4,
                                    Preferencial = phone.IdPhoneStatus == 1
                                });
                    }
                }

                gcTelefones.DataSource = null;
                gcTelefones.DataSource = listaTelefone;

                ((RepositoryItemButtonEdit)(gcTelefones.MainView as GridView).Columns[2].RealColumnEdit)
                    .Buttons[0].Enabled = false;
                Application.DoEvents();
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO POPULAR BOX TELEFONE [PopulaBoxTelefone]");
                throw e;
            }
        }

        /*
        private void PopulaBoxRestricao(VO26 vo26)
        {
            //Box Restrições
            if (vo26.Restricao != null && vo26.Restricao.Count > 0)
            {
                Restricao SPC = vo26.Restricao.Where(p => p.EmpNegativacao.Contains("SPC")).FirstOrDefault();
                Restricao SERASA = vo26.Restricao.Where(p => p.EmpNegativacao.Contains("SERASA")).FirstOrDefault();
                if (SPC != null)
                {
                    lblSPCDtInclusao.Text = SPC.DataInclusaoNeg?.ToString("dd/MM/yyyy");
                    if (SPC.DataReabilNeg != null)
                    {
                        lblSPCIncluso.Text = "NÃO";
                        lblSPCDtExclusao.Text = SPC.DataReabilNeg?.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lblSPCIncluso.Text = "SIM";
                        lblSPCDtExclusao.Text = "-";
                    }
                }
                else
                {
                    lblSPCIncluso.Text = "-";
                    lblSPCDtExclusao.Text = "-";
                    lblSPCDtInclusao.Text = "-";
                }

                if (SERASA != null)
                {
                    lblSERASADtInclusao.Text = SERASA.DataInclusaoNeg?.ToString("dd/MM/yyyy");
                    if (SERASA.DataReabilNeg != null)
                    {
                        lblSERASAIncluso.Text = "NÃO";
                        lblSERASADtExclusao.Text = SERASA.DataReabilNeg?.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lblSERASAIncluso.Text = "SIM";
                        lblSERASADtExclusao.Text = "-";
                    }
                }
                else
                {
                    lblSERASAIncluso.Text = "-";
                    lblSERASADtExclusao.Text = "-";
                    lblSERASADtInclusao.Text = "-";
                }
            }
            else
            {
                lblSPCIncluso.Text = "-";
                lblSPCDtExclusao.Text = "-";
                lblSPCDtInclusao.Text = "-";
                lblSERASAIncluso.Text = "-";
                lblSERASADtExclusao.Text = "-";
                lblSERASADtInclusao.Text = "-";
            }
            Application.DoEvents();
        }
        */

        private void LimparBoxProduto()
        {
            cbListaCartao.Properties.Items.Clear();
            cbListaCartao.SelectedIndex = -1;
            lblSaldo.Text = "-";
            lblDiasAtraso.Text = "-";
            Application.DoEvents();
        }

        private void LimparBoxDadosCadastrais()
        {
            lblNomeCliente.Text = "-";
            lblDataNascimento.Text = "-";
            lblCPF.Text = "-";
            lblSexo.Text = "-";
            lblNomeMae.Text = "-";
            //lblNomeConjuge.Text = "-";
            //lblDtNascConjuge.Text = "-";
            Application.DoEvents();
        }

        private void LimparBoxRestricoes()
        {
            lblSPCIncluso.Text = "-";
            lblSPCDtInclusao.Text = "-";
            lblSPCDtExclusao.Text = "-";
            lblSERASAIncluso.Text = "-";
            lblSERASADtInclusao.Text = "-";
            lblSERASADtExclusao.Text = "-";
            Application.DoEvents();
        }

        private void LimparBoxExtratoResumido()
        {
            lblSaldoMesAnterior.Text = "-";
            lblValorMinimoMesAnterior.Text = "-";
            lblPagamentoMesAnterior.Text = "-";
            lblSaldoFaturaAtual.Text = "-";
            lblValorMinimoFaturaAtual.Text = "-";
            lblPagamentoFaturaAtual.Text = "-";
            lblSaldoAFaturar.Text = "-";
            lblValorMinimoAFaturar.Text = "-";
            lblPagamentoAFaturar.Text = "-";
            lblDataMesAnterior.Text = "-";
            lblDataFaturaAtual.Text = "-";
            lblDataAFaturar.Text = "-";
            Application.DoEvents();
        }

        private void LimparBoxSituacaoCobranca()
        {
            lblResumoDiasAtraso.Text = "-";
            lblResumoDataVencimento.Text = "-";
            lblResumoValorMinimo.Text = "-";
            lblResumoValorTotal.Text = "-";
            lblBloqueio.Text = "-";
            lblResumoSaldoAtualizado.Text = "-";

            lblSituacaoAcordo.Text = "-";
            lblSituacaoAcordo.ForeColor = ThemeForeColor();

            Application.DoEvents();
        }

        private void LimparBoxExtratoDetalhado()
        {
            gcExtratoDetalhado.DataSource = null;
            gcExtratoDetalhado.RefreshDataSource();
            gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[2];
            Application.DoEvents();
        }

        private void LimparBoxOcorrenciaDetalhada()
        {
            gcOcorrenciaDetalhada.DataSource = null;
            gcOcorrenciaDetalhada.RefreshDataSource();

            this.btnShowOcorrenciaDetalhada.ImageOptions.SvgImage = svgImageCollection[5];
            Application.DoEvents();
        }

        private void LimparBoxTelefone()
        {
            gcTelefones.DataSource = null;
            gcTelefones.RefreshDataSource();
            Application.DoEvents();
        }


        private void BloqueiaAcao()
        {
            btnAbrirAcordos.Enabled = false;
            btnExtratoResumido.Enabled = false;
            btnPromessa.Enabled = false;
            btnAcordo.Enabled = false;
            btnBoleto.Enabled = false;
            btnAtualizarCadastro.Enabled = false;
            //btnAddTelefone.Enabled = false;
            cbListaCartao.Enabled = false;
            gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.Enabled = false;
            btnShowOcorrenciaSimplificada.Enabled = false;
            btnShowOcorrenciaDetalhada.Enabled = false;
            btnCopyAccount.Enabled = false;
            btnConcluir.Enabled = false;
            btnPausa.Enabled = false;
            Application.DoEvents();
        }

        private void HabilitaAcao(bool disponivelCobranca)
        {
            try
            {
                barbtnLoadConta.Enabled = true;
                cbListaCartao.Enabled = true;

                if (Permission.Acordo.Listar)
                    btnAbrirAcordos.Enabled = true;

                if (Permission.ExtratoResumido.Visualizar)
                    btnExtratoResumido.Enabled = true;

                if (Permission.Endereco.Visualizar)
                    btnAtualizarCadastro.Enabled = true;

                //if (Permission.Endereco.AdicionarTelefone)
                //    btnAddTelefone.Enabled = true;

                if (Permission.Boleto.Listar)
                    btnBoleto.Enabled = true;

                btnPausa.Enabled = true;
                gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.Enabled = true;
                btnShowOcorrenciaSimplificada.Enabled = true;
                btnShowOcorrenciaDetalhada.Enabled = true;
                btnCopyAccount.Enabled = true;

                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();
                if (workedProduct == null || !workedProduct.AccountCompleted)
                {

                    if (Permission.Acordo.Simular)
                        btnAcordo.Enabled = disponivelCobranca;

                    if (Permission.Promessa.Visualizar)
                        btnPromessa.Enabled = disponivelCobranca;

                    btnConcluir.Enabled = true;
                }

                Splash.Visible(splashScreenManager1, false);

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO HABILITAR AÇÕES [HabilitaAcao()]");
                throw ex;
            }
        }

        /** Lista de Menus dos Produtos **/
        private DXPopupMenu CreateListaCartaoPopupMenu()
        {
            Log.SaveFile("CreateListaCartaoPopupMenu");
            DXPopupMenu menu = new DXPopupMenu();
            menu.Items.Add(new DXMenuItem("Item", OnItemClick));
            menu.Items.Add(new DXMenuCheckItem("CheckItem", false, null, OnItemClick));
            return menu;
        }

        /** Ação do Click no item **/
        private void OnItemClick(object sender, EventArgs e)
        {
            Log.SaveFile("OnItemClick");
            DXMenuItem item = sender as DXMenuItem;
            XtraMessageBox.Show(item.Caption);
        }


        public enum LoadType
        {
            CPF = 0,
            ACCOUNT = 1,
            CARD = 2,
            NULL = 3
        }

        private void cbListaCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cListaCartao = sender as ComboBoxEdit;
                cardSelected = string.Empty;
                if (cListaCartao.SelectedIndex != -1)
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Carregando dados da conta...");
                    barbtnLoadConta.Enabled = false;
                    cardSelected = cListaCartao.SelectedItem.ToString().Split('-').FirstOrDefault().Trim();
                    LimparBoxRestricoes();
                    LimparBoxExtratoResumido();
                    LimparBoxSituacaoCobranca();
                    LimparBoxExtratoDetalhado();
                    LimparBoxOcorrenciaDetalhada();
                    LimparBoxTelefone();

                    //Limpando Listas
                    /*
                    ocorrenciaResumida = null;
                    ocorrenciaDetalhada = null;
                    extratoDetalhado = null;
                    mensagemPermanente = null;
                    */

                    BloqueiaAcao();
                    BoxProduto(cardSelected);
                    ConsultaP2(cardSelected);

                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION => [cbListaCartao_SelectedIndexChanged]");
            }
        }


        private void UpdateIconAtualizarCadastro()
        {
            this.btnAtualizarCadastro.ImageOptions.SvgImageSize = new Size(24, 24);

            if (Person.Address.DtAddress > DateTime.Today.AddDays(-30))
            {
                this.btnAtualizarCadastro.ImageOptions.SvgImage = svgImageCollection[1];
                this.btnAtualizarCadastro.Text = "Endereço Atualizado";
            }
            else
            {
                this.btnAtualizarCadastro.ImageOptions.SvgImage = svgImageCollection[0];
                this.btnAtualizarCadastro.Text = "Atualizar Cadastro";
            }
            Application.DoEvents();
        }


        private void ClearAll()
        {

            Log.SaveFile("ClearAll()");
            LigacaoManual = null;
            productSelected = null;
            PhoneDiscado = string.Empty;

            lblTotalAccountDelay.Text = "";
            lblTotalAccountDelay.ForeColor = Color.Black;

            products = new Collection<Library.Class.Model.Produto>();
            WorkedProduct = new Collection<WorkedProduct>();
            TipoPromessa = string.Empty;
            FlCloseCall = false;

            //ckTransfer.Checked = false;

            LimparBoxProduto();
            LimparBoxDadosCadastrais();
            LimparBoxRestricoes();
            LimparBoxExtratoResumido();
            LimparBoxSituacaoCobranca();
            LimparBoxExtratoDetalhado();
            LimparBoxOcorrenciaDetalhada();
            LimparBoxTelefone();
            BloqueiaAcao();
            Splash.Visible(splashScreenManager1, false);

        }
        private void LimparBoxes()
        {

            products = new Collection<Library.Class.Model.Produto>();
            WorkedProduct = new Collection<WorkedProduct>();
            TipoPromessa = string.Empty;
            FlCloseCall = false;

            //ckTransfer.Checked = false;

            barbtnLoadConta.Enabled = true;
            cbListaCartao.Enabled = true;
            LimparBoxProduto();
            LimparBoxDadosCadastrais();
            LimparBoxRestricoes();
            LimparBoxExtratoResumido();
            LimparBoxSituacaoCobranca();
            LimparBoxExtratoDetalhado();
            LimparBoxOcorrenciaDetalhada();
            LimparBoxTelefone();
            BloqueiaAcao();
            barbtnLoadConta.Enabled = true;
            cbListaCartao.Enabled = true;

        }
        private void LimparBoxDiscador()
        {
            Log.SaveFile("LimparBoxDiscador()");
            //Parando contador de tempo
            if (bilhete == null || (bilhete != null && bilhete.Status != Status.Pausa))
            {
                watchLigacao.Stop();
                timerLigacao.Stop();
                watchTimerPause.Stop();
                timerPause.Stop();
            }
            lblTelefone.Text = "-";
            lblTipoLigacao.Text = "-";
            lblTempo.Text = "-";
            lblTempo.ForeColor = ThemeForeColor();

            Skin Skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            Color themeForeColor = Skin.Colors.GetColor("ControlText");
            lblTipoLigacao.ForeColor = themeForeColor;
            //iconTipoLigacao.SvgImage = svgImageCollection[12];

            Application.DoEvents();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lblTipoLigacao.Text != "-")
            {
                XtraMessageBox.Show("Antes de fechar a aplicação, é necessário concluir a conta corretamente.", "Informações Gerais", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            else
            {
                MessageBox logout = new MessageBox("Fechar a Aplicação", "Tem certeza que deseja encerrar a aplicação?", "Ao confirmar, todas as ações pendentes serão perdidas.", false, "Não", "Sim");
                DialogResult msg = logout.ShowDialog(); //XtraMessageBox.Show("Tem certeza que deseja sair da aplicação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (msg == DialogResult.OK)
                {
                    Log.SaveFile("Fechamento do NPPLUS");
                    try
                    {
                        if (DialerConnection.LogadoDialer)
                            DialerConnection.Logout(this.AgentDialer);
                    }
                    catch (Exception ex)
                    {
                        Log.SaveFile("Problema ao efetuar logout no dialer." + Environment.NewLine + ex.Message);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private void cbListaCartao_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            string item = e.Item.ToString();
            string[] s = item.Split('-');
            if (s.Count() == 3)
            {
                string[] i = s[2].Trim().Split(' ');
                if (Convert.ToInt32(i[0]) > 0)
                {
                    if (e.State != DrawItemState.Selected)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        e.Cache.FillRectangle(Color.Red, e.Bounds);
                        e.Cache.DrawString("  " + e.Item.ToString(), e.Appearance.Font, Color.White, e.Bounds, e.Appearance.GetStringFormatInfo());
                        e.Handled = true;
                    }
                }
            }
        }

        private void groupControl9_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Log.SaveFile("groupControl9_CustomButtonClick");

            if (gcExtratoDetalhado.DataSource == null && products.Count > 0)
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Carregando extrato detalhado...");

                BloqueiaAcao();
                /*PopulaBoxExtratoDetalhado(extratoDetalhado, true);*/
                HabilitaAcao(this.productSelected.DisponivelCobranca);
                Splash.Visible(splashScreenManager1, false);
            }
            else
            {
                LimparBoxExtratoDetalhado();
            }
        }

        private void gcOcorrencias_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Log.SaveFile("gcOcorrencias_CustomButtonClick");
            if (gcOcorrenciaDetalhada.DataSource == null && products.Count > 0)
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Carregando ocorrências...");
                BloqueiaAcao();
                /*
                PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, true);

                ReloadOcmi("Detalhado");
                PopulaBoxOcorrenciaDetalhada(ocorrenciaDetalhada, true);
                */
                Splash.Visible(splashScreenManager1, false);
            }
            else
            {
                LimparBoxOcorrenciaDetalhada();
            }
        }

        private void btnShowOcorrenciaDetalhada_Click(object sender, EventArgs e)
        {
            if (gcOcorrenciaDetalhada.DataSource == null)
            {
                BloqueiaAcao();
            }
            else
            {
                LimparBoxOcorrenciaDetalhada();
                this.btnShowOcorrenciaDetalhada.ImageOptions.SvgImage = svgImageCollection[5];
            }
        }

        private void btnCarregamento_ItemClick(object sender, ItemClickEventArgs e)
        {
            Configuracoes config = new Configuracoes();
            DialogResult drResult = config.ShowDialog(this);
            if (drResult == DialogResult.OK)
            {
                /*
                 UserBLL userBLL = new UserBLL();
                    user = userBLL.GetBykey(user.IdUser);
                    */
            }
        }

        private void btnCopyAccount_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Person.Name + "\t" + productSelected.CPF + "\t" + productSelected.Account + "\t" + PhoneDiscado);
            XtraMessageBox.Show("Informações copiadas com sucesso!", "Informações da Conta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            try
            {
                BloqueiaAcao();
                Application.DoEvents();
                Log.SaveFile(Environment.NewLine + "******** BTN CONCLUIR ********");
                //timerDialer.Stop();
                ///VERIFICA SE CONTA ATUAL POSSUI ACORDO/PROMESSA
                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();

                if (workedProduct == null)
                {
                    ActionBtnConcluir();
                }
                else
                {
                    MetodoConcluir();
                }
            }
            catch (Exception ex)
            {
                if (lblCPF.Text != "-")
                    HabilitaAcao(this.productSelected.DisponivelCobranca);

                Application.DoEvents();
                Log.SaveFile("ERRO AO CLICAR BOTÃO CONCLUIR [btnConcluir_Click]" + Environment.NewLine + ex.Message);
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Falha ao concluir a conta. Tente novamente ou informe o supervisor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //throw ex;
            }
            finally
            {
                Log.SaveFile("FINALLY [btnConcluir_Click]");
                Splash.Visible(splashScreenManager1, false);
            }
        }

        private void ActionBtnConcluir()
        {
            try
            {
                Concluir concluir = new Concluir(Person.Cards.Where(p => p.Account == cardSelected).FirstOrDefault(), User);
                DialogResult drConcluir = concluir.ShowDialog(this);
                if (drConcluir == DialogResult.OK)
                {
                    MetodoConcluir();
                }
                else
                {
                    HabilitaAcao(this.productSelected.DisponivelCobranca);
                    Application.DoEvents();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void MetodoConcluir()
        {
            try
            {
                Log.SaveFile("******** Metodo Concluir ********");
                //this.Cursor = Cursors.WaitCursor;
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Concluindo codificação...");
                //_bwThreadConcluir = Thread.CurrentThread;

                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();


                if (workedProduct != null && !workedProduct.AccountCompleted)
                {

                    if (workedProduct.Tipo == Library.Class.Model.WorkedProduct.Type.Promessa)
                    {
                        Promisse();
                    }
                    //Se acordo, registrar ocal
                    else if (workedProduct.Tipo == Library.Class.Model.WorkedProduct.Type.Acordo)
                    {
                        splashScreenManager1.SetWaitFormDescription("Inserindo ocorrências...");
                        string memo = "ACORDO ";
                        if (workedProduct.DetailPayment.TipoPagamento == DetailPayment.TypePayment.AVista)
                        {
                            memo += "AVISTA " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " PARA " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM/yyyy") + " " + Regex.Replace(PhoneDiscado, "[^0-9]", "");

                        }
                        else if (workedProduct.DetailPayment.TipoPagamento == DetailPayment.TypePayment.SemEntrada)
                        {
                            memo += "PARC SEM ENTRADA " + workedProduct.DetailPayment.QtdParcel + "*" + workedProduct.DetailPayment.VlParcel?.ToString("N2") + " - 1ª PARC P/ " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM");
                        }
                        else
                        {
                            memo += "PARCELADO " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM") + " " + workedProduct.DetailPayment.QtdParcel + "*" + workedProduct.DetailPayment.VlParcel?.ToString("N2") + " " + Regex.Replace(PhoneDiscado, "[^0-9]", "");
                        }
                    }
                    workedProduct.AccountCompleted = true;
                }

                ValidaConcluir();

                Log.SaveFile("MetodoConcluir -> Final MetodoConcluir()");

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveFile("EXCEPTION [MetodoConcluir]");
                throw ex;
            }
        }

        private void ValidaConcluir()
        {

            Log.SaveFile("ValidaConcluir");
            //Verifica se possui outras contas para atendimento
            int unworkedProduct = 0;
            foreach (var prod in products.Where(p => p.DiasAtraso > 1).ToList())
                if (!WorkedProduct.Any(p => p.Produto.Account == prod.Account))
                    unworkedProduct++;


            if (unworkedProduct > 0)
            {
                Splash.Visible(splashScreenManager1, false);
                MessageBox logout = new MessageBox("Encerrar Atendimento", "Deseja finalizar o atendimento deste cliente?", "Ao confirmar, todas as contas deste cliente ficarão indisponíveis para trabalhar neste atendimento.", true, "Não", "Sim");
                DialogResult msg = logout.ShowDialog(this);
                if (msg == DialogResult.OK)
                {
                    //Finaliza no Dialer
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Encerrando atendimento...");
                    EndCall();
                }
                else
                {
                    //Continua na conta para selecionar outro produto

                    Splash.Visible(splashScreenManager1, true);
                    HabilitaAcao(this.productSelected.DisponivelCobranca);
                    btnAcordo.Enabled = false;

                    btnPromessa.Text = "Promessa";
                    btnPromessa.Appearance.BackColor = btnAcordo.Appearance.BackColor;
                    btnPromessa.Enabled = false;

                    btnConcluir.Enabled = false;
                    btnLibera.Enabled = false;
                    cbListaCartao.Enabled = true;
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Atualizando ocorrências resumidas...");
                    /*
                    ocorrenciaResumida = null;
                    ocorrenciaDetalhada = null;
                    ReloadOcmi("Resumido");
                    PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, true);
                    */
                }
                Splash.Visible(splashScreenManager1, false);
            }
            else
            {
                //Finaliza no Dialer
                EndCall();
            }
        }

        private void EndCall()
        {
            bool liberar = true;

            try
            {
                var resultados = new List<Resultado>();

                if (WorkedProduct != null)
                {
                    foreach (var worked in WorkedProduct)
                    {
                        resultados.Add
                        (
                            new Resultado()
                            {
                                Conta = worked.Produto != null ? worked.Produto.Account : "",
                                CodigoCodificacao = worked.CodFinalizacao,
                                //GrupoCodificacao = worked.DsStatusResum,
                                IdStatusDialer = worked.IdStatusDialer,
                                DataRetorno = (worked.CodFinalizacao == "VLC") ? worked.DataRetorno : null,
                                TelefoneRetorno = (worked.CodFinalizacao == "VLC") ? worked.TelefoneRetorno : null
                            }
                        );
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(bilhete.IdCall) && DialerConnection.LogadoDialer)
                        {
                            liberar = DialerConnection.AytyDialerAgentAPI.IdStatusPauseSchedule <= 0;
                            DialerConnection.FinalizarChamada(this.AgentDialer, bilhete.IdCall, resultados.FirstOrDefault().IdStatusDialer, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.SaveFile("Problemas ao Finalizar a conta -> " + ex.Message);
                    }
                }
                ClearAll();

                if (DialerConnection.LogadoDialer)
                    LimparBoxDiscador();
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string error = exception.StackTrace + Environment.NewLine;
                error += exception.Message;

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    error += exception.StackTrace + Environment.NewLine;
                    error += exception.Message + " " + Environment.NewLine;
                }
                Log.SaveFile("ERRO NO MÉTODO EndCall" + Environment.NewLine + "======================" + error);
            }
            finally
            {
                LoadAccount = false;

                Splash.Visible(splashScreenManager1, false);
                btnLibera.Enabled = false;
                if (DialerConnection.LogadoDialer)
                {
                    if (liberar)
                        DialerConnection.Libera(this.AgentDialer);

                    timerDialer.Start();
                }

                Log.SaveFile("EndCall [Finally]");
            }
        }

        private void timerDialer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DialerConnection.LogadoDialer)
                {
                    var _bilhete = GetBilhete;
                    if (_bilhete != null)
                        bilhete = _bilhete;

                    if (conta != bilhete.Conta && telefone != bilhete.NumeroTelefone)
                    {
                        try
                        {
                            conta = bilhete.Conta;
                            telefone = bilhete.NumeroTelefone;
                        }
                        catch (Exception ex)
                        {
                        }
                    }


                    lblCampanha.Text = bilhete.Campanha;
                    Skin Skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
                    Color themeForeColor = Skin.Colors.GetColor("ControlText");
                    //Color themeForeColor = Skin.Colors.GetColor("Control");

                    if (bilhete.Status == Status.Pausa)
                    {
                        if (IdPausa != bilhete.IdPausa)
                        {
                            IdPausa = bilhete.IdPausa;
                            watchLigacao.Stop();
                            watchLigacao.Restart(); // Start contador de tempo ligação
                            timerLigacao.Start(); // Atualizador de tempo da ligação
                        }
                        var pause = ListaPausas.Where(p => p.IdAytyPause == IdPausa).FirstOrDefault();
                        lblStatus.Text = "Pausa" + (pause != null ? " - " + pause.DsPause : (DialerConnection.AytyDialerAgentAPI.DeReturnAPI == "ACW" ? "Pós Atendimento (ACW)" : ""));
                    }
                    else
                    {
                        IdPausa = 0;

                        if (DialerConnection.AytyDialerAgentAPI.IdStatusPauseSchedule > 0)
                        {
                            var pause = ListaPausas.Where(p => p.IdAytyPause == DialerConnection.AytyDialerAgentAPI.IdStatusPauseSchedule).FirstOrDefault();
                            lblStatus.Text = bilhete.DescricaoStatus + " - Pausa " + (pause != null ? pause.DsPause : "") + " agendada";
                        }
                        else
                            lblStatus.Text = bilhete.DescricaoStatus;
                    }
                    if (!string.IsNullOrEmpty(bilhete.NumeroTelefone))
                    {
                        string p = bilhete.NumeroTelefone.Trim();
                        if (p.Length >= 11)
                            p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 00000-0000");
                        else if (p.Length >= 10)
                            p = Convert.ToUInt64(Regex.Replace(p, "[^0-9]", "")).ToString("(00) 0000-0000");
                        else
                            p = Regex.Replace(p, "[^0-9]", "");

                        lblTelefone.Text = p;
                        PhoneDiscado = p;

                        if (bilhete.TipoChamada == TipoChamada.Inbound)
                        {
                            lblTipoLigacao.Text = "INBOUND";
                            lblTipoLigacao.ForeColor = Color.Green;
                            //iconTipoLigacao.SvgImage = svgImageCollection[9];
                        }
                        else if (bilhete.TipoChamada == TipoChamada.Outbound)
                        {
                            lblTipoLigacao.Text = "OUTBOUND";
                            lblTipoLigacao.ForeColor = themeForeColor;
                            // iconTipoLigacao.SvgImage = svgImageCollection[11];
                        }
                        else if (bilhete.TipoChamada == TipoChamada.Manual)
                        {
                            lblTipoLigacao.Text = "MANUAL";
                            lblTipoLigacao.ForeColor = Color.DodgerBlue;
                            //iconTipoLigacao.SvgImage = svgImageCollection[10];
                        }
                        else if (bilhete.TipoChamada == TipoChamada.Transferencia)
                        {
                            lblTipoLigacao.Text = "TRANSFERÊNCIA";
                            lblTipoLigacao.ForeColor = Color.DarkViolet;
                            //iconTipoLigacao.SvgImage = svgImageCollection[11];
                        }
                        else
                        {
                            lblTipoLigacao.Text = "-";
                            lblTipoLigacao.ForeColor = themeForeColor;
                            //iconTipoLigacao.SvgImage = svgImageCollection[12];
                        }

                        if (LoadAccount == false)
                        {
                            if (!string.IsNullOrEmpty(bilhete.CPF) && bilhete.TipoChamada == TipoChamada.Outbound)
                            {
                                try
                                {
                                    Log.SaveFile("Conta -> " + bilhete.Conta + " CPF:" + bilhete.CPF);

                                    //Validando se existe dados na tela
                                    if (products != null && products.Count > 0)
                                    {
                                        Log.SaveFile("=== LIMPANDO TELA - CONTA MANUAL === ");
                                        ClearAll();
                                    }


                                    LoadAccount = true;
                                    TipoPesquisa = LoadType.CPF;
                                    NumeroPesquisa = bilhete.CPF.Trim();
                                    watchLigacao.Stop();
                                    watchLigacao.Restart(); // Start contador de tempo ligação
                                    timerLigacao.Start(); // Atualizador de tempo da ligação
                                    timerDialer.Stop();

                                    if (!string.IsNullOrEmpty(NumeroPesquisa) || !NumeroPesquisa.Contains("0000000000"))
                                    {

                                        Log.SaveFile("==== Conta Recebida Dialer -> " + NumeroPesquisa);
                                        Log.SaveFile(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ":  Inicio da Pesquisa ");
                                        Produto();
                                        Log.SaveFile(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ":  Final da Pesquisa ");
                                        Splash.Visible(splashScreenManager1, false);
                                    }
                                    else
                                    {
                                        Splash.Visible(splashScreenManager1, false);
                                        AbrirCarregaConta("");
                                    }
                                    //this.Cursor = Cursors.Default;

                                }
                                catch (Exception ex)
                                {
                                    Log.SaveFile("ERRO AO PROCESSAR BILHETE => " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                                    //throw ex;
                                }
                                finally
                                {
                                    timerDialer.Start();
                                }
                            }
                            else if (bilhete.TipoChamada == TipoChamada.Inbound || bilhete.TipoChamada == TipoChamada.Transferencia)
                            {
                                if (bilhete.TipoChamada == TipoChamada.Inbound)
                                    Log.SaveFile("Inbound -> " + bilhete.NumeroTelefone);
                                else
                                    Log.SaveFile("Transferencia -> " + bilhete.NumeroTelefone);

                                //Validando se existe dados na tela
                                if (products != null && products.Count > 0)
                                {
                                    Log.SaveFile("=== LIMPANDO TELA - CONTA MANUAL === ");
                                    ClearAll();
                                }

                                watchLigacao.Stop();
                                watchLigacao.Restart(); // Start contador de tempo ligação
                                timerLigacao.Start(); // Atualizador de tempo da ligação
                                LoadAccount = true;
                                string cpf = "";
                                if (!string.IsNullOrEmpty(bilhete.CPF) && !string.IsNullOrWhiteSpace(bilhete.CPF))
                                    cpf = bilhete.CPF;
                                else
                                    cpf = GetCPF(bilhete.NumeroTelefone);

                                AbrirCarregaConta(cpf);
                            }
                            else
                            {
                                if (bilhete.TipoChamada == TipoChamada.Outbound || (bilhete.TipoChamada != TipoChamada.Inbound && bilhete.TipoChamada != TipoChamada.Manual && bilhete.TipoChamada != TipoChamada.Aguardando && bilhete.TipoChamada != TipoChamada.Transferencia))
                                {
                                    watchLigacao.Stop();
                                    watchLigacao.Restart(); // Start contador de tempo ligação
                                    timerLigacao.Start(); // Atualizador de tempo da ligação
                                    btnLibera.Enabled = true;
                                    LoadAccount = true;

                                    if (string.IsNullOrEmpty(bilhete.CPF) && bilhete.TipoChamada == TipoChamada.Outbound)
                                    {
                                        LoadAccount = true;
                                        TipoPesquisa = LoadType.CPF;
                                        NumeroPesquisa = bilhete.CPF.Trim();
                                        watchLigacao.Stop();
                                        watchLigacao.Restart(); // Start contador de tempo ligação
                                        timerLigacao.Start(); // Atualizador de tempo da ligação
                                        timerDialer.Stop();

                                        if (string.IsNullOrEmpty(NumeroPesquisa) || NumeroPesquisa.Contains("0000000000"))
                                        {
                                            Splash.Visible(splashScreenManager1, false);
                                            AbrirCarregaConta("");
                                        }
                                    }
                                    else
                                    {
                                        Log.SaveFile("ERRO AO PROCESSAR BILHETE - [NÃO VEIO CPF E CONTA] - TELEFONE: " + bilhete.NumeroTelefone);
                                        XtraMessageBox.Show("Aconteceu algum problema durante o carregamento da conta, encerrar o Dialer.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        EndCall();
                                    }
                                }
                            }
                        }
                    }

                    /*
                    if (bilhete.Status == Status.Deslogado && user.FlLoginDialer)
                    {
                        timerDialer.Stop();
                        LoginDialer();
                    }
                    */
                    ConfigureButtons(bilhete);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO NO BILHETE [timerDialer_Tick]" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                Splash.Visible(splashScreenManager1, false);
                btnLibera.Enabled = true;
            }
            finally
            {

            }
        }

        private string GetCPF(string nrPhone)
        {
            /*
            var phone = new PhoneBLL().GetByNrPhone(nrPhone);
            if (phone != null)
                return phone.Account.NrCNPJCPF;
            else
                return string.Empty;
            */
            return string.Empty;
        }

        private void btnLibera_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox logout = new MessageBox("Liberar Atendimento", "Realmente deseja liberar o atendimento/conta?", "Ao confirmar, caso esteja em atendimento, a ligação será finalizada.", true, "Não", "Sim");
                DialogResult msg = logout.ShowDialog(); //XtraMessageBox.Show("Tem certeza que deseja sair da aplicação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (msg == DialogResult.OK)
                {
                    ClearAll();
                    LimparBoxDiscador();
                    LoadAccount = false;
                    DialerConnection.Libera(this.AgentDialer);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR Dialer.Libera() [btnLibera_Click]" + Environment.NewLine + ex.Message);
            }
        }

        private void Pausa()
        {
            CodificarPausa codificarPausa = new CodificarPausa(ListaPausas);
            DialogResult dr = codificarPausa.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                //
            }
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            //JANELA DE PAUSAS
            Pausa();
        }

        private void ConfigureButtons(Bilhete bilhete)
        {
            Status status = bilhete.Status;
            GridView gridView = gcTelefones.MainView as GridView;
            switch (status)
            {
                case Status.Liberado: //READY 
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = false;
                    if (bilhete.TipoChamada == TipoChamada.Inbound)
                        this.btnLibera.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
                case Status.EmFilaEspera: //QUEUE
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = false;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
                case Status.EmChamada: //INCALL
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = false;
                    if (lblTipoLigacao.Text == "-")
                        this.btnLibera.Enabled = false;
                    //((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                case Status.Pausa: //PAUSED
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = false;
                    if (lblTipoLigacao.Text == "-")
                        this.btnLibera.Enabled = true;
                    //((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
                case Status.Erro: //DEAD
                    this.btnPausa.Enabled = false;
                    this.btnLibera.Enabled = false;
                    if (lblTipoLigacao.Text == "-")
                        this.btnLibera.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                case Status.Deslogado: //LOGOUT
                    this.btnPausa.Enabled = false;
                    this.btnLibera.Enabled = false;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                default: //UNKNOW
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
            }
            Application.DoEvents();
        }
        private void timerLigacao_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = watchLigacao.Elapsed;
            lblTempo.Text = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            if (ts.Minutes > 5)
                lblTempo.ForeColor = Color.Red;
        }
        private void LoginDialer()
        {
            LoginDialer loginDialer = new LoginDialer();
            if (loginDialer.ShowDialog(this) == DialogResult.OK)
            {
                this.AgentDialer = loginDialer.AgentDialer;
                if (DialerConnection.LogadoDialer)
                {
                    DialerConnection.Libera(this.AgentDialer);
                    timerDialer.Start();

                    Splash.Visible(splashScreenManager1, false);
                }
            }
        }

        private void btnNotes_ItemClick(object sender, ItemClickEventArgs e)
        {
            Notes notes = new Notes();
            notes.Show();
        }

        public void AbortThread(Thread thread)
        {
            if (thread != null)
                thread.Abort();
        }

        private void timerPB_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
        }


        private void btnDesligar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            string phone = ((Telefone)gridView1.GetFocusedRow()).Numero;
            phone = phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        }

        private void btnDiscar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit editor = (ButtonEdit)sender;
                DevExpress.XtraEditors.Controls.EditorButton Button = e.Button;
                GridView gv = gcTelefones.MainView as GridView;


                bool value = e.Button.ImageOptions.SvgImage == svgImageCollection[8];

                if (value)
                {
                    //Desligar
                    LigacaoManual = null;
                    ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;

                    DialerConnection.FinalizarChamada(this.AgentDialer, bilhete.IdCall, "211", true);
                    LimparBoxDiscador();
                }
                else
                {
                    //Ligar
                    LigacaoManual = (Telefone)gv.GetFocusedRow();
                    ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    ((RepositoryItemButtonEdit)gv.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    string phone = LigacaoManual.Numero;
                    phone = phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                    string ddd = phone.Substring(0, 2);
                    string phoneNumber = phone.Substring(2, phone.Length - 2);

                    DialerConnection.ChamadaManual(AgentDialer, ddd, phoneNumber);
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {

            }
        }
        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name != "gridColDiscar")
                return;
            GridView gv = gcTelefones.MainView as GridView;
            var a = gv.GetRowCellValue(e.RowHandle, gv.Columns[0]);
            if (LigacaoManual == null)
            {
                e.RepositoryItem = repoEnable;
            }
            else
            {
                if (LigacaoManual.Numero == a.ToString())
                    e.RepositoryItem = repoEndCall;
                else
                    e.RepositoryItem = repoDisable;
            }
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            if (gridView1.ActiveEditor is ButtonEdit)
            {
                ButtonEdit editor = gridView1.ActiveEditor as ButtonEdit;
                editor.Properties.Buttons[0].Enabled = false;
            }
        }

        private void accRelatorioOperacional_Click(object sender, EventArgs e)
        {
            RelatorioOperacional relatorio = new RelatorioOperacional();
            DialogResult drResult = relatorio.ShowDialog(this);
        }

        private void accordionRelatorioOperacional_Click(object sender, EventArgs e)
        {
            RelatorioOperacional relatorio = new RelatorioOperacional();
            DialogResult drResult = relatorio.ShowDialog(this);
        }

        private void accUsuarios_Click(object sender, EventArgs e)
        {
            Usuario.Listar listarUsuarios = new Usuario.Listar();
            DialogResult drResult = listarUsuarios.ShowDialog(this);
        }

        private void accAddUser_Click(object sender, EventArgs e)
        {
            CadastrarUsuario cadastrarUsuario = new CadastrarUsuario();
            DialogResult drResult = cadastrarUsuario.ShowDialog(this);
        }

        private void accAddPerfil_Click(object sender, EventArgs e)
        {
            CadastrarPerfil cadastrarPerfil = new CadastrarPerfil();
            DialogResult drResult = cadastrarPerfil.ShowDialog(this);
        }

        private void accPerfil_Click(object sender, EventArgs e)
        {
            Perfil.Listar listarPerfil = new Perfil.Listar();
            DialogResult drResult = listarPerfil.ShowDialog(this);
        }

        private void btnListParameter_ItemClick(object sender, ItemClickEventArgs e)
        {
            Configurations.SystemParameter.ListarSystemParameter listarParametros = new Configurations.SystemParameter.ListarSystemParameter();
            DialogResult drResult = listarParametros.ShowDialog(this);
        }

        private void btnAddParameter_ItemClick(object sender, ItemClickEventArgs e)
        {
            Configurations.SystemParameter.Cadastrar cadastrar = new Configurations.SystemParameter.Cadastrar();
            DialogResult drResult = cadastrar.ShowDialog(this);
        }

        private void accRelatorioGerencial_Click(object sender, EventArgs e)
        {
            RelatorioGerencial relatorio = new RelatorioGerencial();
            DialogResult drResult = relatorio.ShowDialog(this);
        }

        private void barbuttonDialer_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoginDialer();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            watchTimerPause.Stop();
            watchTimerPause.Restart(); // Start contador de tempo ligação
            timerPause.Stop();
            //this.btnTimer.ImageOptions.SvgImage = svgImageCollection[13];

        }

        private void timerPause_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = watchTimerPause.Elapsed;
            //lblTimerPause.Text = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            //if (ts.Minutes > 5)
            //    lblTimerPause.ForeColor = Color.Red;
        }

        private void ResizeControlers()
        {
            int width = this.Width;
            int height = this.Height;

            if (width < 1280 && height < 970)
            {
                gcActions.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                gcDiscador.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
                gcDiscador.Left = 6;
                gcDiscador.Top = 861;
            }
            else if (width < 1280 && height >= 970)
            {
                gcActions.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom);
                gcDiscador.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            }
            else if (width >= 1280 && height < 970)
            {
                gcActions.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                gcDiscador.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            }

            else if (width >= 1280 && height >= 970)
            {
                gcActions.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                gcDiscador.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            }
            Application.DoEvents();
            xtraScrollableControl1.Refresh();
            this.Refresh();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            //ResizeControlers();
        }

        private void accAshi_Click(object sender, EventArgs e)
        {
            if (productSelected == null || string.IsNullOrEmpty(productSelected.Account))
            {
                XtraMessageBox.Show("É necessário carregar uma conta para acessar o menu.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Registro.MensagemPermanente.Listar at = new Registro.MensagemPermanente.Listar();
                DialogResult drResult = at.ShowDialog(this);
            }
        }

        private void btnAddTelefone_Click(object sender, EventArgs e)
        {
            try
            {
                BloqueiaAcao();
                AdicionarTelefone at = new AdicionarTelefone();
                DialogResult drResult = at.ShowDialog(this);
                if (drResult == DialogResult.OK)
                {
                    Splash.Visible(splashScreenManager1, true);
                    splashScreenManager1.SetWaitFormDescription("Atualizando informações cadastrais...");
                    PopulaBoxTelefone();
                    Splash.Visible(splashScreenManager1, false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("Erro ao atualizar telefones");
            }
            finally
            {
                Splash.Visible(splashScreenManager1, false);
                HabilitaAcao(this.productSelected.DisponivelCobranca);
            }
        }

        private bool Promisse()
        {
            try
            {
                Splash.Visible(splashScreenManager1, true);
                splashScreenManager1.SetWaitFormDescription("Registrando Promessa...");
                var workedProduct = WorkedProduct.Where(p => p.Produto == productSelected).FirstOrDefault();
                var telefonePrincipal = Person.Phones.Where(p => p.IdPhoneStatus == 1 || p.IdPhoneStatus == 3).OrderByDescending(p => p.IdPhoneStatus).FirstOrDefault();
                if (telefonePrincipal == null && Person.Phones.Count > 0)
                {
                    telefonePrincipal = Person.Phones.OrderByDescending(p => p.IdPhoneStatus).FirstOrDefault();
                }
                if (telefonePrincipal == null)
                {
                    XtraMessageBox.Show("É necessário selecionar o telefone preferencial do cliente!.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Adicionando StatusLead
                var result = FisAPI.PostStatusLead(workedProduct.StatusLead, productSelected.CPF, telefonePrincipal.NrPhone, User.OAuth.access_token);

                btnPromessa.Text = "Promessa";
                btnPromessa.Appearance.BackColor = btnAcordo.Appearance.BackColor;
                return true;
            }
            catch (Exception ex)
            {
                Splash.Visible(splashScreenManager1, false);
                XtraMessageBox.Show("Erro ao registrar promessa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

            }
        }

    }

    public class Telefone
    {
        public long idTelefone { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public bool Preferencial { get; set; }
        public bool Ativo { get; set; }
    }
}

