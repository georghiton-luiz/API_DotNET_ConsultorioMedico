using ClinicMedical.APIRest.Domain.Interfaces;
using CpfCnpjLibrary;
using System.Text.RegularExpressions;

namespace ClinicMedical.APIRest.Domain.Services
{
    public class FormatterService : IFormatterService
    {
        //DateFormatter
        public string ConvertToDateFormat(string date)
        {
            DateTime formattedDate;

            string[] formats = { "dd/MM/yyyy", "dd.MM.yyyy", "ddMMyyyy" };

            if (DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out formattedDate))
            {
                return formattedDate.ToString("dd/MM/yyyy");
            }
            else
            {
                throw new ArgumentException("Formato de data inválido.");
            }
        }

        //PhoneFormatter
        public string FormatPhoneNumber(string phone)
        {
            string numericOnly = Regex.Replace(phone, "[^0-9]", "");

            if (numericOnly.Length == 10 || numericOnly.Length == 11)
            {
                if (numericOnly.Length == 10)
                {
                    return string.Format("({0}) {1}-{2}", numericOnly.Substring(0, 2), numericOnly.Substring(2, 4), numericOnly.Substring(6));
                }
                else
                {
                    return string.Format("({0}) {1}-{2}", numericOnly.Substring(0, 2), numericOnly.Substring(2, 5), numericOnly.Substring(7));
                }
            }
            else
            {
                throw new ArgumentException("Número de telefone inválido.");
            }
        }

        //TimeFormatter
        public string FormatTime(string hours)
        {
            DateTime formattedTime;

            string[] formats = { "HH:mm", "H:mm", "HHmm", "Hmm" };

            if (DateTime.TryParseExact(hours, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out formattedTime))
            {
                return formattedTime.ToString("HH:mm");
            }
            else
            {
                throw new ArgumentException("Formato de hora inválido.");
            }
        }

        public string IsCPF_CNPJ(string cPF_CNPJ)
        {
            if (Cpf.Validar(cPF_CNPJ))
            {
                cPF_CNPJ = Cpf.FormatarComPontuacao(cPF_CNPJ);
            }
            else
            {
                if (Cnpj.Validar(cPF_CNPJ))
                {
                    cPF_CNPJ = Cnpj.FormatarComPontuacao(cPF_CNPJ);
                }
                else
                {
                    throw new Exception("CPF ou CNPJ inválidos");
                }
            }

            return cPF_CNPJ;
        }
    }
}

