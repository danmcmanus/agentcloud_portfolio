using Insure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insure.Web.Logic
{
    public class Calculator
    {
        private double? totalCost;

        public double? TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        DataContext db = new DataContext();
        
        public double? calculateCost()
        {
            double totalCost;
            // Once logic worked out change to form input data
            var policy = new { UserId = 2, CompanyId = 001, Name = "Aurora 5500 HDHP POS", Premium = 280, Deductible = 5500, CoInsurance = 0.20 };
            double procedureCost = 11000;
            double outOfPocketMax = 8000;
            double annualPremium = policy.Premium * 12;


            if (procedureCost < policy.Deductible)
            {
                totalCost = procedureCost;
            }
            else if ((procedureCost - policy.Deductible) + (policy.Deductible * policy.CoInsurance) > outOfPocketMax)
            {
                totalCost = outOfPocketMax;
            }
            else
            {
                totalCost = policy.Deductible + (procedureCost - policy.Deductible) * (policy.CoInsurance);
            }
            totalCost = totalCost + annualPremium;
            return totalCost;
        }
    }
}