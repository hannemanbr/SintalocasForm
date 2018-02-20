using System;
namespace SINTALOCAS.Dominio.Util
{
    public class MensagemUtil
    {
        public static string ErroCPFInvalido() { return "CPF inválido"; }
        public static string ErroRGInvalido() { return "RG inválido"; }
        public static string ErroEMAILInvalido() { return "E-mail inválido"; }
        public static string ErroCNPJInvalido() { return "CNPJ inválido"; }
        public static string ErroPIsInvalido() { return "PIS inválido"; }
        public static string ErroConsirInvalido() { return "Consir inválido"; }
        public static string ErroCEPInvalido() { return "CEP inválido"; }
        public static string ErroUFInvalido() { return "Estado/UF inválido"; }
        public static string ErroCamposNaoPreenchidos() { return "Existem campos obrigatórios incorretos"; }
        public static string ErroDTNASCInvalido() { return "Data de Nascimento inválida"; }
    }
}
