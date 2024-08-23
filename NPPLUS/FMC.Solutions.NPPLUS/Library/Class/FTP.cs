using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;


namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class FTP
    {

        private string Host = null;
        private string User = null;
        private string Pass = null;
        private FtpWebRequest FtpRequest = null;
        private FtpWebResponse FtpResponse = null;
        private Stream FtpStream = null;
        private int BufferSize = 2048;

        public FTP(string hostIP, string userName, string password)
        {
            Host = hostIP;
            User = userName;
            Pass = password;
        }

        public void Download(string remoteFile, string localFile)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpStream = FtpResponse.GetResponseStream();
                FileStream localFileStream = new FileStream(localFile, FileMode.Create);
                byte[] byteBuffer = new byte[BufferSize];
                int bytesRead = FtpStream.Read(byteBuffer, 0, BufferSize);
                try
                {
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = FtpStream.Read(byteBuffer, 0, BufferSize);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                localFileStream.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public void UploadAsync(string remoteFile, string localFile, bool deleteLocalFile = false)
        {

        }

        public void Upload(string remoteFile, string localFile, bool deleteLocalFile = false)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                using (var fileStream = File.Open(localFile, FileMode.Open))
                {
                    using (Stream requestStream = FtpRequest.GetRequestStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;

                        do
                        {
                            bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                            requestStream.Write(buffer, 0, bytesRead);
                        }
                        while (bytesRead > 0);

                        requestStream.Close();

                        using (FtpWebResponse response = (FtpWebResponse)FtpRequest.GetResponse())
                        {
                            response.Close();
                        }
                    }
                }
                if (deleteLocalFile)
                    File.Delete(localFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public void Delete(string deleteFile)
        {
            try
            {
                FtpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + deleteFile);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpResponse.Close();
                FtpRequest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public void Rename(string currentFileNameAndPath, string newFileName)
        {
            try
            {
                FtpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + currentFileNameAndPath);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.Rename;
                FtpRequest.RenameTo = newFileName;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpResponse.Close();
                FtpRequest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public void CreateDirectory(string newDirectory)
        {
            try
            {
                FtpRequest = (FtpWebRequest)WebRequest.Create(Host + "/" + newDirectory);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpResponse.Close();
                FtpRequest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public bool FileExists(string fileName)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.GetFileSize;

                try
                {
                    FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetFileCreatedDateTime(string fileName)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpStream = FtpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(FtpStream);
                string fileInfo = null;
                try { fileInfo = ftpReader.ReadToEnd(); }
                catch (Exception ex) { throw ex; }
                ftpReader.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
                return fileInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFileSize(string fileName)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + fileName);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpStream = FtpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(FtpStream);
                string fileInfo = null;
                try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
                catch (Exception ex) { throw ex; }
                ftpReader.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
                return fileInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<string> DirectoryListSimple(string directory)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + directory);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpStream = FtpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(FtpStream);
                string directoryRaw = null;
                try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex) { throw ex; }
                ftpReader.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
                try
                {
                    string[] directoryList = directoryRaw.Split("|".ToCharArray());
                    return directoryList;
                }
                catch
                {
                    return new List<string> { "" };
                }
            }
            catch
            {
                return new List<string> { "" };
            }
        }

        public string[] DirectoryListDetailed(string directory)
        {
            try
            {
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + directory);
                FtpRequest.Credentials = new NetworkCredential(User, Pass);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                FtpStream = FtpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(FtpStream);
                string directoryRaw = null;
                try
                {
                    while (ftpReader.Peek() != -1)
                    {
                        directoryRaw += ftpReader.ReadLine() + "|";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                ftpReader.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
                try
                {
                    return directoryRaw.Split("|".ToCharArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}