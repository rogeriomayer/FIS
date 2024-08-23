using Microsoft.AspNetCore.Http;
using System;
using System.Configuration;
using System.Xml;

public class Constants
{

    private static IHttpContextAccessor HttpContextAccessor;
    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public static HttpContext Current => HttpContextAccessor.HttpContext;


    #region AppSettings

    public static string Jwt { get; set; }
    public static int TokenExpires { get; set; }

    /// <summary>
    /// Recupera CREDZ
    /// </summary>
    public static string UserRecuperaCredZ { get; set; }
    public static string PassRecuperaCredZ { get; set; }
    public static string TokenRecuperaCredZ { get; set; }
    public static string URL_API_P1 { get; set; }
    public static string TOKEN_API_P1 { get; set; }

    public static string URL_API_CODE_CONNECT { get; set; }
    public static string TOKEN_CODE_CONNECT { get; set; }
    public static string USER_CODE_CONNECT { get; set; }
    public static string PASS_CODE_CONNECT { get; set; }

    #endregion


    public enum ProductType
    {
        CREDZ = 1
    }

}