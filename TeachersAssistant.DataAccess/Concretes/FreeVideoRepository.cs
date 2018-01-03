using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class FreeVideoRepository : IRepository<FreeVideo>, IFreeVideoRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(FreeVideo item)
        {
            try
            {
                DbContextTeachersAssistant.FreeVideos.Add(item);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool Delete(FreeVideo item)
        {
            var freeVid = DbContextTeachersAssistant.FreeVideos.SingleOrDefault(p => p.FreeVideoId == item.FreeVideoId);
            DbContextTeachersAssistant.FreeVideos.Remove(freeVid);
            return true;
        }

        public FreeVideo[] GetAll()
        {
            return DbContextTeachersAssistant.FreeVideos.ToArray();
        }

        public FreeVideo GetById(int id)
        {
            return DbContextTeachersAssistant.FreeVideos.SingleOrDefault(p => p.FreeVideoId == id);
        }

        public bool Update(FreeVideo item)
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
