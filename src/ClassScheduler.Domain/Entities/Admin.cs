using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassScheduler.Domain.Entities;

public class Admin : UserBase
{
    public Admin(string firstName, string lastName, string email)
        : base(firstName, lastName, email)
    {

    }
}