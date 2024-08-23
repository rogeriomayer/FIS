using FMC.FIS.Business.BLL;
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
    /// Recupera AFINZ
    /// </summary>
    public static string UserRecuperaAfinz { get; set; }
    public static string PassRecuperaAfinz { get; set; }
    public static string TokenRecuperaAfinz { get; set; }
    public static string URL_API_P1 { get; set; }
    public static string TOKEN_API_P1 { get; set; }

    /// <summary>
    /// Code Connect FIS
    /// </summary>
    public static string URL_API_CODE_CONNECT { get; set; }
    public static string TOKEN_CODE_CONNECT { get; set; }
    public static string USER_CODE_CONNECT { get; set; }
    public static string PASS_CODE_CONNECT { get; set; }


    /// <summary>
    /// SMTP
    /// </summary>
    public static string HOST_SMTP { get; set; }
    public static string PORT_SMTP { get; set; }
    public static string USER_SMTP { get; set; }
    public static string PASS_SMTP { get; set; }


    /// <summary>
    /// SMS TWW
    /// </summary>
    public static string USER_TWW { get; set; }
    public static string PASS_TWW { get; set; }

    #endregion

    /// <summary>
    /// COBMAIS CREDZ
    /// </summary>
    public static string UserCobmaisCredz { get; set; }
    public static string PassCobmaisCredz { get; set; }
    public static string UrlApiCobmaisCredz { get; set; }

    /// <summary>
    /// TELEGRAM 
    /// </summary>
    public static int TelegramApiId
    {
        get
        {
            return Convert.ToInt32(new ParameterBLL().GetBykey(3, "TelegramApiId").Value);
        }
    }
    public static string TelegramApiHash
    {
        get
        {
            return new ParameterBLL().GetBykey(3, "TelegramApiHash").Value;
        }
    }
    public static string TelegramPhoneNumber
    {
        get
        {
            return new ParameterBLL().GetBykey(3, "TelegramPhoneNumber").Value;
        }
    }
    public static string TelegramCodeAuth
    {
        get
        {
            return new ParameterBLL().GetBykey(3, "TelegramCodAuth").Value;
        }
    }

    public enum ProductType
    {
        FIS = 0,
        AFINZ = 1,
        CERRADO = 2,
        CREDZ = 3
    }

}