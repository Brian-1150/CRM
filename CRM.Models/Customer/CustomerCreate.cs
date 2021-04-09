using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class CustomerCreate : Person
    {
        //Attempt at highlighting IN in EnumDropDownList
        public new PersonState StateOfPerson { get; set; } = PersonState.IN;
        public TextColor TextColor => StateOfPerson == PersonState.IN ? TextColor.Primary : TextColor.Secondary;
    }
}
