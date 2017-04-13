using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SRR_Devolopment.Services;
using SRR_Devolopment.Model;

namespace SRR_Devolopment.UnitTest
{
    [TestFixture]
    public class TestingOne
    {
        [TestCase]
        public void GetMember()
        {
            IMemberDataService sut = new MemberDataService();
            
        }
    }
}
