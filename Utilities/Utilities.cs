using System;
using Entidades;
using BLL;

namespace Utilities
{
    public partial class Utilities
    {
        public static decimal ToDecimal(string criterio)
        {
            if(!string.IsNullOrEmpty(criterio) || !string.IsNullOrWhiteSpace(criterio))
            {
                string numero = "";
                for(int i = 0; i<criterio.Length;i++)
                {
                    if (char.IsNumber(criterio[i])|| (criterio[i]=='.'))
                    {
                        if(numero.Contains('.') && criterio[i] == '.')
                            continue;
                        numero += criterio[i];
                    }
                }
                if(!string.IsNullOrEmpty(numero) || !string.IsNullOrWhiteSpace(numero))
                {
                    return Convert.ToDecimal(numero);
                }else{
                    return -1;
                }
            }else{
                return -1;
            }
        }
        public static int ToIntConsulta(string criterio)
        {
            if(!string.IsNullOrEmpty(criterio) || !string.IsNullOrWhiteSpace(criterio))
            {

                string numero = "";
                for(int i = 0; i<criterio.Length;i++)
                {
                    if (char.IsNumber(criterio[i]))
                    {
                        numero += criterio[i];
                    }
                }
                if(!string.IsNullOrEmpty(numero) || !string.IsNullOrWhiteSpace(numero))
                {
                    return Convert.ToInt32(numero);
                }else{
                    return -1;
                }
            }else{
                return -1;
            }
        }
    }
}