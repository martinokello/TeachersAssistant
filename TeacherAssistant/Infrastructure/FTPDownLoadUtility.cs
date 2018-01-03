using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TeacherAssistant.Infrastructure
{
    public class FTPDownloadUtility
    {
        string username;
        string password;
        string ftpServer;

        public FTPDownloadUtility(string username, string password, string ftpServer)
        {
            this.username = username;
            this.password = password;
            this.ftpServer = ftpServer;
        }
        /*
                public void DownloadFile(ref string virtualPath)
                {
                    virtualPath = virtualPath.Replace("//", "/");

                    FileInfo fileInf = new FileInfo(HttpContext.Current.Server.MapPath("~"+virtualPath.Substring(virtualPath.IndexOf("/Documents/"))));
                    virtualPath = "ftp://" + System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.Url.Host)[0].ToString() + "/httpdocs/"+virtualPath.Substring(virtualPath.IndexOf("TeachersAssistantDeploy"));

                    FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(virtualPath));

                    //Set the FTPRequest Properties:
                    ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    ftpRequest.KeepAlive = false;
                    ftpRequest.Credentials = new NetworkCredential(username, password);
                    ftpRequest.UseBinary = true;
                    ftpRequest.UsePassive = false;
                    ftpRequest.ContentLength = fileInf.Length;
                    ftpRequest.Headers.Add("Content-Type", "application/octet-stream");

                    //Get ftpResponse:

                    FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                    Stream respStream = ftpResponse.GetResponseStream();
                    //Read the response into a byte[];
                    byte[] bytes = new byte[2048];

                    int bytesRead = 0;
                    HttpContext.Current.Response.AppendHeader("content-disposition",
                "attachment; filename=" + virtualPath.Substring(virtualPath.LastIndexOf("/")));

                    Stream response = HttpContext.Current.Response.OutputStream;

                    while ((bytesRead = respStream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        response.Write(bytes, 0, bytesRead);
                    }
                    response.Flush();
                    response.Close();
                    respStream.Close();
                }
          */
        public void DownloadFile(ref string virtualPath,out Stream respStream)
        {
            virtualPath = virtualPath.Replace("//", "/").Replace("~","");

            //FileInfo fileInf = new FileInfo(HttpContext.Current.Server.MapPath("~/"+ virtualPath));
            virtualPath = "ftp://" + System.Net.Dns.GetHostAddresses(HttpContext.Current.Request.Url.Host)[0].ToString() + "/httpdocs" + virtualPath;

            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(virtualPath));

            //Set the FTPRequest Properties:
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpRequest.KeepAlive = false;
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = false;
            //ftpRequest.ContentLength = fileInf.Length;
            ftpRequest.Headers.Add("Content-Type", "application/octet-stream");

            //Get ftpResponse:

            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            respStream = ftpResponse.GetResponseStream();
            //Read the response into a byte[];
        }
    }
}