using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shared
{
    public class TestModel
    {
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Soyad")]
        public string Surname { get; set; }
    }
}
