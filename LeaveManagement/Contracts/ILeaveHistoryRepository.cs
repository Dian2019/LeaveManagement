﻿using LeaveManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Contracts
{
    public interface ILeaveHistoryRepository : IRepositoryBase<LeaveHistory>
    {
        ICollection<LeaveHistory> GetEmployeesByLeaveHistory(int id);
    }
}
