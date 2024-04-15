using KenTaShop.Data;
using KenTaShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Services
{
    public interface IProducerRepository
    {
        Task<JsonResult> AddProducer(ProducerVM producerVM);
        Task<JsonResult> DeleteProducer(int idProducer);
        Task<JsonResult> EditProducer(int idProducer, ProducerVM producerVM);
        Task<List<ProducerMD>> GetAll();
        public Task<ProducerMD> GetById(int id);
    }
    public class ProducerRepository : IProducerRepository
    {
        private readonly ClothesShopManagementContext _context;
        public ProducerRepository(ClothesShopManagementContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> AddProducer(ProducerVM producerVM)
        {
            var producer = new Producer
            {
                Producername = producerVM.Producername,
                Location = producerVM.Location,
                Email = producerVM.Email,
                Phonenumber = producerVM.Phonenumber,

            };
            await _context.Producers.AddAsync(producer);
            _context.SaveChanges();
            return new JsonResult("da khoi tao ")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<JsonResult> DeleteProducer(int idProducer)
        {
            var check = await _context.Producers.SingleOrDefaultAsync(l => l.IdProducer == idProducer);
            if (check == null)
            {
                return new JsonResult("Chua tim thay de xoa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.Producers.Remove(check);
                _context.SaveChanges();
                return new JsonResult("da xoa ")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<JsonResult> EditProducer(int idProducer, ProducerVM producerVM)
        {
            var producer = await _context.Producers.SingleOrDefaultAsync(l => l.IdProducer == idProducer);
            if (producer == null)
            {
                return new JsonResult("khong tim thay loai can sua")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                producer.Producername = producerVM.Producername;
                producer.Location = producerVM.Location;
                producer.Email = producerVM.Email;
                producer.Phonenumber = producerVM.Phonenumber;

                _context.SaveChanges();
                return new JsonResult("da chinh sua")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<List<ProducerMD>> GetAll()
        {
            var producer = await _context.Producers.Select(u => new ProducerMD
            {
                IdProducer = u.IdProducer,
                Producername = u.Producername,
                Location = u.Location,
                Email = u.Email,
                Phonenumber = u.Phonenumber,
            }).ToListAsync();
            return producer;
        }

        public async Task<ProducerMD> GetById(int id)
        {
            var producer = await _context.Producers.SingleOrDefaultAsync(h => h.IdProducer == id);
            if (producer is null)
                return null;
            return new ProducerMD
            {
                IdProducer = producer.IdProducer,
                Producername = producer.Producername,
                Location = producer.Location,
                Email = producer.Email,
                Phonenumber = producer.Phonenumber,
            };
        }
    }
}
