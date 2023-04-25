using Core.DateAccess.EntityFramework;
using DateAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DateAccess.Concrete.EntitiyFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext> ,ICategoryDal
    {
        
    }
}
