﻿using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{

    public class SablonDal : Repository<Sablon>, ISablonDal { public SablonDal(DbSet<Sablon> dbset) : base(dbset) { } }
}
