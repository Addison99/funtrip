﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        
        IEnumerable<User> GetAll();

    }
}