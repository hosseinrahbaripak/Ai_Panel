using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live_Book.Application.DTOs
{
    public class BackUpViewModel
    {
        [DisplayName("نام")]
        public string Name { get; set; }

        [DisplayName("تاریخ ساخت")]
        public DateTime DateCreate { get; set; }
    }
}