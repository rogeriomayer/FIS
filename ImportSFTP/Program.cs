using Renci.SshNet;
using System;
using System.IO;

namespace ImportSFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            foreach (var process in System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName))
                if (process.MainModule.FileName == currentProcess.MainModule.FileName && process.Id != currentProcess.Id)
                    process.Kill();
            
            Download();
        }

        static void Download()
        {
            try
            {
                using (SftpClient sftp = new SftpClient("importacao.cobmais.com.br", "assessoria601", "Cobmais#FTP#2fQ30Wx1"))
                {
                    sftp.Connect();
                    sftp.ChangeDirectory("//DIGITAIS//"); // subdiretorio do ftp
                    string downloadedFileName = "teste";
                    string localPath = "D://CREDZ//Importados//"; //local para salvar o arquivo
                    string dtDia = DateTime.Now.ToString("ddMMyyyy");
                    string arquivo;

                    var listDirectory = sftp.ListDirectory("//DIGITAIS//"); // subdiretorio do ftp
                                                                            // Console.WriteLine("Listando diretório:");
                                                                            // Console.WriteLine("");
                    foreach (var fi in listDirectory)
                    {
                        if (fi.Name.Length > 2)
                        {
                            arquivo = fi.Name.Substring(14, 8);
                            if (arquivo == dtDia)
                            {
                                //  Console.WriteLine(" >   " + fi.Name);
                                downloadedFileName = fi.Name;
                            }
                        }

                    }
                    // Console.WriteLine("");
                    // Console.WriteLine("Digite o nome do arquivo a fazer download:");
                    //downloadedFileName = Console.ReadLine();

                    if (sftp.Exists(downloadedFileName))
                    {
                        //  Console.WriteLine("download em andamento..." + downloadedFileName);
                        using (var file = File.OpenWrite(localPath + downloadedFileName))
                        {
                            sftp.DownloadFile(downloadedFileName, file);
                            // Console.WriteLine("Arquivo feito download com sucesso!");
                            // Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Arquivo não encontrado:");
                    }
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ocorreu um erro" + ex.Message.ToString());
            }
        }
    }
}
