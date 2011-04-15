using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BankAccount.Tests;
using MvcStarterProject.Business;
using MvcStarterProject.Config.StructureMap;
using NUnit.Framework;
using Should;
using StructureMap;
using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Tests.IntegrationTests.Config.StructureMap
{
    public class SampleEntity { }
    public class AnotherSampleEntity { }
    public class SampleRepository : IRepository<SampleEntity>
    {
        public IQueryable<SampleEntity> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public SampleEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<SampleEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<SampleEntity> Find(Expression<Func<SampleEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public SampleEntity Single(Expression<Func<SampleEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public SampleEntity First(Expression<Func<SampleEntity, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(SampleEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Create(SampleEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SampleEntity entity)
        {
            throw new NotImplementedException();
        }
    }
    public class SampleGetObjectService : GetObjectService<SampleEntity>
    {
        public SampleGetObjectService(IRepository<SampleEntity> repository)
            : base(repository)
        {
        }
    }
    public class SampleSaveObjectService : SaveObjectService<SampleEntity>
    {
        public SampleSaveObjectService(IRepository<SampleEntity> repository)
            : base(repository)
        {
        }
    }
    public class SampleDeleteObjectService : DeleteObjectService<SampleEntity>
    {
        public SampleDeleteObjectService(IRepository<SampleEntity> repository)
            : base(repository)
        {
        }
    }

    public class When_configuring_StructureMap : IntegrationTest
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            var config = new StructureMapConfiguration();
            config.ScanAssembly(GetType().Assembly);
            config.Configure();
        }

        [Test]
        public void Should_map_explicit_repository_classes()
        {
            ObjectFactory.GetInstance<IRepository<SampleEntity>>().ShouldBeType<SampleRepository>();
        }

        [Test]
        public void If_no_repository_class_exists_for_an_entity_should_use_the_default()
        {
            ObjectFactory.GetInstance<IRepository<AnotherSampleEntity>>().ShouldBeType(typeof(Repository<AnotherSampleEntity>));
        }

        [Test]
        public void Should_map_explicit_GetObjectService_classes()
        {
            ObjectFactory.GetInstance<IGetObjectService<SampleEntity>>().ShouldBeType(typeof(SampleGetObjectService));
        }

        [Test]
        public void If_no_GetObjectService_class_exists_for_an_entity_should_use_the_default()
        {
            ObjectFactory.GetInstance<IGetObjectService<AnotherSampleEntity>>().ShouldBeType(typeof(GetObjectService<AnotherSampleEntity>));
        }

        [Test]
        public void Should_map_explicit_SaveObjectService_classes()
        {
            ObjectFactory.GetInstance<ISaveObjectService<SampleEntity>>().ShouldBeType(typeof(SampleSaveObjectService));
        }

        [Test]
        public void If_no_SaveObjectService_class_exists_for_an_entity_should_use_the_default()
        {
            ObjectFactory.GetInstance<ISaveObjectService<AnotherSampleEntity>>().ShouldBeType(typeof(SaveObjectService<AnotherSampleEntity>));
        }

        [Test]
        public void Should_map_explicit_DeleteObjectService_classes()
        {
            ObjectFactory.GetInstance<IDeleteObjectService<SampleEntity>>().ShouldBeType(typeof(SampleDeleteObjectService));
        }

        [Test]
        public void If_no_DeleteObjectService_class_exists_for_an_entity_should_use_the_default()
        {
            ObjectFactory.GetInstance<IDeleteObjectService<AnotherSampleEntity>>().ShouldBeType(typeof(DeleteObjectService<AnotherSampleEntity>));
        }
    }
}