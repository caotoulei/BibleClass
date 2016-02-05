using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using SS.Web.Models;

namespace SS.Web.DAL
{
    public class ClassContext : DbContext
    {
        private DbContextTransaction _currentTransaction;
       
        public ClassContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        public DbSet<Class> Classes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Class>().Map(m =>
            {
                m.ToTable("dbo.Syllabus");
            });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void BeginTransaction()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    return;
                }

                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception)
            {
                // todo: log transaction exception
                throw;
            }
        }
        public void CloseTransaction()
        {
            CloseTransaction(exception: null);
        }

        public void CloseTransaction(Exception exception)
        {
            try
            {
                if (_currentTransaction != null && exception != null)
                {
                    // todo: log exception
                    _currentTransaction.Rollback();
                    return;
                }

                SaveChanges();

                if (_currentTransaction != null)
                {
                    _currentTransaction.Commit();
                }
            }
            catch (Exception)
            {
                // todo: log exception
                if (_currentTransaction != null && _currentTransaction.UnderlyingTransaction.Connection != null)
                {
                    _currentTransaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}