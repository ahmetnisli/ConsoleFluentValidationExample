using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
namespace ConsoleFluentValidationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player()
            {
                Age = 152,
                Email = "bbff@",

            };
            PlayerValidator validator = new PlayerValidator();
            ValidationResult result = validator.Validate(player);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            Console.ReadKey();
        }
    }
    class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(p => p.Age).Must(a => a > 18 && a < 100).WithMessage("Geçersiz Yaş");
            RuleFor(p => p.NickName).NotEmpty().WithMessage("Nick boş olamaz");
            RuleFor(p => p.GalaxyName).NotEmpty().WithMessage("Galaxy Adı Girilmeli").Must(CheckGalaxy).WithMessage("Belirli Galxy adları Girilmeli");
            RuleFor(p => p.SahayaGirisTarihi).NotEmpty().WithMessage("Tarih Boş Olamaz").LessThan(DateTime.Now).WithMessage("Daha eski bir tarih olmalı");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçersiz Mail");
        }
        private bool CheckGalaxy(string GalaxyName)
        {
            string[] galaxies = { "Redyum", "platım", "sdadsa" };
            return galaxies.Contains(GalaxyName);
        }
    }
    public class Player
    {
        public string NickName { get; set; }
        public DateTime SahayaGirisTarihi { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string GalaxyName { get; set; }
    }
}
