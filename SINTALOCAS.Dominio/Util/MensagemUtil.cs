using System;
using SINTALOCAS.Dominio.Servico;

namespace SINTALOCAS.Dominio.Util
{
    public class MensagemUtil
    {
        public static string ErroCPFInvalido() { return MensagemServico.Consultar("ERRO", "CPFERRO")[0].Texto; }
        public static string ErroConfirmacaoSenha() { return MensagemServico.Consultar("ERRO", "SENHAERRO")[0].Texto; }
        public static string ErroTamanhoSenha() { return MensagemServico.Consultar("ERRO", "SENHATAM")[0].Texto; }
        public static string ErroCPFExistente() { return MensagemServico.Consultar("ERRO", "CPFEXISTENTE")[0].Texto; }
        public static string ErroEMAILExistente() { return MensagemServico.Consultar("ERRO", "EMAILEXISTE")[0].Texto; }
        public static string ErroExcluirUsuarioLogado() { return MensagemServico.Consultar("ERRO", "DELETEUSUARIOLOGADO")[0].Texto; }
        public static string ErroRGInvalido() { return MensagemServico.Consultar("ERRO", "RGERRO")[0].Texto; }
        public static string ErroEMAILInvalido() { return MensagemServico.Consultar("ERRO", "EMAILERRO")[0].Texto; }
        public static string ErroCNPJInvalido() { return MensagemServico.Consultar("ERRO", "CNPJERRO")[0].Texto; }
        public static string ErroPIsInvalido() { return MensagemServico.Consultar("ERRO", "PISERRO")[0].Texto; }
        public static string ErroConsirInvalido() { return MensagemServico.Consultar("ERRO", "CONSIRERRO")[0].Texto; }
        public static string ErroCEPInvalido() { return MensagemServico.Consultar("ERRO", "CEPERRO")[0].Texto; }
        public static string ErroUFInvalido() { return MensagemServico.Consultar("ERRO", "UFERRO")[0].Texto; }
        public static string ErroCamposNaoPreenchidos() { return MensagemServico.Consultar("ERRO", "CAMPOOBRIG")[0].Texto; }
        public static string ErroDTNASCInvalido() { return MensagemServico.Consultar("ERRO", "DTNASCERRO")[0].Texto; }
        public static string ErroGeneralizado() { return MensagemServico.Consultar("ERRO", "ERROGENERICO")[0].Texto; }
        public static string ErroIDForm() { return MensagemServico.Consultar("ERRO", "IDFORMERRO")[0].Texto; }
        public static string GrauParentescoAcimaPermitido(string grauNome) { return grauNome + " - " + MensagemServico.Consultar("ERRO", "GRAUPARENTEACIMA")[0].Texto; }
        public static string MensagemConcordar() { return MensagemServico.Consultar("CONCORDAR", "CONCORDO")[0].Texto; }
        public static string OperacaoRealizada() { return MensagemServico.Consultar("OP", "OPREALIZADA")[0].Texto; }
        public static string AvisoConcordo() { return MensagemServico.Consultar("AVISO", "CONCORDO")[0].Texto; }
        public static string Saudacao()
        {
            var saudacao = "Bom dia"; ;

            if (DateTime.Now.Hour >= 12)
            {
                saudacao = "Boa tarde";
            }

            if (DateTime.Now.Hour >= 18 || DateTime.Now.Hour < 5)
            {
                saudacao = "Boa noite";
            }

            return saudacao;
        }
    }
}
