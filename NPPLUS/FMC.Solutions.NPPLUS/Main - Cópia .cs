using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using FMC.IBI.BLL;
using FMC.IBI.DAO.Model;
using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.Class.Model;
using FMC.Solutions.NPPLUS.Library.Dialer;
using FMC.Systems.MainframeURA;
using FMC.Systems.P2.Screens;
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
using System.Threading;
using System.Windows.Forms;
using static FMC.Systems.P2.Screens.OCMI;

namespace FMC.Solutions.NPPLUS
{
    public partial class Main : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        //Stopwatch stopWatch = new Stopwatch();
        private Thread _bwThreadConcluir;
        private Thread _bwThreadProduto;
        private Thread _bwThreadConsultaP2;

        Stopwatch watchLigacao = new Stopwatch();
        bool LoadAccount = false;
        string timeLoading = "TEMPO PARA CARREGAMENTO DA CONTA" + Environment.NewLine + Environment.NewLine;
        public LoadType TipoPesquisa;
        public string NumeroPesquisa;
        public ICollection<Library.Class.Model.Produto> products = new Collection<Library.Class.Model.Produto>();
        private string cardSelected = string.Empty;
        public Library.Class.Model.Produto productSelected;
        VA87 va87;
        public ARXQ arxq;
        public ARQN arqn;
        private static Lead lead;
        private static ComplementIBI complementIBI;
        public Lead Lead { get { return lead; } }
        public ComplementIBI ComplementIBI { get { return complementIBI; } }
        public ICollection<WorkedProduct> WorkedProduct = new Collection<WorkedProduct>();
        public User user;
        private ICollection<ExtratoDetalhado> extratoDetalhado = null;
        ICollection<MemoHistory> ocorrenciaResumida = null;
        ICollection<ActionHistory> ocorrenciaDetalhada = null;

        PrivateFontCollection pFontCollection = new PrivateFontCollection();

        public string Token;

        public Main()
        {
            InitializeComponent();
        }

        public Main(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Main_StyleChanged(object sender, EventArgs e)
        {
            var skin = UserLookAndFeel.Default.SkinName;

            if (user.UserProfileScreen == null || user.UserProfileScreen.Count == 0)
            {
                UserBLL userBLL = new UserBLL();
                var usuario = userBLL.GetBykey(user.IdUser);
                usuario.UserProfileScreen.Add(new UserProfileScreen
                {
                    FlEstatementDetail = true,
                    FlOccurrence = true,
                    FlOccurrenceDetail = true,
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
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                lblTotalAccountDelay.Text = "";
                lblTotalAccountDelay.Font = CustomFont("BarlowSemiCondensed-Regular", "ttf", 8, FontStyle.Regular, GraphicsUnit.Point);
                if (user.UserProfileScreen.Count > 0)
                {
                    //UserLookAndFeel.Default.UseDefaultLookAndFeel = false;
                    UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                    UserLookAndFeel.Default.SetSkinStyle(user.UserProfileScreen.FirstOrDefault().Theme);
                }

                if (user.FlLoginDialer)
                {
                    gcDiscador.Enabled = true;

                    //Verificar Token
                    LoginDialer();
                }
                else
                {
                    gcDiscador.Enabled = false;
                }
                /** BGW **/
                bwConsultaP2.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bwConsultaP2_RunWorkerCompleted);
                bwConsultaP2.ProgressChanged -= new ProgressChangedEventHandler(bwConsultaP2_ProgressChanged);
                bwConsultaP2.DoWork -= new DoWorkEventHandler(bwConsultaP2_DoWork);
                bwProduto.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bwProduto_RunWorkerCompleted);
                bwProduto.ProgressChanged -= new ProgressChangedEventHandler(bwProduto_ProgressChanged);
                bwProduto.DoWork -= new DoWorkEventHandler(bwProduto_DoWork);
                bwConsultaP2.Dispose();
                bwProduto.Dispose();

            }
            catch (Exception ex)
            {

            }
            UserLookAndFeel.Default.StyleChanged += Main_StyleChanged;
            SkinHelper.InitSkinPopupMenu(SkinsLink);

            //staticName.Caption = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
            staticName.Caption = user.NmUser;
            staticVersion.Caption = " v." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            pbBoxProduto.Hide();

            //UserLookAndFeel.Default.UseDefaultLookAndFeel = true;
            //UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            //UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
        }

        private Font CustomFont(string font, string extension, float size, FontStyle style, GraphicsUnit unit)
        {
            pFontCollection.AddFontFile(Path.Combine(Application.StartupPath, "Library", "Fonts", font + "." + extension));
            //Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin"))
            return new Font(pFontCollection.Families[0], size, style, unit);
        }

        private void btnAcordo_Click(object sender, EventArgs e)
        {
            if (productSelected.DiasAtraso >= AppSettings.QtdDiasHabilitarAcordo || arxq.AgreementHistory.Count > 0)
            {
                //Acordo acordo = new Acordo(this);
                if (arqn.Page2.Maint?.AddDays(AppSettings.QtdDiasCadastroOk) < DateTime.Today)
                {
                    XtraMessageBox.Show("É necessário Atualizar o Cadastro para realizar o Acordo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnAtualizarCadastro.PerformClick();
                }
                else
                {
                    Acordo acordo = new Acordo();
                    DialogResult drAcordo = acordo.ShowDialog(this);
                    if (drAcordo == DialogResult.OK)
                    {
                        pbBoxProduto.Show();
                        bwAcordo.RunWorkerAsync();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Produto não disponível para realização de ACORDO.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPromessa_Click(object sender, EventArgs e)
        {
            Promessa promessa = new Promessa();
            DialogResult drResult = promessa.ShowDialog(this);
            if (drResult == DialogResult.OK)
            {
                pbBoxProduto.Show();
                bwPromessa.RunWorkerAsync("PROMESSA");
            }
        }

        private void btnBoleto_Click(object sender, EventArgs e)
        {
            Boleto boleto = new Boleto(this);
            DialogResult drResult = boleto.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                pbBoxProduto.Show();
                lead = new LeadBLL().GetBykey(lead.IdLead);
                bwPromessa.RunWorkerAsync("BOLETO");
            }
        }

        private void btnAtualizarCadastro_Click(object sender, EventArgs e)
        {
            AtualizarEndereco ae = new AtualizarEndereco(this);
            DialogResult drResult = ae.ShowDialog();
            if (drResult == DialogResult.OK)
            {
                pbBoxProduto.Show();
                bwAtualizarCadastro.RunWorkerAsync();
            }
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



        private void barbtnLoadConta_ItemClick(object sender, ItemClickEventArgs e)
        {
            CarregarConta carregarConta = new CarregarConta();
            DialogResult resultCarregarConta = carregarConta.ShowDialog(this);

            if (resultCarregarConta == DialogResult.OK)
                if (TipoPesquisa != LoadType.NULL && !string.IsNullOrEmpty(NumeroPesquisa))
                {
                    barbtnLoadConta.Enabled = false;
                    //bwConsultaP2.~CancelAsync()~;
                    //bwProduto.CancelAsync();
                    if (CarregarConta())
                    {
                        bwProduto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwProduto_RunWorkerCompleted);
                        bwProduto.ProgressChanged += new ProgressChangedEventHandler(bwProduto_ProgressChanged);
                        bwProduto.DoWork += new DoWorkEventHandler(bwProduto_DoWork);
                        bwProduto.RunWorkerAsync();
                    }
                    //Produto();
                }
            //Pesquisa
            //ddListaCartao.DropDownControl = CreateListaCartaoPopupMenu();
            //cbContas.Properties.Items.AddRange();
            //LoadAccount loadAccount = new LoadAccount();
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

        private void Produto()
        {
            try
            {
                bwProduto.ReportProgress(0, "Clear");

                var transaction = new TransactionMainframeURA(AppSettings.MainframeUra_Host, AppSettings.MainframeUra_Port);
                if (TipoPesquisa == LoadType.CPF)
                {
                    ProductUR86(transaction, NumeroPesquisa);
                    ProductUR84(transaction, NumeroPesquisa);
                }
                else if (TipoPesquisa == LoadType.CARD)
                {
                    Systems.MainframeURA.UR80 ur80 = ProductUR80(transaction);
                    if (ur80 != null)
                    {
                        ProductUR86(transaction, ur80.NumeroCPF);
                        ProductUR84(transaction, ur80.NumeroCPF);
                    }
                    else
                    {
                        XtraMessageBox.Show("Conta não encontrada. Favor pesquisar por CPF ou Conta", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (TipoPesquisa == LoadType.ACCOUNT)
                {
                    Product product = new ProductBLL().GetByDsProduct(NumeroPesquisa);
                    if (product != null)
                    {
                        ProductUR86(transaction, product.Account.NrCNPJCPF);
                        ProductUR84(transaction, product.Account.NrCNPJCPF);
                    }
                    else
                    {
                        XtraMessageBox.Show("Conta não encontrada na base de dados. Favor pesquisar por CPF ou Cartão", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //bwConsultaP2.CancelAsync();
                        //bwProduto.CancelAsync();
                    }
                }
                //Organizando os produtos encontrados por maior dia de atraso
                if (products != null && products.Count > 0)
                {
                    products = products.OrderByDescending(p => p.DiasAtraso).ThenByDescending(p => p.Card).ToList();
                    //var newProducts = new Collection<Library.Class.Model.Produto>();
                    string lastAccount = string.Empty;
                    foreach (var item in products.ToList())
                    {
                        if (item.Account == lastAccount)
                            products.Remove(item);
                        lastAccount = item.Account;
                    }
                    //products = newProducts;
                    bwProduto.ReportProgress(0, "Popular");
                }
                else
                {
                    XtraMessageBox.Show("Nenhum produto encontrado! Refaça a busca.", "Problemas durante a busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bwProduto.ReportProgress(0, "Stop");
                    //bwConsultaP2.CancelAsync();
                    //bwProduto.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR PRODUTO [Produto()]");
                throw ex;
            }
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
                    lblTotalAccountDelay.ForeColor = Color.Black;
                }
            }
        }

        private void ConsultaP2()
        {
            try
            {
                //Consultar VA87
                va87 = SessionP2.Session.VA87(productSelected.Card.Trim(), productSelected.OrgCMS.Trim());
                foreach (var prod in products)
                    prod.CPF = va87.Documento;

                //Box Dados Cadastrais
                bwConsultaP2.ReportProgress(6, new object[] { "PopulaBoxDadosCadastrais", va87 });
                bwConsultaP2.ReportProgress(7, new object[] { "PopulaBoxResumoCobranca", va87 });

                //Consultar VA82
                VA82 va82 = SessionP2.Session.VA82(productSelected.Card.Trim(), productSelected.OrgCMS);

                //Extrato Resumido
                bwConsultaP2.ReportProgress(0, new object[] { "PopulaExtratoResumido", va82 });


                //Fatura a ser cobrada é a atual
                bool cobrarAnterior = true;
                if (DateTime.Today >= va82.DataVencimento)
                {
                    bwConsultaP2.ReportProgress(2, new object[] { "PopulaBoxResumoCobranca", va82 });
                    cobrarAnterior = false;
                }

                //Fatura Anterior
                va82.GoFaturaAnterior1();
                bwConsultaP2.ReportProgress(3, new object[] { "PopulaExtratoResumido", va82 });

                //Fatura a ser cobrada é a anterior
                if (cobrarAnterior)
                    bwConsultaP2.ReportProgress(4, new object[] { "PopulaBoxResumoCobranca", va82 });

                va82.GoProximaFatura();
                bwConsultaP2.ReportProgress(5, new object[] { "PopulaExtratoResumido", va82 });
                //Fim VA82


                //Consultar VO26
                VO26 vo26 = SessionP2.Session.VO26(productSelected.Account.Trim(), productSelected.OrgCMS.Trim());
                //Box Restrições
                bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxRestricao", vo26 });

                //Consultar ARXQ
                arxq = SessionP2.Session.ARXQ(productSelected.Account.Trim());
                arxq.CarregaAcordos(productSelected.Account.Trim(), true);
                bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxResumoCobranca", arxq });

                if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlEstatementDetail))
                {
                    //Consultar VA81
                    VA81 va81 = SessionP2.Session.VA81(productSelected.Card.Trim(), productSelected.OrgCMS.Trim());
                    extratoDetalhado = va81.ListaExtratoDetalhado().OrderByDescending(p => p.DataFatura).ToList();
                    bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxExtratoDetalhado", extratoDetalhado });
                }
                //Consultar OCMI
                try
                {
                    if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrence))
                    {
                        OCMI ocmi = SessionP2.Session.OCMI(productSelected.Account.Trim(), productSelected.OrgCMS.Trim());
                        ocorrenciaResumida = ocmi.Memos();
                        bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxOcorrenciaSimplificada", ocorrenciaResumida });
                    }

                    if (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrenceDetail))
                    {
                        OCMI ocmi = SessionP2.Session.OCMI(productSelected.Account.Trim(), productSelected.OrgCMS.Trim());
                        ocorrenciaDetalhada = ocmi.Actions();
                        bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxOcorrenciaDetalhada", ocorrenciaDetalhada });
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "CONTA NÃO CARREGA NA OCMI")
                    {
                        XtraMessageBox.Show("Esta conta está indisponível para registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Consultar ARQN
                arqn = SessionP2.Session.ARQN(productSelected.Account.Trim(), productSelected.OrgCMS.Trim());
                //Box Restrições
                bwConsultaP2.ReportProgress(0, new object[] { "IconeBotaoAtualizarCadastro", arqn });

                //depois de integrado com o discador passar o id da lead importada
                LoadLead(null);

                //Carregar Telefones da Conta
                bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxTelefone", null });

            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR CONSULTA P2 [ConsultaP2]");
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

            //PopulaBoxProduto();
            bwConsultaP2.ReportProgress(0, new object[] { "PopulaBoxProduto" });
        }

        private void PopulaExtratoResumido(VA82 va82)
        {
            if (va82.Page == VA82.CurrentPage.FaturaAnterior1 && va82.PrefixoData != null)
            {
                lblDataMesAnterior.Text = va82.DataVencimento?.ToString("dd/MM/yyyy");
                lblSaldoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoMesAnterior.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);

            }
            else if (va82.Page == VA82.CurrentPage.FaturaCorrente && va82.PrefixoData != null)
            {
                lblDataFaturaAtual.Text = va82.DataVencimento?.ToString("dd/MM/yyyy");
                lblSaldoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoFaturaAtual.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);

            }
            else if (va82.Page == VA82.CurrentPage.ProximaFatura && va82.PrefixoData != null)
            {
                lblDataAFaturar.Text = va82.DataVencimento?.ToString("dd/MM/yyyy");
                lblSaldoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
                lblValorMinimoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
                lblPagamentoAFaturar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.Pagamento);

            }
            Application.DoEvents();
        }

        private void PopulaBoxResumoCobranca(VA87 va87)
        {
            lblResumoDataCadastro.Text = va87.DataCadastro.ToString("dd/MM/yyyy");
            lblResumoDiasAtraso.Text = productSelected.DiasAtraso.ToString();
            Application.DoEvents();
        }

        private void PopulaBoxResumoCobranca(VA82 va82)
        {
            lblResumoDataVencimento.Text = va82.DataVencimento?.ToString("dd/MM/yyyy");
            lblResumoValorMinimo.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorMinimo);
            lblResumoValorTotal.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", va82.ValorAtual);
            Application.DoEvents();

        }

        private void PopulaBoxResumoCobranca(ARXQ arxq)
        {
            if (arxq != null && arxq.AgreementHistory != null && arxq.AgreementHistory.Count > 0)
            {
                AgreementHistory ah = arxq.AgreementHistory.FirstOrDefault();
                if (ah != null)
                {
                    StatusAcordoBLL statusAcordoBll = new StatusAcordoBLL();
                    TipoBloqueioBLL tipoBloqueioBll = new TipoBloqueioBLL();
                    StatusAcordo statusAcordo = statusAcordoBll.GetByCod(ah.IndAgreement);

                    TipoBloqueio tipoBloqueio = null;
                    if (!string.IsNullOrEmpty(ah.DetailAgreement.CurrntBlkCd2))
                        tipoBloqueio = tipoBloqueioBll.GetByCod(ah.DetailAgreement.CurrntBlkCd2);
                    if (tipoBloqueio == null && !string.IsNullOrEmpty(ah.DetailAgreement.CurrntBlkCd1))
                        tipoBloqueio = tipoBloqueioBll.GetByCod(ah.DetailAgreement.CurrntBlkCd1);

                    if (tipoBloqueio != null)
                        lblBloqueio.Text = tipoBloqueio.DsTipoBloqueio;
                    else
                        lblBloqueio.Text = "-";

                    if (statusAcordo != null)
                        lblSituacaoAcordo.Text = statusAcordo.DsStatusAcordo;
                    else
                        lblSituacaoAcordo.Text = "-";
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
            Application.DoEvents();
        }

        private void PopulaBoxProduto()
        {
            this.lblTextSaldo.Text = "Fatura Fechada";
            if (productSelected.DiasAtraso > 0) this.lblTextSaldo.Text = "Fatura Fechada";
            this.lblDiasAtraso.Text = productSelected.DiasAtraso.ToString();
            this.lblDiaVencimento.Text = productSelected.DiaVencimento.ToString();
            this.lblSaldo.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", productSelected.Saldo);
            Application.DoEvents();

        }

        private void PopulaBoxDadosCadastrais(VA87 va87)
        {
            lblNomeCliente.Text = va87.Nome;
            lblDataNascimento.Text = va87.DataNascimento.ToString("dd/MM/yyy");
            lblCPF.Text = Convert.ToUInt64(va87.Documento).ToString(@"000\.000\.000\-00");
            lblSexo.Text = va87.SexoCliente;


            lblNomeMae.Text = va87.Mae;
            lblNomeConjuge.Text = (string.IsNullOrWhiteSpace(va87.NomeConjuge.Trim()) || string.IsNullOrEmpty(va87.NomeConjuge.Trim())) ? "-" : va87.NomeConjuge;
            if (va87.DataNascimentoConjuge != null)
                lblDtNascConjuge.Text = va87.DataNascimentoConjuge?.ToString("dd/MM/yyyy");
            else
                lblDtNascConjuge.Text = "-";
            Application.DoEvents();


        }

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

        private void PopulaBoxOcorrenciaSimplificada(ICollection<MemoHistory> memoHistory, bool view)
        {
            ICollection<User> listOperador = new Collection<User>();
            if (memoHistory != null && memoHistory.Count > 0)
                listOperador = new UserBLL().GetByCicsCode(memoHistory.Select(p => p.Col).ToList());
            foreach (var memo in memoHistory)
            {
                if (memo.Col == "SYS")
                    memo.User = AppSettings.UserNameSYS.ToUpper();
                else
                {
                    User operador = null;
                    if (listOperador.Count > 0)
                        operador = listOperador.Where(p => p.Non_Oper_CICS_Cod == memo.Col).FirstOrDefault();
                    if (operador != null)
                        memo.User = operador.NmUser.ToUpper();
                    else
                        memo.User = "-";
                }
            }

            if (view)
            {
                gcOcorrenciaSimplificada.DataSource = memoHistory.OrderByDescending(p => p.Index);
                gcOcorrenciaSimplificada.RefreshDataSource();

                //Habilitar o botão das ocorrências simplificadas
                this.btnShowOcorrenciaSimplificada.ImageOptions.SvgImage = svgImageCollection[6];
                //Alterando ícone do box ocorrências
                if (gcOcorrenciaDetalhada.DataSource == null)
                    gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[4];
                else
                    gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[3];

                Application.DoEvents();
            }

        }

        private void PopulaBoxOcorrenciaDetalhada(ICollection<ActionHistory> actionHistory, bool view)
        {
            ICollection<User> listOperador = new Collection<User>();
            if (actionHistory != null && actionHistory.Count > 0)
                listOperador = new UserBLL().GetByCicsCode(actionHistory.Select(p => p.Col).ToList());
            foreach (var action in actionHistory)
            {
                if (action.Col == "SYS")
                    action.User = AppSettings.UserNameSYS.ToUpper();
                else
                {
                    User operador = null;
                    if (listOperador.Count > 0)
                        operador = listOperador.Where(p => p.Non_Oper_CICS_Cod == action.Col).FirstOrDefault();
                    if (operador != null)
                        action.User = operador.NmUser.ToUpper();
                    else
                        action.User = "-";
                }
            }
            if (view)
            {
                gcOcorrenciaDetalhada.DataSource = actionHistory.OrderByDescending(p => p.Date).ThenByDescending(p => p.Time).ToList();
                gcOcorrenciaDetalhada.RefreshDataSource();

                //Habilitar o botão das ocorrências detalhadas
                this.btnShowOcorrenciaDetalhada.ImageOptions.SvgImage = svgImageCollection[6];
                //Alterando ícone do box ocorrências
                if (gcOcorrenciaSimplificada.DataSource == null)
                    gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[4];
                else
                    gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[3];
            }
            Application.DoEvents();
        }

        private void PopulaBoxTelefone()
        {
            try
            {
                ICollection<Telefone> listaTelefone = new HashSet<Telefone>();
                string telefone;
                if (!string.IsNullOrEmpty(arqn.Page2.HomePhoneNumber.Trim()))
                {
                    telefone = PhoneNumberFormat.PhoneNumber(arqn.Page2.HomePhoneNumber);
                    if (!string.IsNullOrEmpty(telefone) && telefone.Length >= 8)
                        listaTelefone.Add(new Telefone
                        {
                            Phone = telefone,
                            Tipo = telefone.Substring(5, 1) == "9" || telefone.Substring(5, 1) == "8" ? "Celular" : "Fixo"
                        });
                }
                if (!string.IsNullOrEmpty(arqn.Page2.MobilePhoneNumber.Trim()))
                {
                    telefone = PhoneNumberFormat.PhoneNumber(arqn.Page2.MobilePhoneNumber);
                    if (!string.IsNullOrEmpty(telefone) && telefone.Length >= 8)
                        listaTelefone.Add(new Telefone
                        {
                            Phone = telefone,
                            Tipo = telefone.Substring(5, 1) == "9" || telefone.Substring(5, 1) == "8" ? "Celular" : "Fixo"
                        });
                }
                if (!string.IsNullOrEmpty(arqn.Page2.MobilePhoneNumber.Trim()))
                {
                    telefone = PhoneNumberFormat.PhoneNumber(arqn.Page2.FaxPhoneNumber);
                    if (!string.IsNullOrEmpty(telefone) && telefone.Length >= 8)
                        listaTelefone.Add(new Telefone
                        {
                            Phone = telefone,
                            Tipo = telefone.Substring(5, 1) == "9" || telefone.Substring(5, 1) == "8" ? "Celular" : "Fixo"
                        });
                }
                foreach (var phone in Lead.Product.Account.Phone)
                {
                    string p = phone.NrPhone;
                    if (p.Length >= 11)
                        p = Convert.ToUInt64(p).ToString("(00) 00000-0000");
                    else
                        p = Convert.ToUInt64(p).ToString("(00) 0000-0000");

                    string tipo = string.Empty;
                    if (phone.IdPhoneType == 1)
                        tipo = "Residencial";
                    if (phone.IdPhoneType == 2)
                        tipo = "Celular";
                    if (phone.IdPhoneType == 3)
                        tipo = "Comercial";
                    if (phone.IdPhoneType == 4)
                        tipo = "Referência";

                    listaTelefone.Add(
                        new Telefone
                        {
                            Phone = p,
                            Tipo = tipo
                        });
                }
                gcTelefones.DataSource = listaTelefone;
                Application.DoEvents();

            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO POPULAR BOX TELEFONE [PopulaBoxTelefone]");
                throw e;
            }
        }

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

        //private void Timer(string txt, bool restartTimer = false)
        //{
        //    if (restartTimer)
        //        stopWatch.Stop();

        //    TimeSpan ts = stopWatch.Elapsed;
        //    string elapsedTime = String.Format("{0:00}.{1:0000}",
        //    ts.Seconds,
        //    ts.Milliseconds);
        //    timeLoading += txt + ": " + elapsedTime + Environment.NewLine;

        //    if (restartTimer)
        //    {
        //        stopWatch.Reset();
        //        stopWatch.Start();
        //    }
        //}

        private Systems.MainframeURA.UR80 ProductUR80(TransactionMainframeURA transaction)
        {
            return transaction.GetUR80(NumeroPesquisa, "000", false);
        }

        private void ProductUR86(TransactionMainframeURA transaction, string cpf)
        {
            string textFormat = "{0} - {1} - {2} DIAS";

            Systems.MainframeURA.UR86 ur86 = transaction.GetUR86(cpf.Trim(), TransactionMainframeURA.ConsultType.CPF, false);
            LogoBLL logoBll = new LogoBLL();
            if (ur86.Detail != null && ur86.Detail.Count > 0)
            {
                foreach (var detail in ur86.Detail.Where(p => p.IndicadorTitularAdicional == "0"))
                {
                    Logo logo = logoBll.GetByLogoOrg(Convert.ToInt32(detail.Logo), Convert.ToInt32(detail.OrgConta));
                    string card = Convert.ToUInt64(detail.NumeroCartao).ToString("0000 0000 0000 0000");
                    var select = string.Format(textFormat, card, logo != null ? logo.dscProduct.Trim() : "Ibi", Convert.ToInt32(detail.DiasAtraso));

                    products.Add(new Library.Class.Model.Produto
                    {
                        Select = select,
                        Account = detail.NumeroConta,
                        Card = detail.NumeroCartao,
                        Saldo = detail.SaldoDevedor,
                        DiasAtraso = Convert.ToInt32(detail.DiasAtraso),
                        DiaVencimento = Convert.ToInt32(detail.DataVencimento.Substring(0, 2)),
                        OrgCMS = detail.OrgConta,
                        OrgConta = detail.OrgConta,
                        LogoCMS = detail.Logo
                    });
                }
            }
        }

        private void ProductUR84(TransactionMainframeURA transaction, string cpf)
        {
            string textFormat = "{0} - {1} - {2} DIAS";
            Systems.MainframeURA.UR84 ur84 = transaction.GetUR84(cpf.Trim(), TransactionMainframeURA.ConsultType.CPF, false);
            LogoBLL logoBll = new LogoBLL();
            if (ur84.Detail.Count > 0)
            {
                foreach (var detail in ur84.Detail)
                {
                    if (!products.Any(p => p.Card.Contains(detail.NumeroCartao.Substring(0, 13))))
                    {
                        Systems.MainframeURA.UR86 ur86x = transaction.GetUR86(detail.NumeroCartao, TransactionMainframeURA.ConsultType.CARD, false);
                        if (ur86x.Detail != null && ur86x.Detail.Count > 0)
                        {
                            foreach (var detailx in ur86x.Detail)
                            {
                                Logo logo = logoBll.GetByLogoOrg(Convert.ToInt32(detailx.Logo), Convert.ToInt32(detailx.OrgConta));
                                string card = Convert.ToUInt64(detailx.NumeroCartao).ToString("0000 0000 0000 0000");
                                var select = string.Format(textFormat, card, logo != null ? logo.dscProduct.Trim() : "Ibi", Convert.ToInt32(detailx.DiasAtraso));
                                products.Add(new Library.Class.Model.Produto
                                {
                                    Select = select,
                                    Card = detailx.NumeroCartao,
                                    Account = detailx.NumeroConta,
                                    Saldo = detailx.SaldoDevedor,
                                    DiasAtraso = Convert.ToInt32(detail.DiasAtraso),
                                    DiaVencimento = Convert.ToInt32(detailx.DataVencimento.Substring(0, 2)),
                                    OrgCMS = detailx.OrgConta
                                });
                            }
                        }
                    }
                    else
                    {
                        var prod = products.Where(p => p.Card.Contains(detail.NumeroCartao.Substring(0, 13))).FirstOrDefault();
                        if (prod != null && prod.DiasAtraso >= 999)
                        {
                            prod.DiasAtraso = Convert.ToInt32(detail.DiasAtraso);
                            prod.Select = prod.Select.Replace("999", Convert.ToInt32(detail.DiasAtraso).ToString());
                        }
                    }
                }
            }
        }

        private void LimparBoxProduto()
        {
            cbListaCartao.Properties.Items.Clear();
            cbListaCartao.SelectedIndex = -1;
            lblSaldo.Text = "-";
            lblDiasAtraso.Text = "-";
            lblDiaVencimento.Text = "-";
            //pbBoxProduto.Refresh();
            pbBoxProduto.Show();
        }

        private void LimparBoxDadosCadastrais()
        {
            lblNomeCliente.Text = "-";
            lblDataNascimento.Text = "-";
            lblCPF.Text = "-";
            lblSexo.Text = "-";
            lblNomeMae.Text = "-";
            lblNomeConjuge.Text = "-";
            lblDtNascConjuge.Text = "-";
        }

        private void LimparBoxRestricoes()
        {
            lblSPCIncluso.Text = "-";
            lblSPCDtInclusao.Text = "-";
            lblSPCDtExclusao.Text = "-";
            lblSERASAIncluso.Text = "-";
            lblSERASADtInclusao.Text = "-";
            lblSERASADtExclusao.Text = "-";
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
        }

        private void LimparBoxSituacaoCobranca()
        {
            lblResumoDataCadastro.Text = "-";
            lblResumoDiasAtraso.Text = "-";
            lblResumoDataVencimento.Text = "-";
            lblResumoValorMinimo.Text = "-";
            lblResumoValorTotal.Text = "-";
            lblSituacaoAcordo.Text = "-";
            lblBloqueio.Text = "-";
        }

        private void LimparBoxExtratoDetalhado()
        {
            gcExtratoDetalhado.DataSource = null;
            gcExtratoDetalhado.RefreshDataSource();
            gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[2];
        }

        private void LimparBoxOcorrenciaSimplificada()
        {

            gcOcorrenciaSimplificada.DataSource = null;
            gcOcorrenciaSimplificada.RefreshDataSource();
            gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[2];

            this.btnShowOcorrenciaSimplificada.ImageOptions.SvgImage = svgImageCollection[5];
            //Alterando ícone do box ocorrências
            if (gcOcorrenciaDetalhada.DataSource == null)
                gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[2];
            else
                gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[4];
        }

        private void LimparBoxOcorrenciaDetalhada()
        {
            gcOcorrenciaDetalhada.DataSource = null;
            gcOcorrenciaDetalhada.RefreshDataSource();

            this.btnShowOcorrenciaDetalhada.ImageOptions.SvgImage = svgImageCollection[5];
            //Alterando ícone do box ocorrências
            if (gcOcorrenciaSimplificada.DataSource == null)
                gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[2];
            else
                gcOcorrencias.CustomHeaderButtons[0].Properties.ImageOptions.SvgImage = svgImageCollection[4];

        }

        private void LimparBoxTelefone()
        {
            gcTelefones.DataSource = null;
            gcTelefones.RefreshDataSource();
        }

        private void BloqueiaAcao()
        {
            Log.SaveFile("BloqueiaAcao");
            btnAbrirAcordos.Enabled = false;
            btnExtratoResumido.Enabled = false;
            btnPromessa.Enabled = false;
            btnAcordo.Enabled = false;
            btnBoleto.Enabled = false;
            btnAtualizarCadastro.Enabled = false;
            cbListaCartao.Enabled = false;
            gcOcorrencias.CustomHeaderButtons[0].Properties.Enabled = false;
            gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.Enabled = false;
            btnShowOcorrenciaSimplificada.Enabled = false;
            btnShowOcorrenciaDetalhada.Enabled = false;

            btnCopyAccount.Enabled = false;

            btnConcluir.Enabled = false;
        }

        private void HabilitaAcao()
        {
            Log.SaveFile("HabilitaAcao");
            try
            {
                barbtnLoadConta.Enabled = true;
                cbListaCartao.Enabled = true;

                btnAbrirAcordos.Enabled = true;
                btnExtratoResumido.Enabled = true;
                btnAtualizarCadastro.Enabled = true;
                btnBoleto.Enabled = true;
                pbBoxProduto.Hide();
                gcOcorrencias.CustomHeaderButtons[0].Properties.Enabled = true;
                gControlExtratoDetalhado.CustomHeaderButtons[0].Properties.Enabled = true;
                btnShowOcorrenciaSimplificada.Enabled = true;
                btnShowOcorrenciaDetalhada.Enabled = true;
                btnCopyAccount.Enabled = true;

                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();
                if (workedProduct == null || !workedProduct.AccountCompleted)
                {
                    btnAcordo.Enabled = true;
                    btnPromessa.Enabled = true;
                    btnConcluir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO HABILITAR ACÃO [HabilitaAcao()]");
                throw ex;
            }
        }

        /** Lista de Menus dos Produtos **/
        //cbListaCartao.DropDownControl = CreateListaCartaoPopupMenu();
        private DXPopupMenu CreateListaCartaoPopupMenu()
        {
            Log.SaveFile("CreateListaCartaoPopupMenu");
            DXPopupMenu menu = new DXPopupMenu();
            menu.Items.Add(new DXMenuItem("Item", OnItemClick));
            menu.Items.Add(new DXMenuCheckItem("CheckItem", false, null, OnItemClick));
            //DXSubMenuItem subMenu = new DXSubMenuItem("SubMenu");
            //subMenu.Items.Add(new DXMenuItem("SubItem", OnItemClick));
            //menu.Items.Add(subMenu);
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

        private void ReloadArxq()
        {
            Log.SaveFile("ReloadArxq");
            try
            {
                //Consultar ARXQ
                arxq = SessionP2.Session.ARXQ(productSelected.Account);
                arxq.CarregaAcordos(productSelected.Account, true);
                bwAcordo.ReportProgress(0, new object[] { "PopulaBoxResumoCobranca", arxq });
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO CARREGAR ARXQ [ReloadArxq]");
                throw e;
            }
        }

        private void ReloadOcmi()
        {
            Log.SaveFile("ReloadOcmi");
            //Consultar OCMI
            try
            {
                OCMI ocmi = SessionP2.Session.OCMI(productSelected.Account, productSelected.OrgCMS);
                ocorrenciaResumida = ocmi.Memos();
                ocorrenciaDetalhada = ocmi.Actions();

                bwPromessa.ReportProgress(0, new object[] { "PopulaBoxOcorrenciaSimplificada", ocorrenciaResumida });
                bwPromessa.ReportProgress(0, new object[] { "PopulaBoxOcorrenciaDetalhada", ocorrenciaDetalhada });
            }
            catch (Exception e)
            {
                if (e.Message == "CONTA NÃO CARREGA NA OCMI")
                {
                    XtraMessageBox.Show("Esta conta está indisponível para registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ReloadOcmi(string type, bool required = false)
        {
            Log.SaveFile("ReloadOcmi");
            //Consultar OCMI
            try
            {
                if (ocorrenciaResumida == null || ocorrenciaDetalhada == null || required)
                {
                    OCMI ocmi = SessionP2.Session.OCMI(productSelected.Account, productSelected.OrgCMS);
                    if (type == "Resumido")
                        ocorrenciaResumida = ocmi.Memos();

                    if (type == "Detalhado")
                        ocorrenciaDetalhada = ocmi.Actions();
                }
            }
            catch (Exception e)
            {
                if (e.Message == "CONTA NÃO CARREGA NA OCMI")
                {
                    XtraMessageBox.Show("Esta conta está indisponível para registro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void ReloadARQN()
        {
            Log.SaveFile("ReloadARQN");
            try
            {
                //Consultar ARQN
                arqn = SessionP2.Session.ARQN(productSelected.Account.Trim(), productSelected.OrgCMS.Trim());
                //Box Restrições
                bwAtualizarCadastro.ReportProgress(0, new object[] { "IconeBotaoAtualizarCadastro", arqn });
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO CARREGAR ARQN [ReloadArqn]");
                throw e;
            }
        }


        private void ReloadVA81()
        {
            Log.SaveFile("ReloadVA81");
            try
            {
                //Consultar VA81
                if (extratoDetalhado == null)
                {
                    VA81 va81 = SessionP2.Session.VA81(productSelected.Card.Trim(), productSelected.OrgCMS.Trim());
                    extratoDetalhado = va81.ListaExtratoDetalhado().OrderByDescending(p => p.DataFatura).ToList();
                }
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO CARREGAR ARXQ [ReloadVA81]");
                throw e;
            }
        }

        private void cbListaCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.SaveFile("cbListaCartao_SelectedIndexChanged");
            var cListaCartao = sender as ComboBoxEdit;
            cardSelected = string.Empty;
            if (cListaCartao.SelectedIndex != -1)
            {
                barbtnLoadConta.Enabled = false;
                cardSelected = cListaCartao.SelectedItem.ToString();
                LimparBoxRestricoes();
                LimparBoxExtratoResumido();
                LimparBoxSituacaoCobranca();
                LimparBoxExtratoDetalhado();
                LimparBoxOcorrenciaSimplificada();
                LimparBoxOcorrenciaDetalhada();
                LimparBoxTelefone();
                BloqueiaAcao();

                if (!bwConsultaP2.IsBusy)
                {

                    bwConsultaP2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConsultaP2_RunWorkerCompleted);
                    bwConsultaP2.ProgressChanged += new ProgressChangedEventHandler(bwConsultaP2_ProgressChanged);
                    bwConsultaP2.DoWork += new DoWorkEventHandler(bwConsultaP2_DoWork);

                    bwConsultaP2.RunWorkerAsync();
                    pbBoxProduto.Show();
                }
            }
        }

        private void bwConsultaP2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //_bwThreadConsultaP2 = Thread.CurrentThread;
                if (bwConsultaP2.CancellationPending)
                {
                    e.Cancel = true;
                    pbBoxProduto.Hide();
                    barbtnLoadConta.Enabled = true;
                }
                else
                {
                    BoxProduto(cardSelected);
                    ConsultaP2();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("THREAD FINALIZADA [bwConsultaP2]");
                Application.DoEvents();
                // throw ex;
            }
        }

        private void bwConsultaP2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwConsultaP2_RunWorkerCompleted");

            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                //bwProduto.CancelAsync();
            }
            HabilitaAcao();
            //AbortThread(_bwThreadConsultaP2);

            bwConsultaP2.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bwConsultaP2_RunWorkerCompleted);
            bwConsultaP2.ProgressChanged -= new ProgressChangedEventHandler(bwConsultaP2_ProgressChanged);
            bwConsultaP2.DoWork -= new DoWorkEventHandler(bwConsultaP2_DoWork);
            bwConsultaP2.Dispose();

            Application.DoEvents();
        }

        private void UpdateIconAtualizarCadastro(ARQN arqn)
        {
            Log.SaveFile("UpdateIconAtualizarCadastro");
            this.btnAtualizarCadastro.ImageOptions.SvgImageSize = new Size(24, 24);
            if (arqn.Page2.Maint != null)
            {
                if (arqn.Page2.Maint?.AddDays(AppSettings.QtdDiasCadastroOk) < DateTime.Today)
                {
                    this.btnAtualizarCadastro.ImageOptions.SvgImage = svgImageCollection[0];
                    this.btnAtualizarCadastro.Text = "Atualizar Cadastro";
                }
                else
                {
                    this.btnAtualizarCadastro.ImageOptions.SvgImage = svgImageCollection[1];
                    this.btnAtualizarCadastro.Text = "Cadastro Atualizado";
                }
            }
            else
            {
                this.btnAtualizarCadastro.ImageOptions.SvgImage = svgImageCollection[0];
                this.btnAtualizarCadastro.Text = "Atualizar Cadastro";
            }
        }

        private void bwConsultaP2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Log.SaveFile("bwConsultaP2_ProgressChanged");
            object[] obj = (object[])e.UserState;
            if (obj[0].ToString() == "PopulaBoxProduto")
            {
                PopulaBoxProduto();
            }
            else if (obj[0].ToString() == "PopulaExtratoResumido")
            {
                PopulaExtratoResumido((VA82)obj[1]);
            }
            else if (obj[0].ToString() == "PopulaBoxResumoCobranca")
            {
                if (obj[1].GetType() == typeof(VA82))
                    PopulaBoxResumoCobranca((VA82)obj[1]);
                else if (obj[1].GetType() == typeof(VA87))
                    PopulaBoxResumoCobranca((VA87)obj[1]);
                else if (obj[1].GetType() == typeof(ARXQ))
                    PopulaBoxResumoCobranca((ARXQ)obj[1]);
            }
            else if (obj[0].ToString() == "PopulaBoxDadosCadastrais")
            {
                PopulaBoxDadosCadastrais((VA87)obj[1]);
            }
            else if (obj[0].ToString() == "PopulaBoxRestricao")
            {
                PopulaBoxRestricao((VO26)obj[1]);
            }
            else if (obj[0].ToString() == "PopulaBoxExtratoDetalhado")
            {
                bool view = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlEstatementDetail)) ? true : false;
                PopulaBoxExtratoDetalhado((ICollection<ExtratoDetalhado>)obj[1], view);
            }
            else if (obj[0].ToString() == "PopulaBoxOcorrenciaSimplificada")
            {
                bool view = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrence)) ? true : false;
                PopulaBoxOcorrenciaSimplificada((ICollection<MemoHistory>)obj[1], view);
            }
            else if (obj[0].ToString() == "PopulaBoxOcorrenciaDetalhada")
            {
                bool view = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrenceDetail)) ? true : false;
                PopulaBoxOcorrenciaDetalhada((ICollection<ActionHistory>)obj[1], view);
            }
            else if (obj[0].ToString() == "IconeBotaoAtualizarCadastro")
            {
                UpdateIconAtualizarCadastro((ARQN)obj[1]);
            }
            else if (obj[0].ToString() == "PopulaBoxTelefone")
            {
                PopulaBoxTelefone();
            }
        }

        private void bwProduto_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //_bwThreadProduto = Thread.CurrentThread;
                if (bwProduto.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    Produto();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("THREAD FINALIZADA [bwProduto]");
                Application.DoEvents();
                //throw ex;
            }
        }

        private void bwProduto_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile("bwProduto_ProgressChanged");
            if (e.UserState.ToString() == "Clear")
            {
                ClearAll();
            }
            else if (e.UserState.ToString() == "Popular")
            {

            }
            else if (e.UserState.ToString() == "Stop")
            {
                pbBoxProduto.Hide();
                barbtnLoadConta.Enabled = true;
            }
        }

        private void bwProduto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwProduto_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwProduto.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bwProduto_RunWorkerCompleted);
                bwProduto.ProgressChanged -= new ProgressChangedEventHandler(bwProduto_ProgressChanged);
                bwProduto.DoWork -= new DoWorkEventHandler(bwProduto_DoWork);
                bwProduto.Dispose();
                Popular();
                //pbBoxProduto~.Hide()~;
            }
            //AbortThread(_bwThreadProduto);


            Application.DoEvents();
        }

        private void ClearAll()
        {
            Log.SaveFile("ClearAll()");
            productSelected = null;
            lblTotalAccountDelay.Text = "";
            lblTotalAccountDelay.ForeColor = Color.Black;

            extratoDetalhado = null;
            ocorrenciaResumida = null;
            ocorrenciaDetalhada = null;

            products = new Collection<Library.Class.Model.Produto>();
            WorkedProduct = new Collection<WorkedProduct>();

            LimparBoxProduto();
            LimparBoxDadosCadastrais();
            LimparBoxRestricoes();
            LimparBoxExtratoResumido();
            LimparBoxSituacaoCobranca();
            LimparBoxExtratoDetalhado();
            LimparBoxOcorrenciaSimplificada();
            LimparBoxOcorrenciaDetalhada();
            LimparBoxTelefone();
            BloqueiaAcao();
        }
        private void LimparBoxDiscador()
        {
            Log.SaveFile("LimparBoxDiscador()");
            //Parando contador de tempo
            watchLigacao.Stop();
            timerLigacao.Stop();
            //
            lblTelefone.Text = "-";
            lblTipoLigacao.Text = "-";
            lblTempo.Text = "-";
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.SaveFile("Main_FormClosing");
            MessageBox logout = new MessageBox("Fechar a Aplicação", "Tem certeza que deseja encerrar a aplicação?", "Ao confirmar, todas as ações pendentes serão perdidas.", false, "Não", "Sim");
            DialogResult msg = logout.ShowDialog(); //XtraMessageBox.Show("Tem certeza que deseja sair da aplicação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.OK)
            {
                //if (!string.IsNullOrEmpty(Token) && user.FlLoginDialer && (Dialer.Bilhete != null && Dialer.Bilhete.Status != Status.Deslogado && Dialer.Bilhete.Status != Status.Erro))
                try
                {
                    Dialer.Logout();
                }
                catch (Exception ex) { }
                SessionP2.Session.Logout();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void bwAcordo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwAcordo.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadArxq();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR ACORDO [bwAcordo_DoWork]");
                throw ex;
            }
        }

        private void bwAcordo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile("bwAcordo_ProgressChanged");
            object[] obj = (object[])e.UserState;
            if (obj[0].ToString() == "PopulaBoxResumoCobranca")
            {
                PopulaBoxResumoCobranca((ARXQ)obj[1]);
            }
        }

        private void bwAcordo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwAcordo_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwAcordo.CancelAsync();
                //pbBoxProduto~.Hide()~;

                MessageBox emitirBoleto = new MessageBox("Solicitação de Ação", "Deseja emitir boleto para este acordo?", "Ao confirmar, a janela do boleto abrirá automaticamente.", false, "Não", "Sim");
                DialogResult msg = emitirBoleto.ShowDialog();
                if (msg == DialogResult.OK)
                    btnBoleto.PerformClick();
            }
            pbBoxProduto.Hide();
            Application.DoEvents();
        }

        private void bwPromessa_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwAcordo.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadOcmi();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR PROMESSA [bwPromessa_DoWork]");
                throw ex;
            }
        }

        private void bwPromessa_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile("bwPromessa_ProgressChanged");
            object[] obj = (object[])e.UserState;
            if (obj[0].ToString() == "PopulaBoxOcorrenciaSimplificada")
            {
                bool view = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrence)) ? true : false;
                PopulaBoxOcorrenciaSimplificada((ICollection<MemoHistory>)obj[1], view);
            }
            else if (obj[0].ToString() == "PopulaBoxOcorrenciaDetalhada")
            {
                bool view = (user.UserProfileScreen == null || (user.UserProfileScreen.Count > 0 && user.UserProfileScreen.FirstOrDefault().FlOccurrenceDetail)) ? true : false;
                PopulaBoxOcorrenciaDetalhada((ICollection<ActionHistory>)obj[1], view);
            }
        }

        private void bwPromessa_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwPromessa_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwPromessa.CancelAsync();
                //pbBoxProduto~.Hide()~;
            }
            pbBoxProduto.Hide();
            Application.DoEvents();
        }

        private void bwAtualizarCadastro_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwAtualizarCadastro.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadARQN();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO ATUALIZAR CADASTRO [bwAtualizarCadastro_DoWork]");
                throw ex;
            }
        }

        private void bwAtualizarCadastro_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile("bwAtualizarCadastro_ProgressChanged");
            object[] obj = (object[])e.UserState;
            if (obj[0].ToString() == "IconeBotaoAtualizarCadastro")
            {
                UpdateIconAtualizarCadastro((ARQN)obj[1]);
            }
        }

        private void bwAtualizarCadastro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwAtualizarCadastro_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwAtualizarCadastro.CancelAsync();
            }
            pbBoxProduto.Hide();
            Application.DoEvents();
        }

        private void cbListaCartao_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            Log.SaveFile("cbListaCartao_DrawItem");
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

        private void LoadLead(int? idLead)
        {
            Log.SaveFile("LoadLead");
            try
            {
                if (idLead != null)
                {
                    lead = new LeadBLL().GetBykey(idLead);
                }
                else
                {
                    Product product = new ProductBLL().GetByDsProduct(productSelected.Account);

                    if (product != null)
                        lead = product.Lead.Where(p => p.DtInsert >= DateTime.Today).OrderByDescending(p => p.DtInsert).FirstOrDefault();

                    if (lead == null)
                    {
                        AgreementHistory ah = arxq.AgreementHistory.FirstOrDefault();
                        lead = new LeadBLL().GetLeadInbound
                        (
                            productSelected.Account,
                            productSelected.CPF,
                            lblNomeCliente.Text.Trim(),
                            new List<long>(),
                            productSelected.Saldo,
                            Convert.ToInt32(productSelected.OrgConta), //ORG CONTA
                            Convert.ToInt32(productSelected.OrgCMS),
                            productSelected.DiaVencimento,
                            Convert.ToInt32(productSelected.LogoCMS), //LOGO CMS
                            ah != null ? ah.IndAgreement : "", //status do acordo
                            productSelected.DiasAtraso,
                            "MANUAL", //QUANDO IDENTIFICAR INBOUND MUDAR PARA "INBOUND"
                            "MANUAL",
                            "NPPLUS"
                        );
                    }
                }
                complementIBI = new ComplementIBIBLL().GetByIdLead(lead.IdLead);
            }
            catch (Exception e)
            {
                Log.SaveFile("ERRO AO CARREGAR LEAD [LoadLead]");
                throw e;
            }
        }

        private void btnDiscar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //ligar para telefone
        }

        private void groupControl9_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Log.SaveFile("groupControl9_CustomButtonClick");
            if (gcExtratoDetalhado.DataSource == null)
            {
                pbBoxProduto.Show();
                BloqueiaAcao();
                bwExtratoDetalhado.RunWorkerAsync();
            }
            else
            {
                LimparBoxExtratoDetalhado();
            }
        }

        private void gcOcorrencias_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Log.SaveFile("gcOcorrencias_CustomButtonClick");
            if (gcOcorrenciaDetalhada.DataSource == null)
            {
                pbBoxProduto.Show();
                BloqueiaAcao();
                bwOcorrencias.RunWorkerAsync();
            }
            else
            {
                LimparBoxOcorrenciaSimplificada();
                LimparBoxOcorrenciaDetalhada();
            }
        }

        private void btnShowOcorrenciaSimplificada_Click(object sender, EventArgs e)
        {
            if (gcOcorrenciaSimplificada.DataSource == null)
            {
                pbBoxProduto.Show();
                BloqueiaAcao();
                bwOcorrenciaSimplificada.RunWorkerAsync();
                //PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida);
            }
            else
            {
                LimparBoxOcorrenciaSimplificada();
                this.btnShowOcorrenciaSimplificada.ImageOptions.SvgImage = svgImageCollection[5];
            }
        }

        private void btnShowOcorrenciaDetalhada_Click(object sender, EventArgs e)
        {
            if (gcOcorrenciaDetalhada.DataSource == null)
            {
                pbBoxProduto.Show();
                BloqueiaAcao();
                bwOcorrenciaDetalhada.RunWorkerAsync();
                //PopulaBoxOcorrenciaDetalhada(ocorrenciaDetalhada);
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
                UserBLL userBLL = new UserBLL();
                user = userBLL.GetBykey(user.IdUser);
            }
        }

        private void btnCopyAccount_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(va87.Nome + "\t" + productSelected.CPF + "\t" + productSelected.Account);
            XtraMessageBox.Show("Informações copiadas com sucesso!", "Informações da Conta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void bwExtratoDetalhado_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwExtratoDetalhado.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadVA81();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR EXTRATO DETALHADO [bwExtratoDetalhado_DoWork]");
                throw ex;
            }
        }

        private void bwExtratoDetalhado_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwExtratoDetalhado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwExtratoDetalhado_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                PopulaBoxExtratoDetalhado(extratoDetalhado, true);
                bwExtratoDetalhado.CancelAsync();
            }
            HabilitaAcao();
            Application.DoEvents();
        }

        private void bwOcorrencias_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwOcorrencias.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadOcmi("Resumido");
                    ReloadOcmi("Detalhado");

                    bwOcorrencias.ReportProgress(0, "Resumido");
                    bwOcorrencias.ReportProgress(0, "Detalhado");
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR OCORRENCIAS [bwOcorrencias_DoWork]");
                throw ex;
            }
        }

        private void bwOcorrencias_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.UserState.ToString() == "Resumido")
            {
                PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, true);
            }
            else if (e.UserState.ToString() == "Detalhado")
            {
                PopulaBoxOcorrenciaDetalhada(ocorrenciaDetalhada, true);
            }
        }

        private void bwOcorrencias_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwOcorrencias_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwOcorrencias.CancelAsync();
            }
            HabilitaAcao();
            Application.DoEvents();
        }

        private void bwOcorrenciaSimplificada_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwOcorrenciaSimplificada.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadOcmi("Resumido");
                    bwOcorrenciaSimplificada.ReportProgress(0, "Resumido");
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR OCORRENCIA SIMPLIFICADA [bwOcorrenciaSimplificada_DoWork]");
                throw ex;
            }
        }

        private void bwOcorrenciaSimplificada_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile("bwOcorrenciaSimplificada_ProgressChanged");

            if (e.UserState.ToString() == "Resumido")
            {
                PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, true);
            }
        }

        private void bwOcorrenciaSimplificada_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwOcorrenciaSimplificada_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwOcorrenciaSimplificada.CancelAsync();
            }
            HabilitaAcao();
            Application.DoEvents();
        }

        private void bwOcorrenciaDetalhada_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwOcorrenciaDetalhada.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    ReloadOcmi("Detalhado");
                    bwOcorrenciaDetalhada.ReportProgress(0, "Detalhado");
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR OCORRENCIA DETALHADA [bwOcorrenciaDetalhada_DoWork]");
                throw ex;
            }
        }
        private void bwOcorrenciaDetalhada_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState.ToString() == "Detalhado")
            {
                PopulaBoxOcorrenciaDetalhada(ocorrenciaDetalhada, true);
            }
        }

        private void bwOcorrenciaDetalhada_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwOcorrenciaDetalhada_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                //XtraMessageBox.Show("Cancelado.");
            }
            else if (!(e.Error == null))
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                bwOcorrenciaDetalhada.CancelAsync();
            }
            HabilitaAcao();
            Application.DoEvents();
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            try
            {

                ///VERIFICA SE CONTA ATUAL POSSUI ACORDO/PROMESSA
                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();

                if (workedProduct == null)
                {
                    Concluir concluir = new Concluir(lead, user, this);
                    DialogResult drConcluir = concluir.ShowDialog(this);
                    if (drConcluir == DialogResult.OK)
                    {
                        pbBoxProduto.Show();
                        //bwConcluir.RunWorkerAsync();
                        MetodoConcluir();
                    }
                }
                else
                {
                    //bwConcluir.RunWorkerAsync();
                    MetodoConcluir();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CLICAR BOTÃO CONCLUIR [btnConcluir_Click]");
                throw ex;
            }
        }

        private void MetodoConcluir()
        {
            try
            {

                //_bwThreadConcluir = Thread.CurrentThread;

                WorkedProduct workedProduct = null;
                if (WorkedProduct != null)
                    workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();


                if (workedProduct != null && !workedProduct.AccountCompleted)
                {
                    //Se acordo, registrar ocal
                    if (workedProduct.Tipo == Library.Class.Model.WorkedProduct.Type.Acordo)
                    {
                        string memo = "ACORDO ";// + txtTelefone.Text;
                        if (workedProduct.DetailPayment.TipoPagamento == DetailPayment.TypePayment.AVista)
                        {
                            memo += "AVISTA " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " PARA " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            memo += "PARCELADO " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM") + " " + workedProduct.DetailPayment.QtdParcel + "*" + workedProduct.DetailPayment.VlParcel?.ToString("N2");
                        }
                        OCAL ocal = SessionP2.Session.OCAL(productSelected.Account, productSelected.OrgCMS);
                        ocal.SetActionOcal("ACD", "", 0, memo, "", SessionP2.Session.CodUser, workedProduct.DetailPayment.DateEntrance?.ToString("ddMM"), DateTime.Now.ToString("HHmm"));
                    }
                    workedProduct.AccountCompleted = true;
                }

                SessionP2.Session.ASMW(productSelected.Account);

                SessionP2.Session.Refresh(1000);

                Log.SaveFile("====== DEPOIS DA ASMW ======");

                HabilitaAcao();
                ValidaConcluir();
                Log.SaveFile("MetodoConcluir -> Final MetodoConcluir()");

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Log.SaveFile("CONCLUIR FINALIZADO [MetodoConcluir]");
                //throw ex;
            }
        }

        /*
        private void bwConcluir_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (bwConcluir.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    _bwThreadConcluir = Thread.CurrentThread;

                    WorkedProduct workedProduct = null;
                    if (WorkedProduct != null)
                        workedProduct = WorkedProduct.Where(p => p.Produto.Account == productSelected.Account).FirstOrDefault();


                    if (workedProduct != null && !workedProduct.AccountCompleted)
                    {
                        //Se acordo, registrar ocal
                        if (workedProduct.Tipo == Library.Class.Model.WorkedProduct.Type.Acordo)
                        {
                            string memo = "ACORDO ";// + txtTelefone.Text;
                            if (workedProduct.DetailPayment.TipoPagamento == DetailPayment.TypePayment.AVista)
                            {
                                memo += "AVISTA " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " PARA " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                memo += "PARCELADO " + workedProduct.DetailPayment.VlEntrance.ToString("N2") + " " + workedProduct.DetailPayment.DateEntrance?.ToString("dd/MM") + " " + workedProduct.DetailPayment.QtdParcel + "*" + workedProduct.DetailPayment.VlParcel?.ToString("N2");
                            }
                            OCAL ocal = SessionP2.Session.OCAL(productSelected.Account, productSelected.OrgCMS);
                            ocal.SetActionOcal("ACD", "", 0, memo, "", SessionP2.Session.CodUser, workedProduct.DetailPayment.DateEntrance?.ToString("ddMM"), DateTime.Now.ToString("HHmm"));
                        }
                        workedProduct.AccountCompleted = true;
                    }

                    SessionP2.Session.ASMW(productSelected.Account);

                    SessionP2.Session.Refresh(1000);

                    Log.SaveFile("====== bwConcluir_DoWork DEPOIS ASMW ======");

                    //Log.SaveFile(SessionP2.Session.ScreenText);

                    //ReloadOcmi("Resumido", true);

                    //Log.SaveFile("=================Depois do resumido");

                    //SessionP2.Session.Refresh(1000);
                    //ReloadOcmi("Detalhado", true);

                    //Log.SaveFile("=================Depois do detalhado");

                    //Log.SaveFile(SessionP2.Session.ScreenText);

                    //SessionP2.Session.ASMW(productSelected.Account);

                    //SessionP2.Session.Refresh(1000);

                    //    bwConcluir.ReportProgress(0, "Resumido");
                    //    bwConcluir.ReportProgress(0, "Detalhado");

                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("THREAD CONCLUIR FINALIZADA [bwConcluir_DoWork]");
                //throw ex;
            }
        }

        private void bwConcluir_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Log.SaveFile(" bwConcluir_ProgressChanged");
            if (e.UserState.ToString() == "Resumido")
            {
                PopulaBoxOcorrenciaSimplificada(ocorrenciaResumida, true);
            }
            else if (e.UserState.ToString() == "Detalhado")
            {
                PopulaBoxOcorrenciaDetalhada(ocorrenciaDetalhada, true);
            }
        }

        private void bwConcluir_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.SaveFile("bwConcluir_RunWorkerCompleted");
            if (e.Cancelled == true)
            {
                XtraMessageBox.Show("Cancelado [bwConcluir_RunWorkerCompleted]");
            }
            else if (e.Error != null)
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            else
            {
                HabilitaAcao();
                ValidaConcluir();
                Log.SaveFile("bwConcluir_RunWorkerCompleted -> Final bwConcluir()");
            }
            Application.DoEvents();
        }*/

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
                MessageBox logout = new MessageBox("Encerrar Atendimento", "Deseja finalizar o atendimento deste cliente?", "Ao confirmar, todas as contas deste cliente ficarão indisponíveis para trabalhar neste atendimento.", true, "Não", "Sim");
                DialogResult msg = logout.ShowDialog(); //XtraMessageBox.Show("Tem certeza que deseja sair da aplicação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (msg == DialogResult.OK)
                {
                    //Finaliza no Dialer
                    EndCall();
                }
                else
                {
                    //Continua na conta para selecionar outro produto
                    btnAcordo.Enabled = false;
                    btnPromessa.Enabled = false;
                    btnConcluir.Enabled = false;
                    cbListaCartao.Enabled = true;
                }
            }
            else
            {
                //Finaliza no Dialer
                EndCall();
            }
        }

        private void EndCall()
        {
            Log.SaveFile("EndCall");
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
                                Conta = worked.Produto.Account,
                                CodigoCodificacao = worked.CodFinalizacao,
                                GrupoCodificacao = worked.DsStatusResum,
                                DataRetorno = (worked.CodFinalizacao == "VLC") ? worked.DataRetorno : null,
                                TelefoneRetorno = (worked.CodFinalizacao == "VLC") ? worked.TelefoneRetorno : null,

                            }
                        );
                    }
                    var ret = Dialer.FinalizarChamada(resultados);
                }

                ClearAll();
                LimparBoxDiscador();
                LoadAccount = false;
                pbBoxProduto.Hide();
                Log.SaveFile("Final EndCall()");
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO NO MÉTODO EndCall[EndCall()]");
            }
            finally
            {
                Log.SaveFile("EndCall -> AbortBWConcluir()");
                //AbortBWConcluir();
            }
        }

        private void timerDialer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
                //Log.SaveFile("Total threads -> " + currentThreads.Count.ToString());

                if (Dialer.LogadoDialer)
                {
                    var bilhete = Dialer.Bilhete;
                    lblStatus.Text = bilhete.DescricaoStatus;
                    lblCampanha.Text = bilhete.Campanha;

                    if (!string.IsNullOrEmpty(bilhete.NumeroTelefone))
                    {
                        string p = bilhete.NumeroTelefone.Trim();
                        if (p.Length >= 11)
                            p = Convert.ToUInt64(p).ToString("(00) 00000-0000");
                        else
                            p = Convert.ToUInt64(p).ToString("(00) 0000-0000");
                        lblTelefone.Text = p;
                    }

                    if (bilhete.TipoChamada == TipoChamada.Inbound)
                        lblTipoLigacao.Text = "INBOUND";
                    else if (bilhete.TipoChamada == TipoChamada.Outbound)
                        lblTipoLigacao.Text = "OUTBOUND";
                    else if (bilhete.TipoChamada == TipoChamada.Manual)
                        lblTipoLigacao.Text = "MANUAL";
                    else
                        lblTipoLigacao.Text = "-";

                    if (LoadAccount == false)
                    {
                        if (!string.IsNullOrEmpty(bilhete.CPF)) //&& (productSelected == null || (bilhete.CPF.Trim() != productSelected.CPF.Trim()))
                        {
                            try
                            {
                                Log.SaveFile("Conta -> " + bilhete.Conta);
                                LoadAccount = true;
                                TipoPesquisa = LoadType.ACCOUNT;
                                NumeroPesquisa = bilhete.Conta.Trim();
                                watchLigacao.Stop();
                                watchLigacao.Restart(); // Start contador de tempo ligação
                                timerLigacao.Start(); // Atualizador de tempo da ligação

                                if (!bwProduto.IsBusy)
                                {
                                    bwProduto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwProduto_RunWorkerCompleted);
                                    bwProduto.DoWork += new DoWorkEventHandler(bwProduto_DoWork);
                                    bwProduto.RunWorkerAsync(); //Carrega conta
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.SaveFile("ERRO AO PROCESSAR BILHETE [bwProduto.RunWorkerAsync" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                                //throw ex;
                            }

                        }

                    }

                    if (Dialer.Bilhete.Status == Status.Deslogado && user.FlLoginDialer)
                    {
                        timerDialer.Stop();
                        LoginDialer();
                    }
                    ConfigureButtons();
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR BILHETE [timerDialer_Tick]" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                //throw ex;
            }
        }

        private void btnLibera_Click(object sender, EventArgs e)
        {
            try
            {
                Dialer.Libera();
                LoadAccount = false;
            }
            catch (Exception ex)
            {
                Log.SaveFile("ERRO AO CARREGAR Dialer.Libera() [btnLibera_Click]");
            }
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            //JANELA DE PAUSAS
            CodificarPausa codificarPausa = new CodificarPausa();
            DialogResult dr = codificarPausa.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                //
            }
        }

        private void ConfigureButtons()
        {
            Status status = Dialer.Bilhete.Status;
            GridView gridView = gcTelefones.MainView as GridView;
            switch (status)
            {
                case Status.Liberado: //READY 
                    this.btnLibera.Enabled = false;
                    this.btnPausa.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
                case Status.EmFilaEspera: //QUEUE
                    this.btnLibera.Enabled = true;
                    this.btnPausa.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
                case Status.EmChamada: //INCALL
                    this.btnLibera.Enabled = false;
                    this.btnPausa.Enabled = false;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                case Status.Pausa: //PAUSED
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;

                case Status.Erro: //DEAD
                    this.btnLibera.Enabled = false;
                    this.btnPausa.Enabled = false;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                case Status.Deslogado: //LOGOUT
                    this.btnLibera.Enabled = false;
                    this.btnPausa.Enabled = false;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = false;
                    break;
                default: //UNKNOW
                    this.btnPausa.Enabled = true;
                    this.btnLibera.Enabled = true;
                    ((RepositoryItemButtonEdit)gridView.Columns[2].RealColumnEdit).Buttons[0].Enabled = true;
                    break;
            }

        }
        private void timerLigacao_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = watchLigacao.Elapsed;
            lblTempo.Text = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
        private void LoginDialer()
        {
            Token = Dialer.GetToken;
            if (!string.IsNullOrEmpty(Token))
            {
                LoginDialer loginDialer = new LoginDialer();
                if (loginDialer.ShowDialog(this) == DialogResult.OK)
                {
                    timerDialer.Start();
                    pbBoxProduto.Hide();
                }
            }
            else
            {
                pbBoxProduto.Hide();
                XtraMessageBox.Show("Erro de conexão com o Dialer. Não foi possível obter o token.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class Telefone
        {
            public string Phone { get; set; }
            public string Tipo { get; set; }
        }

        private void btnNotes_ItemClick(object sender, ItemClickEventArgs e)
        {
            Notes notes = new Notes();
            notes.Show();
        }

        public void AbortBWConcluir()
        {
            if (_bwThreadConcluir != null)
                _bwThreadConcluir.Abort();
        }

        public void AbortThread(Thread thread)
        {
            if (thread != null)
                thread.Abort();
        }
    }
}

