using System.ComponentModel;

namespace ClinicMedical.APIRest.Domain.Enums
{
    public enum StatusAppointmentEnum
    {
        [Description("Scheduled")]
        Agendado,
        [Description("Canceled")]
        Cancelado,
        [Description("Return")]
        Retorno,
        [Description("Finished")]
        Finalizado
    }
}
