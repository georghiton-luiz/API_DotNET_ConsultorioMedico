using System.ComponentModel;

namespace ClinicMedical.APIRest.Domain.Enums
{
    public enum PerfilEnum
    {
        [Description("Admin")]
        Admin,
        [Description("Paciente")]
        Paciente,
        [Description("Médico")]
        Medico,
        [Description("Funcionário")]
        Funcionairo
    }
}
