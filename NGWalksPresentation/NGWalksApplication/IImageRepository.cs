using NGWalksDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGWalksApplication
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
