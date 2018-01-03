using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public  class PaidVideoRepository : IRepository<PaidVideo>, IPaidVideoRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(PaidVideo item)
        {
            try
            {
                DbContextTeachersAssistant.PaidVideos.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PaidVideo item)
        {
            try
            {
                var paidDoc = DbContextTeachersAssistant.PaidVideos.SingleOrDefault(p => p.PaidVideoId == item.PaidVideoId);
                DbContextTeachersAssistant.PaidVideos.Remove(paidDoc);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PaidVideo[] GetAll()
        {
            return DbContextTeachersAssistant.PaidVideos.ToArray();
        }

        public PaidVideo GetById(int id)
        {
            return DbContextTeachersAssistant.PaidVideos.SingleOrDefault(p => p.PaidVideoId == id);
        }

        public bool Update(PaidVideo item)
        {
            try
            {
                var paidDoc = DbContextTeachersAssistant.PaidVideos.SingleOrDefault(p => p.PaidVideoId == item.PaidVideoId);
                paidDoc.PaidVideoId = item.PaidVideoId;
                paidDoc.FilePath = item.FilePath;
                paidDoc.SubjectId = item.SubjectId;
                paidDoc.RoleName = item.RoleName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
