using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic
    //class=referans tip
    //IEntity=Ientity olaiblir yada onu ımplemente eden bir nesne olabilir.
    //new(): newlenebilir olmalı 
  public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression <Func<T,bool>> filter =null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
