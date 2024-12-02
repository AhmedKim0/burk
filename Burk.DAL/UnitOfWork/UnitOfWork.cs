using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Imp;
using Burk.DAL.Repository.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Burk.DAL.UnitOfWork.UnitOfWork;

namespace Burk.DAL.UnitOfWork;
public class UnitOfWork:IUnitOfWork
{

		private readonly BurkDbContext _context;

		public UnitOfWork(BurkDbContext context)
		{
			_context = context;
		WaitingLists = new Repository<WaitingList>(_context);
		Reviews = new Repository<Review>(_context);
		RecordedVisits = new Repository<RecordedVisit>(_context);
		Questions = new Repository<Question>(_context);
		Clients = new Repository<Client>(_context);
	}

		public IAsyncRepository<WaitingList> WaitingLists { get; private set; }

		public IAsyncRepository<Review> Reviews { get; private set; }

		public IAsyncRepository<RecordedVisit> RecordedVisits { get; private set; }
		public IAsyncRepository<Question> Questions { get; private set; }
		public IAsyncRepository<Client> Clients { get; private set; }



	//public IRepostory<Employee> employees { get;private set; }

	public int commitChanges()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}

