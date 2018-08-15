﻿using Park.Entitys.FareRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Park.Tests.FareRule
{
    public   class FareRule_Test
    {
        [Fact]
        public void time()
        {

            var inTime = new DateTime(2018, 8, 15, 16, 39, 0);var outTime = new DateTime(2018, 8, 16, 3, 03, 2);
            Park.Entitys.FareRules.FareRule fareRule = new Entitys.FareRules.FareRule();
            fareRule.TimeRangeList = new List<RangeTime>() {
                new RangeTime() {  StartTime=new TimeSpan(7,0,0), FareRule=fareRule, EndTime=new TimeSpan(20,0,0), FeeMinutes=60,  FeeMoney=2, TopMoney=26, MinSpanTime=30 },
                new RangeTime() {  StartTime=new TimeSpan(20,1,0), FareRule=fareRule, EndTime=new TimeSpan(6,59,0), FeeMinutes=60,  FeeMoney=2, TopMoney=5, MinSpanTime=30 }

            };
            fareRule.DayMaxMoney = 31;
            fareRule.IsStartTopMoney = true;
            fareRule.IsInculdeFeeTime = true;
            fareRule.FreeTime = 30;
            fareRule.PreFeeDate = 15;

            var money = fareRule.CalculateFees(inTime,outTime, 0);

            Assert.Equal(money,13);
        }
    }
}
