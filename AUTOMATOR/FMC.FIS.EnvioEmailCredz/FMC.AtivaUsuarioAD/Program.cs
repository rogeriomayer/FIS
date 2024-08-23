// See https://aka.ms/new-console-template for more information

Console.WriteLine("Checando!");

IList<string> users = new List<string>() { "credz", "daiony.barbio", "daiony" };
foreach (var user in users)
    FMC.AtivaUsuarioAD.VerificarAD.VericarUsuariosAD(user);