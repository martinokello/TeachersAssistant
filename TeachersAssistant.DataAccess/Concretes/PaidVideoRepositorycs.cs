using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public  class PaidVideoRepository : AbstractRepository<PaidVideo>, IPaidVideoRepositoryMarker
    {
 
        public override PaidVideo GetById(int id)
        {
            return DbContextTeachersAssistant.PaidVideos.SingleOrDefault(p => p.PaidVideoId == id);
        }

        public override bool Update(PaidVideo item)
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
