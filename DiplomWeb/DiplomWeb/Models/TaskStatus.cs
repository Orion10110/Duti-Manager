
using System.ComponentModel.DataAnnotations;

namespace DiplomWeb.Models
{
    public enum TaskStatus
    {
        новая,
        прочитанная,
        [Display(Name = "в работе")]
        в_работе,
        выполненая,
        [Display(Name = "на уточнении")]
        на_уточнении,
        [Display(Name = "не принятая")]
        не_принятая,
        закрытая,
        отмененная,
    }
}