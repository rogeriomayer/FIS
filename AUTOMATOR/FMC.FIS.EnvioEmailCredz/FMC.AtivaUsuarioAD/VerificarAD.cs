using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.AtivaUsuarioAD
{
    internal class VerificarAD
    {
        public static void VericarUsuariosAD(string userAd)
        {
            try
            {
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "fmcudi.com.br", "leandro.silva", "fmc@1234");

                // find a user
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userAd);

                if (user != null)
                {
                    string displayName = user.DisplayName;


                    if (user.IsAccountLockedOut())
                    {
                        try
                        {
                            if (user.AccountLockoutTime != null)
                                SaveFile("Bloqueado as " + user.AccountLockoutTime.Value.ToString("dd/HH/yyyy HH:mm:sss"));

                            user.UnlockAccount();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                    SaveFile("Erro ao checar usuario CREDZ");
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += Environment.NewLine + ex.Message;
                }

                SaveFile("Erro: " + erro);
            }
        }

        public static string pathLog = AppDomain.CurrentDomain.BaseDirectory + "LOG";
        public static void SaveFile(string message)
        {
            string fileName = pathLog + "\\LOG_" + Environment.CurrentManagedThreadId.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";
            StreamWriter logExecution = null;
            try
            {
                if (!Directory.Exists(pathLog))
                    Directory.CreateDirectory(pathLog);

                using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
                {
                    logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
