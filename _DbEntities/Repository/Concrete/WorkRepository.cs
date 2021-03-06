﻿using _DbEntities.Models;
using _DbEntities.Models.ValueObj;
using _DbEntities.Models.ViewModel;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
    public class WorkRepository
    {
        private IRepository<Work> _WorkRepository;
        private IUnitOfWork _WorkUnitofWork;
        private ApplicationDbContext _dbContext;
        public WorkRepository()
        {
            _dbContext = new ApplicationDbContext();
            _WorkRepository = new EFRepository<Work>(_dbContext);
            _WorkUnitofWork = new EFUnitOfWork(_dbContext);
        }
        public int AddWork(Work entity) {
            _WorkRepository.Insert(entity);
            return _WorkUnitofWork.SaveChanges();

        }
        public int WorkUpdate(Work entity)
        {
            _WorkRepository.Updete(entity);
            return _WorkUnitofWork.SaveChanges();

        }
        public Work GetWork(Work entity)
        {

            return _WorkRepository.Find(entity);
        }
        public List<Work> GetListWorks(string UserId)
        {
            return _WorkRepository.GetAll(m => m.EmployeeUser_Id == UserId).ToList();
        }
        public List<Work> GetListWorksAftertoday(string UserId)
        {
           
            return _WorkRepository.GetAll(m => m.EmployeeUser_Id == UserId&&m.WorkDateTime.Day>= DateTime.Now.Day).ToList();
        }

        public List<Work> GetListPlannedWorks(string UserId)
        {
            return _WorkRepository.GetAll(m => m.EmployeeUser_Id == UserId&&m.WorkState=="Planlanan").ToList();
        }
        public List<Work> GetListWaitingWorks(string UserId)
        {
            return _WorkRepository.GetAll(m => m.EmployeeUser_Id == UserId && m.WorkState == "Beklemede").ToList();
        }
        public List<Work> GetListComletedWorks(string UserId)
        {
            return _WorkRepository.GetAll(m => m.EmployeeUser_Id == UserId && m.WorkState == "Tamamlandı").ToList();
        }
        public Work GetWorkById(Guid WorkId)
        {
            return _WorkRepository.Get(w=>w.Id==WorkId);
        }
        public int DeleteWork(Work entity)
        {
            _WorkRepository.Delete(entity);
            return _WorkUnitofWork.SaveChanges();
        }

    }
}
