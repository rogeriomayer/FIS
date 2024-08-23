using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;
using FMC.Automator.DistribuicaoDesk.DAO.Persistence;
using FMC.Automator.DistribuicaoDesk.BLL;
using System.Windows.Forms;
using FMC.Systems;

namespace FMC.Automator.DistribuicaoDesk.Classes
{
    public class DistribuicaoDesk
    {

        public void DistributeDesk(WCCAccess wccAccess, BZAAccess bzaAccess, TabControl tabMainFrame,
                                   ProgressBar pbContas, Label lblRegistros, Label lblTotal, DataGridView dgvErros)
        {


            IList<int> listDebtorUpdate = new DebtorBLL().GetListDebtorUpdate();

            string desk = string.Empty;
            string programLetter = string.Empty;
            string lastDesk = string.Empty;
            pbContas.Minimum = 0;
            pbContas.Maximum = listDebtorUpdate.Count;
            lblRegistros.Text = listDebtorUpdate.Count.ToString();
            int count = 1;
            IList<Error> listContaErros = new List<Error>();


            foreach (int idDebtor in listDebtorUpdate)
            {
                try
                {
                    DebtorBLL debtorBLL = new DebtorBLL();

                    TB_DEBTOR debtor = debtorBLL.GetById(idDebtor);

                    //bool verifyFull = UpdateDebtor(debtor, tabMainFrame, wccAccess, bzaAccess, globestarAccess);
                    bool verifyFull = UpdateDebtor(debtor, tabMainFrame, wccAccess, bzaAccess);

                    if (desk != "AMX" && desk != "802" && desk != "900" && desk != "500" && desk != "100")
                        lastDesk = desk;

                    CardType cardType = GetCardType(debtor.cd_document);
                    string program = new ProgramPersistencia().GetById(debtor.id_program.Value).cd_program_letter;
                    IList<string> desks = GetDesks(program, cardType, debtor.vl_balance);

                    if (cardType == CardType.RCP || cardType == CardType.Corporate)
                    {
                        if (program != programLetter)
                        {
                            programLetter = program;
                            desk = string.Empty;
                        }
                        if (program == "A")
                        {
                            desk = GetDesk(lastDesk, "A", desks, cardType);
                            /*if (amexImport.VL_BALANCE <= 2050 && cardType == CardType.RCP)
                                desk = "155"; //desk = "300";
                            else
                            {
                                if (cardType == CardType.Corporate)

                                    desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "A", desks, cardType);
                                /* else
                                     desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "A", desks, cardType);*/
                        }
                        else if (program == "B")
                            desk = GetDesk(lastDesk, "B", desks, cardType);
                        else if (program == "C")
                            desk = "100";
                        else if (program == "D")
                            desk = "802";
                        else if (program == "F")
                            desk = "888";
                        else if (program == "E" || program == "K")
                            desk = "900";
                        else
                            desk = "AMX";
                    }
                    else
                    {
                        if (program != programLetter)
                        {
                            programLetter = program;
                            desk = string.Empty;
                        }
                        if (program == "A")
                        {
                            if (debtor.vl_balance <= 5000)
                                desk = "500";
                            else
                                desk = "100";
                        }

                        else if (program == "B")
                        {
                            if (debtor.vl_balance <= 5000)
                                desk = "500";
                            else
                                desk = GetDesk(lastDesk, program, desks, cardType);
                        }
                        else if (program == "C")
                        {
                            /*if (amexImport.VL_BALANCE <= 6000)
                                desk = "500";
                            else
                            desk = GetDesk(lastDesk, amexImport.VL_BALANCE, amexImport.CD_PROGRAM_LETTER, desks, cardType);*/
                            desk = "500";
                        }
                        else if (program == "D")
                            desk = "802";
                        else if (program == "F")
                            desk = "888";
                        else if (program == "E" || program == "K")
                            desk = "900";
                        else
                            desk = "AMX";
                    }

                    debtor.cd_desk = desk;
                    debtor.dt_update = DateTime.Now;

                    int nrDesk = new DebtorDeskPersistencia().MaxNrDesk(debtor.id_debtor);

                    InsertDebtorDesk(debtor, (byte)nrDesk, desk);

                    debtorBLL.Update(debtor);

                    InsertDeskAgendaDetail(desk, debtor.cd_debtor, debtor.CD_STATUS, debtor.vl_balance);


                    if (verifyFull)
                        InsertRequest(debtor.id_debtor);
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    do
                    {
                        erro = ex.Message;
                        ex = ex.InnerException;
                    } while (ex != null);

                    listContaErros.Add(new Error() { ID_DEBTOR = idDebtor.ToString(), ERRO = erro });
                }
                finally
                {
                    pbContas.Increment(1);
                    lblTotal.Text = count.ToString();
                    count++;
                }

            }
            if (listContaErros.Any())
            {
                dgvErros.Visible = true;
                dgvErros.DataSource = null;
                dgvErros.DataSource = listContaErros;
            }
            else
                dgvErros.Visible = false;
        }

        private void InsertRequest(int idDebtor)
        {
            TB_REQUEST request = new TB_REQUEST();
            request.id_debtor = idDebtor;
            request.id_request_status = 0;
            request.id_request_type = 30;
            request.id_user_open = 1;
            request.dt_open = DateTime.Now;

            new RequestPersistencia().Add(request);

        }

        private bool UpdateDebtor(TB_DEBTOR debtor, TabControl tabMainFrame, WCCAccess wccAccess, BZAAccess bzaAccess)
        {
            bool verifyFull = false;
            tabMainFrame.SelectTab(0);
            wccAccess.AcessWCADV(debtor.cd_document);
            Nullable<DateTime> dtBegin = wccAccess.GetDateCodificacao;
            if (dtBegin.HasValue)
            {
                debtor.DT_BEGIN = dtBegin.Value;
                //debtor.dt_cancel = dtBegin.Value;
            }

            Nullable<DateTime> dtCancel = wccAccess.GetDateCancelWCPOA;
            if (dtCancel.HasValue)
            {
                debtor.dt_cancel = dtCancel.Value;
            }

            //Nullable<DateTime> dtcancel = wccAccess.GetDateCancelHis;
            //if (dtcancel.HasValue)
            //debtor.dt_cancel = dtcancel.Value;


            IList<string> phones = wccAccess.GetListPhoneWCDAD(debtor.cd_document).Select(p => p.number).ToList();

            foreach (string phone in phones)
            {
                //string phoneFormat = FormatPhone(phone);

                if (!debtor.tb_debtor_phone.Any(p => p.cd_phone.Contains(phone)))
                {
                    Byte idphone = debtor.tb_debtor_phone.Count == 0 ? Convert.ToByte(1) : Convert.ToByte(debtor.tb_debtor_phone.Max(p => p.id_phone) + 1);

                    debtor.tb_debtor_phone.Add(new tb_debtor_phone
                    {
                        id_debtor = debtor.id_debtor,
                        id_phone = idphone,
                        id_phone_type = 1,
                        cd_phone = phone.StartsWith("0") ? phone : "0" + phone,
                        ds_phone = "WCDAD",
                        DT_INSERT = DateTime.Now
                    }
                    );
                }
            }

            if (GetCardType(debtor.cd_document) == CardType.RCP || GetCardType(debtor.cd_document) == CardType.Corporate)
            {
                tabMainFrame.SelectTab(1);
                if (!debtor.dt_cancel.HasValue)
                    debtor.dt_cancel = bzaAccess.GetDateCancelRASI(debtor.cd_document);

                bzaAccess.AccessRSTI(debtor.cd_document);

                debtor.DT_AMEX_DATA_CORTE = bzaAccess.GetCutDateRSTI.AddMonths(1);
                debtor.vl_balance = bzaAccess.GetFullAccount(debtor.cd_document, ref verifyFull, false);
                debtor.vl_main = debtor.vl_balance;
            }
            /*else
            {
                tabMainFrame.SelectTab(2);
                //globestarAccess.AccessCardGlobestar(debtor.cd_document);
                if (!debtor.dt_cancel.HasValue)
                    debtor.dt_cancel = globestarAccess.GetDateCanceledARQB(debtor.cd_document);

                globestarAccess.AccessARIQ(debtor.cd_document);
                debtor.vl_balance = globestarAccess.GetCurrentARIQ + globestarAccess.GetAmexPlanARIQ;
                debtor.vl_main = debtor.vl_balance;

                debtor.DT_AMEX_DATA_CORTE = globestarAccess.GetCutDate;
            } */

            return verifyFull;
        }

        private string GetDesk(string lastDesk, string programLetter, IList<string> listDesks, CardType cardType)
        {
            string desk = string.Empty;

            if (string.IsNullOrWhiteSpace(lastDesk) || (!listDesks.Any(p => p == lastDesk)))
            {
                desk = new DebtorBLL().GetLastDeskInsert(programLetter, GetListCdDebtor(cardType), listDesks);
                if (listDesks.ToList().FindIndex(p => p.Equals(desk)) == listDesks.Count - 1)
                    desk = listDesks.FirstOrDefault();
                else
                    desk = listDesks.ToList()[listDesks.ToList().FindIndex(p => p.Equals(desk)) + 1].ToString();
            }
            else if (listDesks.ToList().FindIndex(p => p.Equals(lastDesk)) == listDesks.Count - 1)
                desk = listDesks.FirstOrDefault();
            else
                desk = listDesks.ToList()[listDesks.ToList().FindIndex(p => p.Equals(lastDesk)) + 1].ToString();

            return desk;
        }

        private IList<string> GetDesks(string program, CardType cardType, decimal value)
        {
            List<string> lista;

            if (program == "A" && cardType == CardType.RCP && value < 5000)
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("RCP_PROGRAMA_A_5000").ToList();
                return GetDesksAtivas(lista);
            }

            else if (program == "A" && (cardType == CardType.RCP || cardType == CardType.Corporate))
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("RCP_PROGRAMA_A").ToList();
                return GetDesksAtivas(lista);
            }

            else if (program == "B" && (cardType == CardType.RCP || cardType == CardType.Corporate))
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("RCP_PROGRAMA_B").ToList();
                return GetDesksAtivas(lista);
            }
            else if (program == "A" && cardType == CardType.GRCC)
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("GRCC_PROGRAMA_A").ToList();
                return GetDesksAtivas(lista);
            }
            else if (program == "B" && cardType == CardType.GRCC)
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("GRCC_PROGRAMA_B").ToList();
                return GetDesksAtivas(lista);
            }
            else if (program == "C" && cardType == CardType.GRCC)
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("GRCC_PROGRAMA_C").ToList();
                return GetDesksAtivas(lista);
            }
            else
                return new List<string> { { "AMX" } };
        }

        private IList<string> GetDesksAppConfig(string programkey)
        {
            return new System.Configuration.AppSettingsReader().GetValue(programkey, typeof(string)).ToString().Split(',').ToList();
        }

        private IList<string> GetDesksAtivas(IList<string> lista)
        {
            IList<string> novaLista = new List<string>();
            foreach (string item in lista)
            {
                if (new DebtorDeskPersistencia().GetDeskAtiva(item))
                {
                    novaLista.Add(item);
                }
            }

            return novaLista;
        }

        /*private void UpdateAmexImportReserved(int idReserved, int idAmexImport)
        {
            AmexImportReservedPersistencia persistencia = new AmexImportReservedPersistencia();
            TB_AMEX_IMPORT_RESERVED amexImportReserved = persistencia.GetById(idReserved);
            amexImportReserved.ID_AMEX_IMPORT = idAmexImport;
            persistencia.Update(amexImportReserved);
        }*/

        private void InsertDebtorComment(string system, int idDebtor, string comments)
        {
            DebtorCommentsPersistencia persistencia = new DebtorCommentsPersistencia();
            tb_debtor_comments debtorComments = new tb_debtor_comments();

            debtorComments.cd_user_system = system;
            debtorComments.id_debtor = idDebtor;
            debtorComments.dt_comment = DateTime.Now;
            debtorComments.nr_comment = Convert.ToInt16(persistencia.GetMaxNrComments(idDebtor) + 1);
            debtorComments.ds_comment = comments;

            persistencia.Add(debtorComments);

        }

        private int CriaDeskAnoCorrente(string desk)
        {
            DeskAgendaPersistencia persistencia = new DeskAgendaPersistencia();
            tb_desk_agenda deskAgenda = persistencia.GetAgendaAnoCorrente(desk);
            DateTime anoCorrente = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());

            if (deskAgenda == null)
            {
                persistencia.Add(new tb_desk_agenda() { cd_desk = desk, dt_agenda = anoCorrente, qt_debtor = 1 });
                return 1;
            }
            else
            {
                deskAgenda.qt_debtor++;
                persistencia.Update(deskAgenda);
                return deskAgenda.qt_debtor.Value;
            }
        }

        public void InsertDeskAgendaDetail(string cdDesk, string cdDebtor, string cdStatus, decimal vlBalance)
        {
            DateTime anoCorrente = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());


            new DeskAgendaPersistencia().AddDeskAgendaDetail(cdDesk, cdDebtor, cdStatus, vlBalance, anoCorrente,
                                                             anoCorrente, anoCorrente, CriaDeskAnoCorrente(cdDesk));

        }

        private void InsertDebtorDesk(TB_DEBTOR debtor, byte nrDesk, string cdDesk)
        {
            tb_debtor_desks debtorDesk = new tb_debtor_desks();
            debtorDesk.nr_desk = nrDesk;
            debtorDesk.cd_desk = cdDesk;
            debtorDesk.dt_desk = DateTime.Now;

            debtor.tb_debtor_desks.Add(debtorDesk);
        }

        private void InsertDebtorNotice(TB_DEBTOR debtor, string notice)
        {
            tb_debtor_notices debtorNotice = new tb_debtor_notices();
            debtorNotice.cd_notice = notice;
            debtorNotice.dt_notice = DateTime.Now;
            debtorNotice.nr_notice = 1;
            debtor.tb_debtor_notices.Add(debtorNotice);
        }

        private int GetProgram(string programLetter, CardType cardType, string cardMember)
        {
            return GetProgramCode(programLetter, cardType, cardMember);
        }

        private int GetProgramCode(string programLetter, CardType cardType, string cardMember)
        {
            TB_PROGRAM program = null;
            ProgramPersistencia persistencia = new ProgramPersistencia();

            if (cardType == CardType.RCP || cardType == CardType.Corporate)
            {
                if (cardMember.StartsWith("37643") || cardMember.StartsWith("37660"))
                    program = persistencia.GetByLetterCardType(programLetter, "CORPORATE");
                else
                    program = persistencia.GetByLetterCardType(programLetter, "RCP");
            }
            else if (cardType == CardType.GRCC)
                program = persistencia.GetByLetterCardType(programLetter, "GRCC");
            else
                return -1;

            return program.id_program;
        }

        private int GetProgram(string programLetter, string cardType)
        {
            return new ProgramPersistencia().GetByLetterCardType(programLetter, cardType).id_program;
        }

        public static string FormatPhone(string phone)
        {
            int CutPhone = 0;
            int zeroLeft = 0;
            string PhoneReturn = "";

            foreach (char sPhone in phone.ToList())
            {

                if (sPhone.ToString() == "0" && zeroLeft == 0)
                {

                }
                else
                {
                    zeroLeft = 1;
                    PhoneReturn = PhoneReturn + sPhone.ToString();
                }
            }

            CutPhone = phone.Length - 11;

            if (CutPhone > 0 && !string.IsNullOrEmpty(PhoneReturn.Trim()))
                PhoneReturn = PhoneReturn.Substring(CutPhone, PhoneReturn.Count() - CutPhone);

            return PhoneReturn.Trim();
        }

        public enum CardType
        {
            GRCC,
            RCP,
            Corporate
        }

        private CardType GetCardType(string cardMember)
        {
            if (cardMember.StartsWith("37648"))
                return CardType.GRCC;
            else if (cardMember.StartsWith("37646"))
                return CardType.GRCC;
            else if (cardMember.StartsWith("37660"))
                return CardType.Corporate;
            else if (cardMember.StartsWith("37717"))
                return CardType.RCP;
            else if (cardMember.StartsWith("37643"))
                return CardType.Corporate;
            else if (cardMember.StartsWith("3764"))
                return CardType.RCP;
            else if (cardMember.StartsWith("3765"))
                return CardType.GRCC;
            else if (cardMember.StartsWith("3747"))
                return CardType.GRCC;
            else if (cardMember.StartsWith("3766"))
                return CardType.GRCC;
            else if (cardMember.StartsWith("3751"))
                return CardType.GRCC;
            else
                return CardType.RCP;


        }

        private IList<string> GetListCdDebtor(CardType cardType)
        {
            if (cardType == CardType.RCP)
                return new List<string>() {
                    { "75*AMX100" },
                    { "75*AMX200" },
                    { "75*AMX500" },
                    { "75*AMX600" },
                    { "75*AMX700" },
                    { "75*AMX900" },
                    { "75*AMX210" },
                    { "75*AMX510" },
                    { "75*AMX610" },
                    { "75*AMX910" },
                };

            else if (cardType == CardType.GRCC)
                return new List<string>() {
                    { "75*AMX101" },
                    { "75*AMX102" },
                    { "75*AMX103" },
                    { "75*AMX201" },
                    { "75*AMX202" },
                    { "75*AMX501" },
                    { "75*AMX601" },
                    { "75*AMX602" },
                    { "75*AMX701" },
                    { "75*AMX901" },
                };
            else
                return new List<string>()
                {
                    { "75*AMX110" },
                };
        }


        internal class Error
        {
            public string ID_DEBTOR { get; set; }
            public string ERRO { get; set; }
        }

    }
}
