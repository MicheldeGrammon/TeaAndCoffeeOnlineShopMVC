﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndCoffee_Models;

namespace TeaAndCoffee_DataAccess.Repository.IRepository
{
    public interface IInquiryDetailsRepository : IRepository<InquiryDetails>
    {
        void Update(InquiryDetails obj);
    }
}