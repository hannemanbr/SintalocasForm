using System;
using SINTALOCAS.Dominio.Servico;

namespace SINTALOCAS.Dominio.Util
{
    public class MensagemUtil
    {
        //public static string ErroCPFInvalido() { return "CPF inválido"; }
        //public static string ErroRGInvalido() { return "RG inválido"; }
        //public static string ErroEMAILInvalido() { return "E-mail inválido"; }
        //public static string ErroCNPJInvalido() { return "CNPJ inválido"; }
        //public static string ErroPIsInvalido() { return "PIS inválido"; }
        //public static string ErroConsirInvalido() { return "Consir inválido"; }
        //public static string ErroCEPInvalido() { return "CEP inválido"; }
        //public static string ErroUFInvalido() { return "Estado/UF inválido"; }
        //public static string ErroCamposNaoPreenchidos() { return "Existem campos obrigatórios incorretos"; }
        //public static string ErroDTNASCInvalido() { return "Data de Nascimento inválida ou com idade menor que 16 anos"; }

        public static string ErroCPFInvalido() { return MensagemServico.Consultar("ERRO","CPFERRO")[0].Texto; }
        public static string ErroRGInvalido() { return MensagemServico.Consultar("ERRO","RGERRO")[0].Texto; }
        public static string ErroEMAILInvalido() { return MensagemServico.Consultar("ERRO","EMAILERRO")[0].Texto; }
        public static string ErroCNPJInvalido() { return MensagemServico.Consultar("ERRO","CNPJERRO")[0].Texto; }
        public static string ErroPIsInvalido() { return MensagemServico.Consultar("ERRO","PISERRO")[0].Texto; }
        public static string ErroConsirInvalido() { return MensagemServico.Consultar("ERRO","CONSIRERRO")[0].Texto; }
        public static string ErroCEPInvalido() { return MensagemServico.Consultar("ERRO","CEPERRO")[0].Texto; }
        public static string ErroUFInvalido() { return MensagemServico.Consultar("ERRO","UFERRO")[0].Texto; }
        public static string ErroCamposNaoPreenchidos() { return MensagemServico.Consultar("ERRO","CAMPOOBRIG")[0].Texto; }
        public static string ErroDTNASCInvalido() { return MensagemServico.Consultar("ERRO","DTNASCERRO")[0].Texto; }

        public static string MensagemConcordar() { return MensagemServico.Consultar("CONCORDAR", "CONCORDO")[0].Texto; }

    }
}
