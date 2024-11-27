using AutoMapper;
using Burk.Client.BL.Interfaces;
using Burk.Client.DTO;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Burk.Client.BL.Imp
{
    public class UserReserveService: IUserReserveService
	{
        private readonly IAsyncRepository<WaitingList> _waitinigRepo;
        private readonly IAsyncRepository<DAL.Entity.Client> _clientRepo;
        private readonly IMapper _mapper;

        public UserReserveService(IAsyncRepository<WaitingList> waitinigRepo,
            IAsyncRepository<Burk.DAL.Entity.Client> clientRepo,
            IMapper mapper
           )
        {
            _waitinigRepo=waitinigRepo;
            _clientRepo=clientRepo;
            _mapper=mapper;
            
        }
        public async Task<string> AddReservaiton(WatinigListDto waiting)
        {
            var client = await  _clientRepo.FirstOrDefaultAsync(c => c.PhoneNumber == waiting.PhoneNumber,false);
            if (client == null)
            {
                Burk.DAL.Entity.Client addClient = new()
                {
                    Name = waiting.ClientName,
                    PhoneNumber = waiting.PhoneNumber,
                    Email = waiting.Email,
                    

                };


				WaitingList waitinglist = _mapper.Map<WaitingList>(waiting);

				client= await _clientRepo.AddAsync(addClient);
				waitinglist.ClientId =client.Id;
                await _waitinigRepo.AddAsync(waitinglist);
                return "success and new";
			}
            else
            {
                if (client.Name != waiting.ClientName)
                {
                    client.Name = waiting.ClientName;
                    await _clientRepo.UpdateAsync(client);
                }



				WaitingList waitinglist = _mapper.Map<WaitingList>(waiting);
				client = await _clientRepo.FirstOrDefaultAsync(c => c.PhoneNumber == waiting.PhoneNumber, false);
				waitinglist.ClientId = client.Id;
				await  _waitinigRepo.AddAsync(waitinglist);
				return "success";

			}
        }
        

        
    }
}
