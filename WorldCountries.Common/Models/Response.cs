﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCountries.Common.Models
{
    public class Response<T> where T : class
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<T> Result { get; set; }
    }
}
