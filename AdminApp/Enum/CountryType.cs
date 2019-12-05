using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Enum
{
    public enum CountryType
    {
        [Display(Name = "ESPAÑA")]
        ESPAÑA = 0,
        PORTUGAL = 1,
        PENINSULA = 2,
    }
}
