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
    public class FreeVideoRepository : AbstractRepository<FreeVideo>, IFreeVideoRepositoryMarker
    {
        public override FreeVideo GetById(int id)
        {
            return DbContextTeachersAssistant.FreeVideos.SingleOrDefault(p => p.FreeVideoId == id);
        }

        public override bool Update(FreeVideo item)
        {
            try
            {
                var freeVid = DbContextTeachersAssistant.FreeVideos.SingleOrDefault(p => p.FreeVideoId == item.FreeVideoId);
                freeVid.FilePath = item.FilePath;
                freeVid.SubjectId = item.SubjectId;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
