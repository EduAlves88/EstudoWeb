using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Times
    {
        [Display(Name = "Id")]//validações DataAnnotation
        public int TimeId { get; set; }
        [Required(ErrorMessage ="Informe o TIME!")]//validações DataAnnotation
        public string Time { get; set; }
        [Required(ErrorMessage = "Informe o ESTADO do Time!")]//validações DataAnnotation
        public string Estado { get; set; }
        [Required(ErrorMessage = "Informe as CORES do Time!")]//validações DataAnnotation
        public string Cores { get; set; }
    }
}