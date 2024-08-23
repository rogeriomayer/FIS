
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class Permission
    {
        public static UserProfile UserProfile;


        public static bool Enabled(string constant)
        {
            return UserProfile.UserProfileConstantAccess.Where(p => p.ConstantAccess.DsConstantAccess == constant).Count() > 0;
        }

        public class Usuario
        {
            public static bool Listar = Enabled(@"PRINCIPAL\Usuário\Listar");
            public static bool Adicionar = Enabled(@"PRINCIPAL\Usuário\Adicionar");
            public static bool Atualizar = Enabled(@"PRINCIPAL\Usuário\Atualizar");
        }

        public class Perfil
        {
            public static bool Listar = Enabled(@"PRINCIPAL\Perfil\Listar");
            public static bool Adicionar = Enabled(@"PRINCIPAL\Perfil\Adicionar");
            public static bool Atualizar = Enabled(@"PRINCIPAL\Perfil\Atualizar");
        }

        public class Acordo
        {
            public static bool Listar = Enabled(@"PRINCIPAL\Acordo\Visualizar Realizados");
            public static bool Simular = Enabled(@"PRINCIPAL\Acordo\Simular");
            public static bool Finalizar = Enabled(@"PRINCIPAL\Acordo\Finalizar");
        }

        public class Endereco
        {
            public static bool Atualizar = Enabled(@"PRINCIPAL\Endereço\Atualizar");
            public static bool Visualizar = Enabled(@"PRINCIPAL\Endereço\Visualizar");
            public static bool AdicionarTelefone = Enabled(@"PRINCIPAL\Endereço\Adicionar Telefone");
        }

        public class ExtratoResumido
        {
            public static bool Visualizar = Enabled(@"PRINCIPAL\Extrato Resumido\Visualizar Outros");
        }

        public class Promessa
        {
            public static bool Visualizar = Enabled(@"PRINCIPAL\Promessa\Finalizar");
        }

        public class Boleto
        {
            public static bool Listar = Enabled(@"PRINCIPAL\Boleto\Listar");
            public static bool Adicionar = Enabled(@"PRINCIPAL\Boleto\Adicionar");
            public static bool Email = Enabled(@"PRINCIPAL\Boleto\Enviar por Email");
            public static bool SMS = Enabled(@"PRINCIPAL\Boleto\Enviar por SMS");
            public static bool Download = Enabled(@"PRINCIPAL\Boleto\Download");
            public static bool Apagar = Enabled(@"PRINCIPAL\Boleto\Apagar");
        }
        public class Acao
        {
            public static bool CarregarConta = Enabled(@"PRINCIPAL\Ações\Carregar Conta");
        }

        public class Configuracao
        {
            public static bool Tema = Enabled(@"PRINCIPAL\Configurações\Alterar Tema");
            public static bool Carregamento = Enabled(@"PRINCIPAL\Configurações\Carregamento Opcional");
            //public static bool ParameterAdd = Enabled(@"PRINCIPAL\Configurações\Parâmetros\Adicionar");
            //public static bool ParameterList = Enabled(@"PRINCIPAL\Configurações\Parâmetros\Listar");

            public class Parametros
            {
                public static bool Adicionar = Enabled(@"PRINCIPAL\Configurações\Parâmetros\Adicionar");
                public static bool Listar = Enabled(@"PRINCIPAL\Configurações\Parâmetros\Listar");
            }

        }

        public class Relatorio
        {
            public static bool Operacional = Enabled(@"PRINCIPAL\Relatórios\Operacional");
            public static bool Gerencial = Enabled(@"PRINCIPAL\Relatórios\Gerencial");
        }

        public class RelatorioGerencial
        {
            public static bool Todos = Enabled(@"PRINCIPAL\Relatório Gerencial\Todos Operadores");
            public static bool Acordos = Enabled(@"PRINCIPAL\Relatório Gerencial\Acordos");
            public static bool Promessas = Enabled(@"PRINCIPAL\Relatório Gerencial\Promessas");
            public static bool Resumo = Enabled(@"PRINCIPAL\Relatório Gerencial\Resumo de Ligações");
            public static bool Extrato = Enabled(@"PRINCIPAL\Relatório Gerencial\Extrato de Finalizações");
            public static bool Ranking = Enabled(@"PRINCIPAL\Relatório Gerencial\Ranking");
        }
        public class MensagemPermanente
        {
            public static bool Listar = Enabled(@"PRINCIPAL\Mensagem Permanente\Listar");
            public static bool Cadastrar = Enabled(@"PRINCIPAL\Mensagem Permanente\Cadastrar");
        }
    }
}
