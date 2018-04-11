using System;
namespace SINTALOCAS.Dominio.Util
{
    public class DataUtil
    {
        public static DateTime ConverterString( string dataTx)
        {
            var dtAux = Convert.ToDateTime(dataTx);
            DateTime dataResult = new DateTime();

            var vetorData = dtAux.ToString("dd/MM/yyyy").Split('/');

            if (vetorData.Length!=3) return dataResult;

            dataResult = Convert.ToDateTime( Convert.ToInt32(vetorData[2]) + "/" + Convert.ToInt32(vetorData[1]) + "/" + Convert.ToInt32(vetorData[0]));

            return dataResult;
        }
        
    }
}
