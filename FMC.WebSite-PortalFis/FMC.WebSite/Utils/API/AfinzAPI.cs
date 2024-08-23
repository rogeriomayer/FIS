using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using FMC.WebSite.FIS.Utils.API.Ibi;
using System;
using System.Collections.Generic;

public class AfinzAPI
{
    private static string AFINZ_URL = AppSettings.URL_API_AFINZ; //"http://10.40.0.109/afinz/api";
    public static Navigation SetNavigation(Navigation navigation)
    {
        try
        {
            var result = RestAPI.Post<Navigation, Navigation>(AFINZ_URL, "navigation", navigation);
            if (result != null)
                return result;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static Product SetProduct(Product product)
    {
        try
        {
            var result = RestAPI.Post<Product, Product>(AFINZ_URL, "product", product);
            if (result != null)
                return result;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static Simulate SetSimulate(Simulate simulate)
    {
        try
        {
            var result = RestAPI.Post<Simulate, Simulate>(AFINZ_URL, "simulate", simulate);
            if (result != null)
                return result;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static Agreement SetAgreement(Agreement agreement)
    {
        try
        {
            var result = RestAPI.Post<Agreement, Agreement>(AFINZ_URL, "agreement", agreement);
            if (result != null)
                return result;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static Billet SetBillet(Billet billet)
    {
        try
        {
            var result = RestAPI.Post<Billet, Billet>(AFINZ_URL, "billet", billet);
            if (result != null)
                return result;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static IList<string> GetPIDNames(string name)
    {
        return RestAPI.Get<IList<string>>(AFINZ_URL, "pid/" + name + "/" + "4");
    }
}
