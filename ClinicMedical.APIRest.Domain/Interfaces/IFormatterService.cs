using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Domain.Interfaces
{
    public interface IFormatterService
    {
        string ConvertToDateFormat(string date);
        string FormatPhoneNumber(string phone);
        string FormatTime(string hours);
        string IsCPF_CNPJ(string cPF_CNPJ);
    }
}
