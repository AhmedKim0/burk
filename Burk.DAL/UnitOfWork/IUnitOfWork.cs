using Burk.DAL.Entity;
using Burk.DAL.Repository.Imp;
using Burk.DAL.Repository.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.UnitOfWork

{
    public interface IUnitOfWork : IDisposable
	{
		IAsyncRepository<WaitingList> WaitingLists { get;  }
		IAsyncRepository<Review> Reviews { get; }
		IAsyncRepository<RecordedVisit> RecordedVisits { get; }
		IAsyncRepository<Question> Questions { get;  }
		IAsyncRepository<Client> Clients { get;  }

		int commitChanges();
	}
}

