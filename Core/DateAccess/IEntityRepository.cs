﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DateAccess
{

    //Generic constraint
    // class: referans tip
    // Burdaki amaç sınırlandırmak
    // IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    // new() : new'lenebilir olmalıdır
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entitiy);
        void Update(T entitiy);
        void Delete(T entitiy);

    }
}
