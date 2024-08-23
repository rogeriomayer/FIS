using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;
using FMC.Automator.DistribuicaoDesk.DAO.Persistence;
using FMC.Automator.DistribuicaoDesk.BLL;
using System.Windows.Forms;


namespace FMC.Automator.DistribuicaoDesk.BLL
{
    public class DistribuicaoDeskBLL
    {

        public void DistributeDeskReserved(ProgressBar pbContas, Label lblRegistros, Label lblTotal)
        {
            AmexImportReservedPersistencia reservedPersistencia = new AmexImportReservedPersistencia();
            IList<TB_AMEX_IMPORT_RESERVED> listaAmexImportReserved = new List<TB_AMEX_IMPORT_RESERVED>();
            pbContas.Minimum = 0;
            pbContas.Maximum = listaAmexImportReserved.Count;
            lblRegistros.Text = listaAmexImportReserved.Count.ToString();
            int count = 1;
            foreach (var amexImportReserved in listaAmexImportReserved)
            {
                CardType cardType = GetCardType(amexImportReserved.NR_CARDMEMBER);
                TB_DEBTOR debtor = InsertDebtor(amexImportReserved.TB_AMEX_IMPORT, cardType, amexImportReserved.CD_DESK);
                InsertDebtorComment("SYS", debtor.id_debtor, "CONTA AVULSA");
                pbContas.Increment(1);
                lblTotal.Text = count.ToString();
                count++;
            }
        }

        public void DistributeDesk(ProgressBar pbContas, Label lblRegistros, Label lblTotal, DataGridView dgvErros)
        {
            AmexImportPersistencia persistencia = new AmexImportPersistencia();
            IList<TB_AMEX_IMPORT> listAmexImport = persistencia.GetByIdProcess(1);
            string desk = string.Empty;
            string programLetter = string.Empty;
            string lastDesk = string.Empty;
            pbContas.Minimum = 0;
            pbContas.Maximum = listAmexImport.Count;
            lblRegistros.Text = listAmexImport.Count.ToString();
            int count = 1;
            IList<Error> listContaErros = new List<Error>();
            foreach (var amexImport in listAmexImport)
            {
                try
                {
                    if (desk != "AMX" && desk != "802" && desk != "900" && desk != "500" && desk != "100")
                        lastDesk = desk;

                    CardType cardType = GetCardType(amexImport.NR_CARDMEMBER);
                    IList<string> desks = GetDesks(amexImport.CD_PROGRAM_LETTER, cardType, amexImport.VL_BALANCE);
                    if (cardType == CardType.RCP || cardType == CardType.Corporate)
                    {
                        if (amexImport.CD_PROGRAM_LETTER != programLetter)
                        {
                            programLetter = amexImport.CD_PROGRAM_LETTER;
                            desk = string.Empty;
                        }
                        if (amexImport.CD_PROGRAM_LETTER == "A")
                        {
                            desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "A", desks, cardType);
                            /*if (amexImport.VL_BALANCE <= 2050 && cardType == CardType.RCP)
                                desk = "155"; //desk = "300";
                            else
                            {
                                if (cardType == CardType.Corporate)

                                    desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "A", desks, cardType);
                                /* else
                                     desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "A", desks, cardType);*/


                        }
                        else if (amexImport.CD_PROGRAM_LETTER == "B")
                            desk = GetDesk(lastDesk, amexImport.VL_BALANCE, "B", desks, CardType.RCP);
                        else if (amexImport.CD_PROGRAM_LETTER == "C")
                            desk = "100";
                        else if (amexImport.CD_PROGRAM_LETTER == "D")
                            desk = "802";
                        else if (amexImport.CD_PROGRAM_LETTER == "F")
                            desk = "888";
                        else if (amexImport.CD_PROGRAM_LETTER == "E" || amexImport.CD_PROGRAM_LETTER == "K")
                            desk = "900";
                        else
                            desk = "AMX";
                    }
                    else
                    {
                        if (amexImport.CD_PROGRAM_LETTER != programLetter)
                        {
                            programLetter = amexImport.CD_PROGRAM_LETTER;
                            desk = string.Empty;
                        }
                        if (amexImport.CD_PROGRAM_LETTER == "A" || amexImport.CD_PROGRAM_LETTER == "B")
                        {
                            if (amexImport.VL_BALANCE <= 4000)
                                desk = "500";
                            else

                                desk = GetDesk(lastDesk, amexImport.VL_BALANCE, amexImport.CD_PROGRAM_LETTER, desks, cardType);
                        }
                        else if (amexImport.CD_PROGRAM_LETTER == "C")
                        {
                            /*if (amexImport.VL_BALANCE <= 6000)
                                desk = "500";
                            else
                            desk = GetDesk(lastDesk, amexImport.VL_BALANCE, amexImport.CD_PROGRAM_LETTER, desks, cardType);*/
                            desk = "500";
                        }
                        else if (amexImport.CD_PROGRAM_LETTER == "D")
                            desk = "802";
                        else if (amexImport.CD_PROGRAM_LETTER == "F")
                            desk = "888";
                        else if (amexImport.CD_PROGRAM_LETTER == "E" || amexImport.CD_PROGRAM_LETTER == "K")
                            desk = "900";
                        else
                            desk = "AMX";
                    }

                    TB_DEBTOR debtor = InsertDebtor(amexImport, cardType, desk);

                    foreach (TB_AMEX_IMPORT_SUPP amexImportSupp in amexImport.TB_AMEX_IMPORT_SUPP)
                        InsertDebtorComment("AUT", debtor.id_debtor, "SUPLEMENTAR: " + amexImportSupp.DS_NAME.Trim() + " - " + amexImportSupp.NR_CARDMEMBER + (amexImportSupp.NR_CPF != null ? " - " + amexImportSupp.NR_CPF : ""));
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    do
                    {
                        erro = ex.Message;
                        ex = ex.InnerException;
                    } while (ex != null);

                    listContaErros.Add(new Error() { ID_AMEX_IMPORT = amexImport.ID_AMEX_IMPORT.ToString(), ERRO = erro });

                    if (erro.ToUpper().Contains("PK_UNIQUE_CD_DEBTOR"))
                    {
                        UpdateAmexImport(amexImport.ID_AMEX_IMPORT, desk, amexImport.ID_DEBTOR);
                    }

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

        private string GetDesk(string lastDesk, decimal vlBalance, string programLetter, IList<string> listDesks, CardType cardType)
        {
            string desk = string.Empty;

            if (string.IsNullOrWhiteSpace(lastDesk) || (!listDesks.Any(p => p == lastDesk)))
            {
                desk = new DebtorPersistencia().GetLastDeskInsert(programLetter, GetListCdDebtor(cardType), listDesks);
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

            if (program == "A" && cardType == CardType.RCP && value < 2050)
            {
                lista = new List<string>();
                lista = GetDesksAppConfig("RCP_PROGRAMA_A_2050").ToList();
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

        private void UpdateAmexImportReserved(int idReserved, int idAmexImport)
        {
            AmexImportReservedPersistencia persistencia = new AmexImportReservedPersistencia();
            TB_AMEX_IMPORT_RESERVED amexImportReserved = persistencia.GetById(idReserved);
            amexImportReserved.ID_AMEX_IMPORT = idAmexImport;
            persistencia.Update(amexImportReserved);
        }

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


            new DeskAgendaPersistencia().AddDeskAgendaDetail(cdDesk,
                                                             cdDebtor,
                                                             cdStatus,
                                                             vlBalance,
                                                             anoCorrente,
                                                             anoCorrente,
                                                             anoCorrente,
                                                            CriaDeskAnoCorrente(cdDesk));

        }

        private void InsertDebtorDesk(TB_DEBTOR debtor, byte nrDesk, string cdDesk)
        {
            tb_debtor_desks debtorDesk = new tb_debtor_desks();
            debtorDesk.nr_desk = nrDesk;
            debtorDesk.cd_desk = cdDesk;
            debtorDesk.dt_desk = DateTime.Now;

            debtor.tb_debtor_desks.Add(debtorDesk);
        }

        private void UpdateAmexImport(int idAmexPersistencia, string cdDesk, int? idDebtor)
        {
            AmexImportPersistencia persistencia = new AmexImportPersistencia();
            TB_AMEX_IMPORT amexImport = persistencia.GetById(idAmexPersistencia);
            amexImport.CD_DESK = cdDesk;
            amexImport.ID_PROCESS = 8;
            amexImport.ID_DEBTOR = idDebtor;
            persistencia.Update(amexImport);
        }

        private TB_DEBTOR InsertDebtor(TB_AMEX_IMPORT amexImport, CardType cardType, string cdDesk)
        {
            TB_DEBTOR debtor = new TB_DEBTOR();
            int idProgram = GetProgram(amexImport.CD_PROGRAM_LETTER, cardType, amexImport.NR_CARDMEMBER.Replace(" ", ""));

            if (idProgram > 0)
            {
                debtor.cd_debtor = new DebtorPersistencia().GetCdDebtor(idProgram);

                debtor.cd_desk = cdDesk;
                debtor.ds_uf = amexImport.DS_UF;
                debtor.ds_cep = amexImport.DS_CEP;
                debtor.vl_main = amexImport.VL_BALANCE;
                debtor.vl_balance = amexImport.VL_BALANCE;
                debtor.DT_BEGIN = amexImport.DT_ASSING;
                debtor.dt_cancel = amexImport.DT_LAST_ACT;
                debtor.dt_company_payment = DateTime.Now;
                debtor.NR_CPF = amexImport.NR_CPF;
                debtor.ds_city = amexImport.DS_CITY;
                debtor.cd_document = amexImport.NR_CARDMEMBER.Replace(" ", "");
                debtor.ds_name = amexImport.DS_NAME;
                debtor.ds_address = amexImport.DS_ADDRESS;
                debtor.ds_complement = amexImport.DS_COMPLEMENT;
                debtor.CD_STATUS = "NEW";
                debtor.id_program = idProgram;
                debtor.DS_EMAIL = amexImport.DS_EMAIL;
                debtor.DT_DEBTOR_INSERT = DateTime.Now;
                if (amexImport.DT_AMEX_DATA_CORTE.HasValue)
                    debtor.DT_AMEX_DATA_CORTE = amexImport.DT_AMEX_DATA_CORTE;

                InsertDebtorNotice(debtor, (amexImport.FL_ELEGIBLE ? "LEG" : "NLE"));
                InsertDebtorDesk(debtor, 1, "AMX");
                InsertDebtorDesk(debtor, 2, cdDesk);

                TB_DEBTOR newDebtor = new DebtorPersistencia().AddDebtor(debtor);

                InsertDebtorPhone(newDebtor.id_debtor, amexImport.TB_AMEX_IMPORT_PHONES.ToList());

                InsertDeskAgendaDetail(cdDesk, newDebtor.cd_debtor, newDebtor.CD_STATUS, newDebtor.vl_balance);

                new ProgramPersistencia().UpdateCdDebtor(debtor.cd_debtor, idProgram);

                UpdateAmexImport(amexImport.ID_AMEX_IMPORT, newDebtor.cd_desk, newDebtor.id_debtor);

                return newDebtor;
            }
            else
                throw new Exception("ID_PROGRAM NÃO ENCONTRADO PARA LETTER " + amexImport.CD_PROGRAM_LETTER + " " + cardType.ToString());
        }

        private void InsertDebtorPhone(int idDebtor, IList<TB_AMEX_IMPORT_PHONES> amexImportPhones)
        {
            byte count = 1;

            foreach (TB_AMEX_IMPORT_PHONES amexImportPhone in amexImportPhones)
            {
                tb_debtor_phone debtorPhone = new tb_debtor_phone();
                debtorPhone.id_debtor = idDebtor;
                debtorPhone.id_phone = count;
                debtorPhone.cd_phone = amexImportPhone.DS_PHONE;
                debtorPhone.ds_phone = "";
                debtorPhone.id_phone_type = amexImportPhone.id_phone_type.Value;
                debtorPhone.DT_INSERT = DateTime.Now;
                new DebtorPhonePersistencia().AddPhone(debtorPhone);
                count++;


            }
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

        private void UpdateDebtorAMX(int idDebtor)
        {
            DebtorPersistencia persistencia = new DebtorPersistencia();
            TB_DEBTOR debtor = persistencia.GetById(idDebtor);
            debtor.cd_desk = "AMX";
            persistencia.Update(debtor);
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
            public string ID_AMEX_IMPORT { get; set; }
            public string ERRO { get; set; }
        }

    }
}
