﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NoiDen
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int Gia { get; set; }
        public override string ToString()
        {
            return Ten;
        }
    }
}
