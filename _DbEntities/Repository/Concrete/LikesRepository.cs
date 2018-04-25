using _DbEntities.Models;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
    public class LikesRepository
    {
        private IRepository<Likes> _likesRepository;
        private IUnitOfWork _likesUnitofWork;
        private ApplicationDbContext _dbContext;
        public LikesRepository()
        {
            _dbContext = new ApplicationDbContext();
            _likesRepository = new EFRepository<Likes>(_dbContext);
            _likesUnitofWork = new EFUnitOfWork(_dbContext);
        }
        public List<Guid> GetLikedList(string UserId, Guid[] WorkflowIds)
        {
            List<Guid> likedWorkFlowIds = _likesRepository.GetAll(x => x.LikeUser.Id == UserId && WorkflowIds.Contains(x.WorkflowId)).Select(x => x.Workflow.Id).ToList();
            return likedWorkFlowIds;
        }
        public Likes GetLiked(string UserId, Guid WfId)
        {
            return _likesRepository.GetAll(x => x.LikeUser.Id == UserId && x.WorkflowId == WfId).FirstOrDefault();
        }
        public int RemoveLike(Likes like)
        {
            _likesRepository.Delete(like);
          return  _likesUnitofWork.SaveChanges();
        }
        public int AddLike(Likes entity)
        {
            _likesRepository.Insert(entity);
           return _likesUnitofWork.SaveChanges();
        }
    }
}
