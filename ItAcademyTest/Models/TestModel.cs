using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItAcademyTest.Models
{
    public class TestModel
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Размер массива")]
        public int Size { set; get; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Мин. значение")]
        public int MinValue { set; get; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Макс. значение")]
        public int MaxValue { set; get; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Вводимое число v")]
        public int xValue { set; get; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Номер первого элементоа более числа v ")]
        public int NumberOfFirstElementAboveX { set; get; }

        public int[] ArrayOfInt { set; get; }

        public TestModel()
        {
            ArrayOfInt = new int[Size];
        }
    }
}