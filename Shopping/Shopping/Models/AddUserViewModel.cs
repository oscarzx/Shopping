﻿using Shopping.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "EL campo {0} debe tener entre {2} y {1} caractéres")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no sol iguales.")]
        [Display(Name = "Confirmación de contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "EL campo {0} debe tener entre {2} y {1} caractéres")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }
    }
}
