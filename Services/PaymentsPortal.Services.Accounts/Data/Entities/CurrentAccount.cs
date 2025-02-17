﻿using System.ComponentModel.DataAnnotations.Schema;
using PaymentsPortal.Common.DTOs.Accounts.Enums;

namespace PaymentsPortal.Services.Accounts.Data.Entities
{
    public class CurrentAccount : Account
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal OverdraftLimit { get; set; }

        public CurrentAccount() : base(AccountType.Current) { }
    }
}




