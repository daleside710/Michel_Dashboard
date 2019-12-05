using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Enum
{
    public enum UserType
    {
        [Display(Name = "SAC")]
        SAC = 0,
        ADM =1,
        TAL = 2,
    }
}
