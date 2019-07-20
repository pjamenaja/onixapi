using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Factories;
using Onix.Api.Commons;
using Onix.Test.Commons;

namespace Onix.Test.Api.Factories
{
    public class FactoryDbOperationTest : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        private IDatabaseSequence getSequenceOperation(string dbName, string name)
        {
            DbContext ctx = getInmemoryDbContextForQueryTesting(dbName);
            IDatabaseSequence opr = FactoryDbOperation.GetDataSequenceObject(name, ctx);

            return opr;
        }

        [TestCase("ManipulationGetSeq")]
        public void GetDataSequenceObjectTest(string seqOprName)
        {
            IDatabaseSequence opr = getSequenceOperation("InmoryTestDB", seqOprName);

            Type expectedType = typeof(ManipulationGetSeq);
            Assert.IsInstanceOf(expectedType, opr, "Expected type {0} to return", expectedType.FullName);
        }                                  
    }    
}