using System;
using System.Collections.Generic;

#nullable disable

namespace ConsoleAppScafold_DbContext
{
    public partial class Продукты
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
    }
}
